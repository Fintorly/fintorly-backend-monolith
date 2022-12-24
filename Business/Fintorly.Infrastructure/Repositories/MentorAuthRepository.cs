using System.Globalization;
using AutoMapper;
using Fintorly.Application.Dtos.MentorDtos;
using Fintorly.Application.Dtos.PortfolioDtos;
using Fintorly.Application.Dtos.UserDtos;
using Fintorly.Application.Features.Commands.AuthCommands;
using Fintorly.Application.Features.Commands.EmailCommands;
using Fintorly.Application.Features.Commands.MentorAuth;
using Fintorly.Application.Features.Queries.AuthQueries;
using Fintorly.Application.Interfaces.Utils;
using Fintorly.Domain.Common;
using Fintorly.Domain.ConfigureEntities;
using Fintorly.Domain.Entities;
using Fintorly.Domain.Utils;
using Fintorly.Infrastructure.Context;
using Fintorly.Infrastructure.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Fintorly.Infrastructure.Repositories;

public class MentorAuthRepository : GenericRepository<Mentor>, IMentorAuthRepository
{
    FintorlyContext _context;
    IJwtHelper _jwtHelper;
    IPhoneService _phoneService;
    IMailService _mailService;
    IHttpContextAccessor _httpContextAccessor;

    IMapper _mapper;

    //IUserProfilePictureService _userProfilePictureService;
    IPortfolioRepository _portfolioRepository;
    private string _ipAddress;

    public MentorAuthRepository(IMapper mapper, FintorlyContext context, IJwtHelper jwtHelper, IMailService mailService,
        IPhoneService phoneService, IHttpContextAccessor httpContextAccessor,
        IPortfolioRepository portfolioRepository) : base(context)
    {
        _jwtHelper = jwtHelper;
        _mailService = mailService;
        _phoneService = phoneService;
        _httpContextAccessor = httpContextAccessor;
        _portfolioRepository = portfolioRepository;
        _mapper = mapper;
        _context = context;
        // _userProfilePictureService = userProfilePictureService;
        _ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
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

    public async Task<IResult> CheckCodeIsTrueByPhoneAsync(
        CheckCodeIsTrueByPhoneNumberQuery codeIsTrueByPhoneNumberQuery)
    {
        var mentor = await _context.Mentors.SingleOrDefaultAsync(a =>
            a.PhoneNumber == codeIsTrueByPhoneNumberQuery.PhoneNumber);
        if (mentor is null)
            return Result.Fail("Böyle bir kullanıcı bulunamadı");

        var codeVerify =
            await _context.VerificationCodes.SingleOrDefaultAsync(a =>
                a.PhoneNumber == codeIsTrueByPhoneNumberQuery.PhoneNumber);
        if (codeVerify is null)
            return Result.Fail("Bu Telefon Numarasına ait bir doğrulama bilgisi bulunamadı.");
        if (codeVerify.PhoneCode != codeIsTrueByPhoneNumberQuery.VerificationCode)
            return Result.Fail("Doğrulama Kodu Doğru değil..");
        if (codeVerify.VerificationCodeValidDate < DateTime.Now)
            return Result.Fail("Doğrulama kodu süresi geçmiştir lütfen tekrar deneyiniz");
        else
            return Result.Success("Doğrulama Başarılı.");
    }

    public async Task<IResult> CheckCodeIsTrueByEmailAsync(
        CheckCodeIsTrueByEmailAddressQuery codeIsTrueByEmailAddressQuery)
    {
        var mentor = await _context.Mentors.SingleOrDefaultAsync(a =>
            a.EmailAddress == codeIsTrueByEmailAddressQuery.EmailAddress);
        if (mentor is null)
            return Result.Fail("Böyle bir mentör bulunamadı");

        var codeVerify = await _context.VerificationCodes.SingleOrDefaultAsync(a =>
            a.EmailAddress == codeIsTrueByEmailAddressQuery.EmailAddress);
        if (codeVerify is null)
            return Result.Fail("Bu Maile ait bir doğrulama bilgisi bulunamadı.");
        if (codeVerify.MailCode != codeIsTrueByEmailAddressQuery.VerificationCode)
            return Result.Fail("Doğrulama Kodu Doğru değil..");
        if (codeVerify.VerificationCodeValidDate < DateTime.Now)
            return Result.Fail("Doğrulama kodu süresi geçmiştir lütfen tekrar deneyiniz");

        return Result.Success("Doğrulama Başarılı.");
    }

    public async Task<AccessToken> CreateAccessTokenAsync(Mentor mentor)
    {
        var claims = await GetClaimsAsync(mentor);
        var accessToken = await _jwtHelper.CreateTokenAsync(mentor, claims,true);
        return accessToken;
    }

    public async Task<IResult> ChangePasswordAsync(ChangePasswordCommand changePasswordCommand)
    {
        var mentor = await _context.Mentors.SingleOrDefaultAsync(a => a.Id == changePasswordCommand.UserId);
        if (mentor is null)
            return Result.Fail("Böyle bir kullanıcı bulunamadı.");

        var isPasswordTrue = HashingHelper.VerifyPasswordHash(changePasswordCommand.Password, mentor.PasswordHash,
            mentor.PasswordSalt);
        if (!isPasswordTrue)
            return Result.Fail();

        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(changePasswordCommand.Password, out passwordHash, out passwordSalt);
        mentor.PasswordHash = passwordHash;
        mentor.PasswordSalt = passwordSalt;
        mentor.ModifiedDate = DateTime.Now;
        _context.Update(mentor);
        await _context.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<IResult> ForgotPasswordEmailAsync(
        ForgotPasswordEmailCommand changePasswordEmailCommand)
    {
        var mentor = await _context.Mentors.SingleOrDefaultAsync(a =>
            a.EmailAddress == changePasswordEmailCommand.EmailAddress);
        if (mentor is null)
            return Result.Fail(Messages.General.NotFoundArgument("kullanıcı"));
        if (changePasswordEmailCommand.Password != changePasswordEmailCommand.ReTypePassword)
            return Result.Fail("Şifreler aynı değil.");

        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(changePasswordEmailCommand.Password, out passwordHash,
            out passwordSalt);
        mentor.PasswordHash = passwordHash;
        mentor.PasswordSalt = passwordSalt;
        mentor.ModifiedDate = DateTime.Now;
        _context.Mentors.Update(mentor);
        await _context.SaveChangesAsync();
        return Result.Success("Şifre başarıyla değiştirildi.");
    }

    public async Task<IResult> ForgotPasswordPhoneAsync(ForgotPasswordPhoneCommand changePasswordCommand)
    {
        var mentor = await _context.Mentors.SingleOrDefaultAsync(a =>
            a.PhoneNumber == changePasswordCommand.PhoneNumber);
        if (mentor is null)
            return Result.Fail(Messages.General.NotFoundArgument("kullanıcı"));
        if (changePasswordCommand.Password != changePasswordCommand.ReTypePassword)
            return Result.Fail("Şifreler aynı değil.");

        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(changePasswordCommand.Password, out passwordHash, out passwordSalt);
        mentor.PasswordHash = passwordHash;
        mentor.PasswordSalt = passwordSalt;
        mentor.ModifiedDate = DateTime.Now;
        _context.Mentors.Update(mentor);
        await _context.SaveChangesAsync();

        return Result.Success("Şifre başarıyla değiştirildi.");
    }

    public async Task<IEnumerable<OperationClaim>> GetClaimsAsync(Mentor mentor)
    {
        var roles = await _context.OperationClaims.ToListAsync();
        var userRoles = _context.MentorAndOperationClaims.Where(a => a.MentorId == mentor.Id);
        var list = new List<OperationClaim>();
        await userRoles.ForEachAsync(a =>
        {
            var role = roles.SingleOrDefault(b => b.Id == a.OperationClaimId);
            if (role != null) list.Add(role);
        });
        return list;
    }

    public async Task<IResult<MentorAndTokenDto>> LoginWithEmailAsync(MentorLoginWithMailCommand loginWithMailCommand)
    {
        //ValidationTool.Validate(new LoginWithMailCommandValidator(), loginWithMailCommand);
        //Include(a => a.Portfolios).ThenInclude(c => c.PortfolioTokens)
        var mentor = await _context.Mentors.SingleOrDefaultAsync(a =>
            a.EmailAddress == loginWithMailCommand.EmailAddress);

        if (mentor is null)
            return Result<MentorAndTokenDto>.Fail(Messages.General.NotFoundArgument("mentör"));

        if (!mentor.IsEmailAddressVerified)
            return Result<MentorAndTokenDto>.Fail("Mail adresinizi doğrulayın.");

        if (HashingHelper.VerifyPasswordHash(loginWithMailCommand.Password, mentor.PasswordHash, mentor.PasswordSalt))
        {
            if (!mentor.IsActive)
                return Result<MentorAndTokenDto>.Fail("Hesabınızı Aktif Etmek İçin Destek ile İletişime Geçiniz.");
            if (!mentor.IsEmailAddressVerified)
                return Result<MentorAndTokenDto>.Fail(
                    "Hesabınızı Aktif Etmek İçin Mailinizi Doğrulamanız Gerekmektedir.");

            mentor.LastLogin = DateTime.Now;
            mentor.IpAddress = _ipAddress;

            var currentPortfolio = mentor.Portfolios.SingleOrDefault(a => a.Id == mentor.CurrentPortfolioId);
            if (currentPortfolio is null)
            {
                currentPortfolio = mentor.Portfolios.FirstOrDefault();
                if (currentPortfolio is not null)
                    mentor.CurrentPortfolioId = currentPortfolio.Id;
                else
                {
                    var result =
                        (await _portfolioRepository.CreatePortfolioAsync(mentor.Id, "My Portfolio"))
                        .Data as Portfolio;
                    mentor.CurrentPortfolioId = result.Id;
                    currentPortfolio = result;
                }
            }

            var accessToken = await CreateAccessTokenAsync(mentor);
            AccessToken userToken = new AccessToken
            {
                MentorId = mentor.Id,
                Token = accessToken.Token,
                CreatedDate = DateTime.Now,
                IpAddress = mentor.IpAddress,
                Mentor = mentor
            };
            _context.Mentors.Update(mentor);
            await _context.AccessTokens.AddAsync(userToken);
            await _context.SaveChangesAsync();
            var userLoginCommand = new MentorAndTokenDto()
            {
                TokenId = userToken.Id,
                Token = userToken.Token,
                MentorId = mentor.Id,
                Mentor= _mapper.Map<MentorDto>(mentor),
                IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                CreatedDate = DateTime.Now,
            };
            userLoginCommand.Mentor.Portfolio = _mapper.Map<PortfolioDto>(currentPortfolio);
            userLoginCommand.Mentor.CurrentPortfolioId = mentor.CurrentPortfolioId;

            return Result<MentorAndTokenDto>.Success(userLoginCommand);
        }

        return Result<MentorAndTokenDto>.Fail();
    }

    public async Task<IResult<MentorAndTokenDto>> LoginWithPhoneAsync(MentorLoginWithPhoneCommand loginWithPhoneCommand)
    {
        //ValidationTool.Validate(new LoginWithPhoneCommandValidator(), loginWithPhoneCommand);
        
        var mentor = await _context.Mentors.Include(a => a.Portfolios).ThenInclude(c => c.PortfolioTokens).ThenInclude(a => a.PortfolioTransactions).SingleOrDefaultAsync(
            a => a.PhoneNumber == loginWithPhoneCommand.PhoneNumber);
        if (mentor is null)
            return Result<MentorAndTokenDto>.Fail(Messages.General.NotFoundArgument("kullanıcı"));

        if (HashingHelper.VerifyPasswordHash(loginWithPhoneCommand.Password, mentor.PasswordHash, mentor.PasswordSalt))
        {
            if (!mentor.IsActive)
                return Result<MentorAndTokenDto>.Fail("Hesabınızı Aktif Etmek İçin Destek ile İletişime Geçiniz.");
            if (!mentor.IsEmailAddressVerified)
                return Result<MentorAndTokenDto>.Fail(
                    "Hesabınızı Aktif Etmek İçin Mailinizi Doğrulamanız Gerekmektedir.");
            if (!mentor.IsPhoneNumberVerified)
                //return Result.(ResultStatus.Error, "Hesabınızı Aktif Etmek İçin Telefon Numaranızı Doğrulamanız Gerekmektedir.");
                mentor.LastLogin = DateTime.Now;
            mentor.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            var currentPortfolio = mentor.Portfolios.SingleOrDefault(a => a.Id == mentor.CurrentPortfolioId);

            if (currentPortfolio is null)
            {
                var firstPortfolio = mentor.Portfolios.FirstOrDefault();
                if (firstPortfolio is not null)
                    mentor.CurrentPortfolioId = firstPortfolio.Id;
                else
                {
                    var result =
                        (await _portfolioRepository.CreatePortfolioAsync(mentor.Id, "My Portfolio"))
                        .Data as Portfolio;
                    mentor.CurrentPortfolioId = result.Id;
                    currentPortfolio = result;
                }
            }

            var accessToken = await CreateAccessTokenAsync(mentor);
            AccessToken userToken = new AccessToken
            {
                MentorId = mentor.Id,
                Mentor = mentor,
                Token = accessToken.Token,
                CreatedDate = DateTime.Now,
                IpAddress = mentor.IpAddress
            };
            _context.Mentors.Update(mentor);
            await _context.AccessTokens.AddAsync(userToken);
            await _context.SaveChangesAsync();
            var userLoginCommand = new MentorAndTokenDto()
            {
                Mentor = _mapper.Map<MentorDto>(mentor),
                Token = userToken.Token,
                TokenId = userToken.Id,
                MentorId = mentor.Id,
                IpAddress = _ipAddress,
                CreatedDate = DateTime.Now
            };
            userLoginCommand.Mentor.Portfolio = _mapper.Map<PortfolioDto>(currentPortfolio);
            userLoginCommand.Mentor.CurrentPortfolioId = mentor.CurrentPortfolioId;
            return Result<MentorAndTokenDto>.Success(userLoginCommand);
        }

        return Result<MentorAndTokenDto>.Fail("Lütfen bilgilerinizi kontrol ediniz.");
    }

    public async Task<IResult<MentorAndTokenDto>> LoginWithUserNameAsync(
        MentorLoginWithUserNameCommand loginWithUserNameCommand)
    {
        //ValidationTool.Validate(new LoginWithUserNameCommandValidator(), loginWithUserNameCommand);
        //.Include(a => a.Portfolios).ThenInclude(c => c.PortfolioTokens)

        var mentor = await _context.Mentors.SingleOrDefaultAsync(a => a.UserName == loginWithUserNameCommand.UserName);
        if (mentor is null)
            return Result<MentorAndTokenDto>.Fail(Messages.General.NotFoundArgument("kullanıcı"));

        if (HashingHelper.VerifyPasswordHash(loginWithUserNameCommand.Password, mentor.PasswordHash,
                mentor.PasswordSalt))
        {
            if (!mentor.IsActive)
                return Result<MentorAndTokenDto>.Fail("Hesabınızı Aktif Etmek İçin Destek ile İletişime Geçiniz.");
            if (!mentor.IsEmailAddressVerified)
                return Result<MentorAndTokenDto>.Fail(
                    "Hesabınızı Aktif Etmek İçin Mailinizi Doğrulamanız Gerekmektedir.");

            mentor.LastLogin = DateTime.Now;
            mentor.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            var currentPortfolio = mentor.Portfolios.SingleOrDefault(a => a.Id == mentor.CurrentPortfolioId);

            if (currentPortfolio is null)
            {
                var firstPortfolio = mentor.Portfolios.FirstOrDefault();
                if (firstPortfolio is not null)
                    mentor.CurrentPortfolioId = firstPortfolio.Id;
                else
                {
                    var result =
                        (await _portfolioRepository.CreatePortfolioAsync(mentor.Id, "My Portfolio"))
                        .Data as Portfolio;
                    mentor.CurrentPortfolioId = result.Id;
                    currentPortfolio = result;
                }
            }


            var accessToken = await CreateAccessTokenAsync(mentor);
            AccessToken userToken = new AccessToken
            {
                MentorId = mentor.Id,
                Mentor = mentor,
                Token = accessToken.Token,
                CreatedDate = DateTime.Now,
                IpAddress = mentor.IpAddress
            };
            _context.Mentors.Update(mentor);
            await _context.AccessTokens.AddAsync(userToken);
            await _context.SaveChangesAsync();

            var userLoginCommand = new MentorAndTokenDto
            {
                Mentor = _mapper.Map<MentorDto>(mentor),
                Token = userToken.Token,
                TokenId = accessToken.Id,
                MentorId = mentor.Id,
                IpAddress = _ipAddress,
                CreatedDate = DateTime.Now
            };
            userLoginCommand.Mentor.Portfolio = _mapper.Map<PortfolioDto>(currentPortfolio);
            userLoginCommand.Mentor.CurrentPortfolioId = mentor.CurrentPortfolioId;
            return Result<MentorAndTokenDto>.Success(userLoginCommand);
        }

        return Result<MentorAndTokenDto>.Fail("Lütfen bilgilerinizi kontrol ediniz.");
    }

    public async Task<IResult> RegisterAsync(MentorRegisterCommand registerCommand)
    {
        //ValidationTool.Validate(new RegisterCommandValidator(), registerCommand);
        //var formattedString = String.Format("dd/MM/yyyy", registerCommand.Birth);
        if (await _context.Mentors.SingleOrDefaultAsync(a =>
                a.PhoneNumber == registerCommand.PhoneNumber || a.UserName == registerCommand.UserName ||
                a.EmailAddress == registerCommand.EmailAddress) is not null)
            return Result.Fail("Bu kullanıcı mevcut");
        //var userVerifyCheck = await _context.VerificationCodes.SingleOrDefaultAsync(a => a.EmailAddress == registerCommand.EmailAddress && a.PhoneNumber == registerCommand.PhoneNumber);
        var mentorVerifyCheck =
            await _context.VerificationCodes.SingleOrDefaultAsync(a =>
                a.EmailAddress == registerCommand.EmailAddress);
        if (mentorVerifyCheck is null)
            return Result.Fail("Böyle bir kayıt bulunamadı.");

        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(registerCommand.Password, out passwordHash, out passwordSalt);
        var mentor = _mapper.Map<Mentor>(registerCommand);
        mentor.Birthday = DateTime.Parse(registerCommand.Birth, new CultureInfo("es-ES"));
        mentor.UserName = mentor.UserName.ToLower();
        mentor.PasswordHash = passwordHash;
        mentor.PasswordSalt = passwordSalt;
        mentor.IsEmailAddressVerified = true;
        mentor.IsPhoneNumberVerified = true;
        mentor.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
        mentor.CreatedDate = DateTime.Now;
        mentor.IsMentorVerified = false;
        mentor.IsActive = true;
        var accessToken = await CreateAccessTokenAsync(mentor);

        await _context.Mentors.AddAsync(mentor);
        Portfolio portfolio = new Portfolio()
        {
            Mentor = mentor,
            MentorId = mentor.Id,
            TotalPrice = 0,
            TotalPriceUser24Hour = 0,
            TotalPriceChange = 0,
            TotalPriceChangePercent = 0,
            Name = "My Portfolio",
            PortfolioTokens = new List<PortfolioToken>()
        };
        await _portfolioRepository.AddAsync(portfolio);
        mentor.Portfolios.Add(portfolio);
        mentor.CurrentPortfolioId = portfolio.Id;

        AccessToken mentorToken = new AccessToken
        {
            MentorId = mentor.Id,
            Mentor = mentor,
            Token = accessToken.Token,
            CreatedDate = DateTime.Now,
            IpAddress = mentor.IpAddress
        };
        await _context.AccessTokens.AddAsync(mentorToken);
        UserAndOperationClaim userOperationClaim = new UserAndOperationClaim
        {
            UserId = mentor.Id,
            //Burası statik verilecek!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            OperationClaimId = Guid.NewGuid()
        };
        await _context.UserAndOperationClaims.AddAsync(userOperationClaim);
        var pictures = await _context.ProfilePictures.ToListAsync();
        if (pictures.Count > 0)
        {
            var randomPicId = pictures[0].Id;
            if (pictures.Count != 1)
                randomPicId = pictures[(Random.Shared.Next(0, pictures.Count))].Id;
            var randomPic = pictures.SingleOrDefault(a => a.Id == randomPicId);
            mentor.ProfilePicture = randomPic;
            mentor.ProfilePictureId = randomPicId;
            //await _userProfilePictureService.AddAsync(user.Id, randomPic.Id);
        }

        var userAndTokenDto = new MentorAndTokenDto
        {
            Mentor = _mapper.Map<MentorDto>(mentor),
            Token = mentorToken.Token,
            IpAddress = _ipAddress,
            CreatedDate = DateTime.Now,
            TokenId = accessToken.Id,
            MentorId = mentor.Id,
        };
        userAndTokenDto.Mentor.Portfolio = _mapper.Map<PortfolioDto>(portfolio);
        userAndTokenDto.Mentor.CurrentPortfolioId = portfolio.Id;
        await _context.SaveChangesAsync();
        return Result.Success($"Hoşgeldiniz Sayın {mentor.FirstName} {mentor.LastName}.", userAndTokenDto);
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
        };

        await _context.VerificationCodes.AddAsync(verificationCode);
        await _context.SaveChangesAsync();
        return Result.Success("Kod eşleşmesi başarıyla oluşturuldu.");
    }
}