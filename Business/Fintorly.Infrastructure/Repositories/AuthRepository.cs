using AutoMapper;
using Fintorly.Application.Features.Commands.AuthCommands;
using Fintorly.Application.Features.Commands.EmailCommands;
using Fintorly.Application.Interfaces.Utils;
using Fintorly.Domain.Common;
using Fintorly.Domain.Entities;
using Fintorly.Domain.Utils;
using Fintorly.Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Fintorly.Infrastructure.Repositories;

public class AuthRepository:IAuthRepository
{
    FintorlyContext _context;
    IPhoneService _phoneService;
    IMailService _mailService;
    private string _ipAddress;


    public AuthRepository(FintorlyContext context, IPhoneService phoneService, IMailService mailService, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _phoneService = phoneService;
        _mailService = mailService;
        _ipAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
    }

    public async Task<IResult> ActiveEmailByActivationCodeAsync(EmailActiveCommand emailActiveCommand)
    {
        var verificationCode =
            await _context.VerificationCodes.SingleOrDefaultAsync(a =>
                a.EmailAddress == emailActiveCommand.EmailAddress);
        if (verificationCode is null)
            return Result.Fail("Bu Mail adresi ile daha önce kayıt oluşturma işleminde bulunulmamıştır.");
        if (verificationCode.MailCode != emailActiveCommand.ActivationCode)
            return Result.Fail("Doğrulamada kodu doğru değildir.");
        if (verificationCode.MailCode == emailActiveCommand.ActivationCode &&
            verificationCode.VerificationCodeValidDate < DateTime.Now)
            return Result.Fail("Bu kodun geçerlilik süresi sona ermiştir");
        
        verificationCode.IsMailConfirmed = true;
        _context.VerificationCodes.Update(verificationCode);
        await _context.SaveChangesAsync();
        return Result.Success($"{emailActiveCommand.EmailAddress} mail adresi başarıyla onaylanmıştır.");
    }

    public async Task<IResult> ActivePhoneByActivationCodeAsync(PhoneActiveCommand phoneActiveCommand)
    {
        //ValidationTool.Validate(new UserPhoneActiveCommandValidator(), userPhoneActiveCommand);
        var verificationCode =
            await _context.VerificationCodes.SingleOrDefaultAsync(a =>
                a.PhoneNumber == phoneActiveCommand.PhoneNumber);
        if (verificationCode is null)
            return Result.Fail("Bu Telefon numarası ile daha önce kayıt oluşturma işleminde bulunulmamıştır.");
        if (verificationCode.PhoneCode == phoneActiveCommand.ActivationCode &&
            verificationCode.VerificationCodeValidDate < DateTime.Now)
            return Result.Fail("Bu kodun geçerlilik süresi sona ermiştir");
        if (verificationCode.PhoneCode != phoneActiveCommand.ActivationCode)
            return Result.Fail("Doğrulamada kodu doğru değildir.");

        verificationCode.IsPhoneNumberConfirmed = true;
        _context.VerificationCodes.Update(verificationCode);
        await _context.SaveChangesAsync();
        return Result.Success($"{phoneActiveCommand.PhoneNumber} numaralı hesap başarıyla onaylanmıştır.");
    }

    public async Task<IResult> SendActivationCodeEmailAsync(
        SendActivationCodeEmailAddressCommand activationCodeEmailAddressCommand)
    {
        var verification = await _context.VerificationCodes.SingleOrDefaultAsync(a =>
            a.EmailAddress == activationCodeEmailAddressCommand.EmailAddress);
        if (verification is null)
            return Result.Fail("Lütfen kayıt olma ekranına geri dönüp tekrar deneyiniz.");
      
        verification.MailCode = VerificationCodeGenerator.Generate();
        verification.VerificationCodeValidDate = DateTime.Now.AddMinutes(3);
        verification.ModifiedDate = DateTime.Now;
        _context.VerificationCodes.Update(verification);
        await _context.SaveChangesAsync();
        await _mailService.SendEmail(new EmailSendCommand
        {
            ReceiverMail = activationCodeEmailAddressCommand.EmailAddress,
            Subject = "Fintorly Doğrulama Kodu",
            Content =
                $@" Doğrulama kodunun süresi 3 dakika ile sınırlıdır. Doğrulama kodunuz: {verification.MailCode}"
        });
        return Result.Success(
            "Doğrulama kodu başarıyla mail adresinize gönderildi. Kodun geçerlilik süresi 3 dakikadır.",
            verification);
    }

    public async Task<IResult> SendActivationCodePhoneAsync(
        SendActivationCodePhoneNumberCommand activationCodePhoneNumberCommand)
    {
        var phoneIsExist = await _context.VerificationCodes.SingleOrDefaultAsync(a =>
            a.PhoneNumber == activationCodePhoneNumberCommand.PhoneNumber);
        if (phoneIsExist is null)
            return Result.Fail("Lütfen kayıt olma ekranına geri dönüp tekrar deneyiniz.");
        phoneIsExist.PhoneCode = VerificationCodeGenerator.Generate();
        phoneIsExist.VerificationCodeValidDate = DateTime.Now.AddMinutes(3);
        phoneIsExist.ModifiedDate = DateTime.Now;
        _context.VerificationCodes.Update(phoneIsExist);
        await _context.SaveChangesAsync();
        await _phoneService.SendPhoneVerificationCodeAsync(activationCodePhoneNumberCommand.PhoneNumber,
            phoneIsExist.PhoneCode);
        return Result.Success(
            "Doğrulama kodu başarıyla telefon numaranıza gönderildi. Kodun geçerlilik süresi 3 dakikadır.",
            phoneIsExist);
    }

    public async Task<IResult> VerificationCodeAddAsync(VerificationCodeAddCommand verificationCodeAddCommand)
    {
        var verification = _context.VerificationCodes.Where(a =>
            a.PhoneNumber == verificationCodeAddCommand.PhoneNumber ||
            a.EmailAddress == verificationCodeAddCommand.EmailAddress);
        if (verification.Count() > 0)
            foreach (var item in verification)
                _context.VerificationCodes.Remove(item);

        VerificationCode verificationCode = new VerificationCode()
        {
            EmailAddress = verificationCodeAddCommand.EmailAddress,
            PhoneNumber = verificationCodeAddCommand.PhoneNumber,
            CreatedDate = DateTime.Now,
            IsMailConfirmed = false,
            IsPhoneNumberConfirmed = false,
            IpAddress = _ipAddress
        };

        await _context.VerificationCodes.AddAsync(verificationCode);
        await _context.SaveChangesAsync();
        return Result.Success("Kod eşleşmesi başarıyla oluşturuldu.");
    }
}