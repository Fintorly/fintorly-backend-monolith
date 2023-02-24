using System.Globalization;
using AutoMapper;
using Fintorly.Application.Dtos.MentorDtos;
using Fintorly.Application.Dtos.PortfolioDtos;
using Fintorly.Application.Dtos.UserDtos;
using Fintorly.Application.Features.Commands.AuthCommands;
using Fintorly.Application.Features.Commands.EmailCommands;
using Fintorly.Application.Features.Queries.AuthQueries;
using Fintorly.Application.Interfaces.Utils;
using Fintorly.Domain.Common;
using Fintorly.Domain.ConfigureEntities;
using Fintorly.Domain.Entities;
using Fintorly.Domain.Enums;
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

    IMapper _mapper;

    //IUserProfilePictureService _userProfilePictureService;
    IPortfolioRepository _portfolioRepository;
    private string _ipAddress;

    public MentorAuthRepository(IMapper mapper, FintorlyContext context, IJwtHelper jwtHelper,
        IHttpContextAccessor httpContextAccessor,
        IPortfolioRepository portfolioRepository) : base(context)
    {
        _jwtHelper = jwtHelper;
        _portfolioRepository = portfolioRepository;
        _mapper = mapper;
        _context = context;
        // _userProfilePictureService = userProfilePictureService;
        _ipAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
    }

    public async Task<IResult> CheckCodeIsTrueByPhoneAsync(
        CheckCodeIsTrueByPhoneNumberQuery codeIsTrueByPhoneNumberQuery)
    {
        var mentor = await _context.Mentors.SingleOrDefaultAsync(a =>
            a.PhoneNumber == codeIsTrueByPhoneNumberQuery.PhoneNumber);
        if (mentor is null)
            return await Result.FailAsync("Böyle bir kullanıcı bulunamadı");

        var codeVerify =
            await _context.VerificationCodes.SingleOrDefaultAsync(a =>
                a.PhoneNumber == codeIsTrueByPhoneNumberQuery.PhoneNumber);
        if (codeVerify is null)
            return await Result.FailAsync("Bu Telefon Numarasına ait bir doğrulama bilgisi bulunamadı.", ResultStatus.Warning);
        if (codeVerify.PhoneCode != codeIsTrueByPhoneNumberQuery.VerificationCode)
            return await Result.FailAsync("Doğrulama Kodu Doğru değil..", ResultStatus.Warning);
        if (codeVerify.VerificationCodeValidDate < DateTime.Now)
            return await Result.FailAsync("Doğrulama kodu süresi geçmiştir lütfen tekrar deneyiniz", ResultStatus.Warning);

        return await Result.SuccessAsync("Doğrulama Başarılı.");
    }

    public async Task<IResult> CheckCodeIsTrueByEmailAsync(
        CheckCodeIsTrueByEmailAddressQuery codeIsTrueByEmailAddressQuery)
    {
        var mentor = await _context.Mentors.SingleOrDefaultAsync(a =>
            a.EmailAddress == codeIsTrueByEmailAddressQuery.EmailAddress);
        if (mentor is null)
            return await Result.FailAsync("Böyle bir mentör bulunamadı");

        var codeVerify = await _context.VerificationCodes.SingleOrDefaultAsync(a =>
            a.EmailAddress == codeIsTrueByEmailAddressQuery.EmailAddress);
        if (codeVerify is null)
            return await Result.FailAsync("Bu Maile ait bir doğrulama bilgisi bulunamadı.", ResultStatus.Warning);
        if (codeVerify.MailCode != codeIsTrueByEmailAddressQuery.VerificationCode)
            return await Result.FailAsync("Doğrulama Kodu Doğru değil..", ResultStatus.Warning);
        if (codeVerify.VerificationCodeValidDate < DateTime.Now)
            return await Result.FailAsync("Doğrulama kodu süresi geçmiştir lütfen tekrar deneyiniz", ResultStatus.Warning);

        return await Result.SuccessAsync("Doğrulama Başarılı.");
    }


    public async Task<IResult> ChangePasswordAsync(ChangePasswordCommand changePasswordCommand)
    {
        var mentor = await _context.Mentors.SingleOrDefaultAsync(a => a.Id == changePasswordCommand.UserId);
        if (mentor is null)
            return await Result.FailAsync("Böyle bir kullanıcı bulunamadı.");

        if (changePasswordCommand.NewPassword != changePasswordCommand.ReTypePassword)
            return await Result.FailAsync(ResultStatus.Warning);
        var isPasswordTrue = HashingHelper.VerifyPasswordHash(changePasswordCommand.Password, mentor.PasswordHash,
            mentor.PasswordSalt);
        if (!isPasswordTrue)
            return await Result.FailAsync(ResultStatus.Warning);

        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(changePasswordCommand.NewPassword, out passwordHash, out passwordSalt);
        mentor.PasswordHash = passwordHash;
        mentor.PasswordSalt = passwordSalt;
        mentor.ModifiedDate = DateTime.Now;
        _context.Update(mentor);
        await _context.SaveChangesAsync();
        return await Result.SuccessAsync();
    }

    public async Task<IResult> ForgotPasswordEmailAsync(
        ForgotPasswordEmailCommand changePasswordEmailCommand)
    {
        var mentor = await _context.Mentors.SingleOrDefaultAsync(a =>
            a.EmailAddress == changePasswordEmailCommand.EmailAddress);
        if (mentor is null)
            return await Result.FailAsync(Messages.General.NotFoundArgument("kullanıcı"));
        if (changePasswordEmailCommand.Password != changePasswordEmailCommand.ReTypePassword)
            return await Result.FailAsync("Şifreler aynı değil.", ResultStatus.Warning);

        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(changePasswordEmailCommand.Password, out passwordHash,
            out passwordSalt);
        mentor.PasswordHash = passwordHash;
        mentor.PasswordSalt = passwordSalt;
        mentor.ModifiedDate = DateTime.Now;
        _context.Mentors.Update(mentor);
        await _context.SaveChangesAsync();
        return await Result.SuccessAsync("Şifre başarıyla değiştirildi.");
    }

    public async Task<IResult> ForgotPasswordPhoneAsync(ForgotPasswordPhoneCommand changePasswordCommand)
    {
        var mentor = await _context.Mentors.SingleOrDefaultAsync(a =>
            a.PhoneNumber == changePasswordCommand.PhoneNumber);
        if (mentor is null)
            return await Result.FailAsync(Messages.General.NotFoundArgument("kullanıcı"));
        if (changePasswordCommand.Password != changePasswordCommand.ReTypePassword)
            return await Result.FailAsync("Şifreler aynı değil.", ResultStatus.Warning);

        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(changePasswordCommand.Password, out passwordHash, out passwordSalt);
        mentor.PasswordHash = passwordHash;
        mentor.PasswordSalt = passwordSalt;
        mentor.ModifiedDate = DateTime.Now;
        _context.Mentors.Update(mentor);
        await _context.SaveChangesAsync();

        return await Result.SuccessAsync("Şifre başarıyla değiştirildi.");
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

    public async Task<IResult<UserAndTokenDto>> LoginWithEmailAsync(LoginWithMailCommand loginWithMailCommand)
    {
        //ValidationTool.Validate(new LoginWithMailCommandValidator(), loginWithMailCommand);
        //Include(a => a.Portfolios).ThenInclude(c => c.PortfolioTokens)
        var mentor = await _context.Mentors.Include(a => a.Portfolios).ThenInclude(c => c.PortfolioTokens).ThenInclude(a=>a.PortfolioTransactions).SingleOrDefaultAsync(a =>
            a.EmailAddress == loginWithMailCommand.EmailAddress);

        if (mentor is null)
            return await Result<UserAndTokenDto>.FailAsync(Messages.General.NotFoundArgument("mentör"));

        if (!mentor.IsEmailAddressVerified)
            return await Result<UserAndTokenDto>.FailAsync("Mail adresinizi doğrulayın.", ResultStatus.Warning);

        if (HashingHelper.VerifyPasswordHash(loginWithMailCommand.Password, mentor.PasswordHash, mentor.PasswordSalt))
        {
            if (!mentor.IsActive)
                return await Result<UserAndTokenDto>.FailAsync("Hesabınızı Aktif Etmek İçin Destek ile İletişime Geçiniz.",
                    ResultStatus.Warning);
            if (!mentor.IsEmailAddressVerified)
                return await Result<UserAndTokenDto>.FailAsync(
                    "Hesabınızı Aktif Etmek İçin Mailinizi Doğrulamanız Gerekmektedir.", ResultStatus.Warning);

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
                    var result = (await _portfolioRepository.CreatePortfolioAsync(mentor)).Data as Portfolio;

                    mentor.CurrentPortfolioId = result.Id;
                    currentPortfolio = result;
                }
            }

            var accessToken = await AccessTokenAddAsync(mentor);

            _context.Mentors.Update(mentor);
            await _context.AccessTokens.AddAsync(accessToken);
            await _context.SaveChangesAsync();
            var userLoginCommand = new UserAndTokenDto()
            {
                TokenId = accessToken.Id,
                Token = accessToken.Token,
                UserId = mentor.Id,
                User = _mapper.Map<UserDto>(mentor),
                UserType = UserType.Mentor,
                CreatedDate = DateTime.Now,
            };
            userLoginCommand.User.Portfolio = _mapper.Map<PortfolioDto>(currentPortfolio);
            userLoginCommand.User.CurrentPortfolioId = mentor.CurrentPortfolioId;

            return await Result<UserAndTokenDto>.SuccessAsync(userLoginCommand);
        }

        return await Result<UserAndTokenDto>.FailAsync(ResultStatus.Warning);
    }

    public async Task<IResult<UserAndTokenDto>> LoginWithPhoneAsync(LoginWithPhoneCommand loginWithPhoneCommand)
    {
        //ValidationTool.Validate(new LoginWithPhoneCommandValidator(), loginWithPhoneCommand);

        var mentor = await _context.Mentors.Include(a => a.Portfolios).ThenInclude(c => c.PortfolioTokens).ThenInclude(a=>a.PortfolioTransactions)
            .SingleOrDefaultAsync(
                a => a.PhoneNumber == loginWithPhoneCommand.PhoneNumber);
        if (mentor is null)
            return await Result<UserAndTokenDto>.FailAsync(Messages.General.NotFoundArgument("kullanıcı"));

        if (HashingHelper.VerifyPasswordHash(loginWithPhoneCommand.Password, mentor.PasswordHash, mentor.PasswordSalt))
        {
            if (!mentor.IsActive)
                return await Result<UserAndTokenDto>.FailAsync("Hesabınızı Aktif Etmek İçin Destek ile İletişime Geçiniz.",
                    ResultStatus.Warning);
            if (!mentor.IsEmailAddressVerified)
                return await Result<UserAndTokenDto>.FailAsync(
                    "Hesabınızı Aktif Etmek İçin Mailinizi Doğrulamanız Gerekmektedir.", ResultStatus.Warning);
            if (!mentor.IsPhoneNumberVerified)
                //return Result.(ResultStatus.Error, "Hesabınızı Aktif Etmek İçin Telefon Numaranızı Doğrulamanız Gerekmektedir.");
                mentor.LastLogin = DateTime.Now;
            mentor.IpAddress = _ipAddress;

            var currentPortfolio = mentor.Portfolios.SingleOrDefault(a => a.Id == mentor.CurrentPortfolioId);

            if (currentPortfolio is null)
            {
                var firstPortfolio = mentor.Portfolios.FirstOrDefault();
                if (firstPortfolio is not null)
                    mentor.CurrentPortfolioId = firstPortfolio.Id;
                else
                {
                    var result = (await _portfolioRepository.CreatePortfolioAsync(mentor)).Data as Portfolio;

                    mentor.CurrentPortfolioId = result.Id;
                    currentPortfolio = result;
                }
            }

            var accessToken = await AccessTokenAddAsync(mentor);
            _context.Mentors.Update(mentor);
            await _context.AccessTokens.AddAsync(accessToken);
            await _context.SaveChangesAsync();
            var userLoginCommand = new UserAndTokenDto()
            {
                User = _mapper.Map<UserDto>(mentor),
                Token = accessToken.Token,
                TokenId = accessToken.Id,
                UserId = mentor.Id,
                UserType = UserType.Mentor,
                CreatedDate = DateTime.Now
            };
            userLoginCommand.User.Portfolio = _mapper.Map<PortfolioDto>(currentPortfolio);
            userLoginCommand.User.CurrentPortfolioId = mentor.CurrentPortfolioId;
            return await Result<UserAndTokenDto>.SuccessAsync(userLoginCommand);
        }

        return await Result<UserAndTokenDto>.FailAsync("Lütfen bilgilerinizi kontrol ediniz.", ResultStatus.Warning);
    }

    public async Task<IResult<UserAndTokenDto>> LoginWithUserNameAsync(
        LoginWithUserNameCommand loginWithUserNameCommand)
    {
        //ValidationTool.Validate(new LoginWithUserNameCommandValidator(), loginWithUserNameCommand);
        //.Include(a => a.Portfolios).ThenInclude(c => c.PortfolioTokens).ThenInclude(a=>a.PortfolioTransactions)
        var mentor = await _context.Mentors.Include(a => a.Portfolios).ThenInclude(c => c.PortfolioTokens).ThenInclude(a=>a.PortfolioTransactions).SingleOrDefaultAsync(a => a.UserName == loginWithUserNameCommand.UserName);
        if (mentor is null)
            return await Result<UserAndTokenDto>.FailAsync(Messages.General.NotFoundArgument("kullanıcı"));

        if (HashingHelper.VerifyPasswordHash(loginWithUserNameCommand.Password, mentor.PasswordHash,
                mentor.PasswordSalt))
        {
            if (!mentor.IsActive)
                return await Result<UserAndTokenDto>.FailAsync("Hesabınızı Aktif Etmek İçin Destek ile İletişime Geçiniz.",
                    ResultStatus.Warning);
            if (!mentor.IsEmailAddressVerified)
                return await Result<UserAndTokenDto>.FailAsync(
                    "Hesabınızı Aktif Etmek İçin Mailinizi Doğrulamanız Gerekmektedir.", ResultStatus.Warning);

            mentor.LastLogin = DateTime.Now;
            mentor.IpAddress = _ipAddress;

            var currentPortfolio = mentor.Portfolios.SingleOrDefault(a => a.Id == mentor.CurrentPortfolioId);

            if (currentPortfolio is null)
            {
                var firstPortfolio = mentor.Portfolios.FirstOrDefault();
                if (firstPortfolio is not null)
                    mentor.CurrentPortfolioId = firstPortfolio.Id;
                else
                {
                    var result = (await _portfolioRepository.CreatePortfolioAsync(mentor)).Data as Portfolio;
                    mentor.CurrentPortfolioId = result.Id;
                    currentPortfolio = result;
                }
            }

            var accessToken = await AccessTokenAddAsync(mentor);
            _context.Mentors.Update(mentor);
            await _context.AccessTokens.AddAsync(accessToken);
            await _context.SaveChangesAsync();

            var userLoginCommand = new UserAndTokenDto
            {
                User = _mapper.Map<UserDto>(mentor),
                Token = accessToken.Token,
                TokenId = accessToken.Id,
                UserId = mentor.Id,
                UserType = UserType.Mentor,
                CreatedDate = DateTime.Now
            };
            userLoginCommand.User.Portfolio = _mapper.Map<PortfolioDto>(currentPortfolio);
            userLoginCommand.User.CurrentPortfolioId = mentor.CurrentPortfolioId;
            return await Result<UserAndTokenDto>.SuccessAsync(userLoginCommand);
        }

        return await Result<UserAndTokenDto>.FailAsync("Lütfen bilgilerinizi kontrol ediniz.", ResultStatus.Warning);
    }

    public async Task<IResult<UserAndTokenDto>> RegisterAsync(RegisterCommand registerCommand)
    {
        //ValidationTool.Validate(new RegisterCommandValidator(), registerCommand);
        //var formattedString = String.Format("dd/MM/yyyy", registerCommand.Birth);
        if (await _context.Mentors.SingleOrDefaultAsync(a =>
                a.PhoneNumber == registerCommand.PhoneNumber || a.UserName == registerCommand.UserName ||
                a.EmailAddress == registerCommand.EmailAddress) is not null)
            return await Result<UserAndTokenDto>.FailAsync("Bu kullanıcı mevcut");
        //var userVerifyCheck = await _context.VerificationCodes.SingleOrDefaultAsync(a => a.EmailAddress == registerCommand.EmailAddress && a.PhoneNumber == registerCommand.PhoneNumber);
        var mentorVerifyCheck =
            await _context.VerificationCodes.SingleOrDefaultAsync(a =>
                a.EmailAddress == registerCommand.EmailAddress);
        if (mentorVerifyCheck is null)
            return await Result<UserAndTokenDto>.FailAsync("Böyle bir kayıt bulunamadı.");

        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(registerCommand.Password, out passwordHash, out passwordSalt);
        var mentor = _mapper.Map<Mentor>(registerCommand);
        mentor.Birthday = DateTime.Parse(registerCommand.Birth, new CultureInfo("es-ES"));
        mentor.UserName = mentor.UserName.ToLower();
        mentor.PasswordHash = passwordHash;
        mentor.PasswordSalt = passwordSalt;
        mentor.IsEmailAddressVerified = true;
        mentor.IsPhoneNumberVerified = true;
        mentor.IpAddress = _ipAddress;
        mentor.CreatedDate = DateTime.Now;
        mentor.IsMentorVerified = false;
        mentor.IsActive = true;
        await _context.Mentors.AddAsync(mentor);
        
        var accessToken = await AccessTokenAddAsync(mentor);

        await OperationClaimAddAsync(mentor);
        mentor = await PictureAddAsync(mentor);
        var portfolio = await PortfolioAddAsync(mentor);
        mentor.Portfolios.Add(portfolio);
        mentor.CurrentPortfolioId = portfolio.Id;
        mentor = await ApplicationRequestAddAsync(mentor);


        var userAndTokenDto = new UserAndTokenDto
        {
            User = _mapper.Map<UserDto>(mentor),
            Token = accessToken.Token,
            UserType = UserType.Mentor,
            CreatedDate = DateTime.Now,
            TokenId = accessToken.Id,
            UserId = mentor.Id,
        };

        userAndTokenDto.User.Portfolio = _mapper.Map<PortfolioDto>(portfolio);
        userAndTokenDto.User.CurrentPortfolioId = portfolio.Id;
        await _context.SaveChangesAsync();
        return await Result<UserAndTokenDto>.SuccessAsync($"Hoşgeldiniz Sayın {mentor.FirstName} {mentor.LastName}.",
            userAndTokenDto);
    }

    public async Task<AccessToken> CreateAccessTokenAsync(Mentor mentor)
    {
        var claims = await GetClaimsAsync(mentor);
        var accessToken = await _jwtHelper.CreateTokenAsync(mentor, claims, true);
        return accessToken;
    }

    private async Task<AccessToken> AccessTokenAddAsync(Mentor mentor)
    {
        var accessToken = await CreateAccessTokenAsync(mentor);
        AccessToken userToken = new AccessToken
        {
            MentorId = mentor.Id,
            Mentor = mentor,
            Token = accessToken.Token,
            CreatedDate = DateTime.Now,
            IpAddress = mentor.IpAddress
        };
        await _context.AccessTokens.AddAsync(userToken);
        return userToken;
    }

    private async Task<Portfolio> PortfolioAddAsync(Mentor mentor)
    {
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
        return portfolio;
    }

    private async Task OperationClaimAddAsync(Mentor mentor)
    {
        var operationClaims = await _context.OperationClaims.ToListAsync();
        MentorAndOperationClaim mentorAndOperationClaim = new MentorAndOperationClaim()
        {
            MentorId = mentor.Id,
            OperationClaimId = operationClaims.SingleOrDefault(a => a.Name == "Mentor")!.Id
        };
        await _context.MentorAndOperationClaims.AddAsync(mentorAndOperationClaim);
    }

    private async Task<Mentor> PictureAddAsync(Mentor mentor)
    {
        var pictures = await _context.ProfilePictures.Where(a => a.UserType == UserType.Mentor).ToListAsync();
        if (pictures.Count > 0)
        {
            var randomPicId = pictures[0].Id;
            if (pictures.Count != 1)
                randomPicId = pictures[(Random.Shared.Next(0, pictures.Count))].Id;
            var randomPic = pictures.SingleOrDefault(a => a.Id == randomPicId);
            mentor.ProfilePicture = randomPic;
            mentor.ProfilePictureId = randomPicId;
        }

        return mentor;
    }

    private async Task<Mentor> ApplicationRequestAddAsync(Mentor mentor)
    {
        var application = await _context.ApplicationRequests.SingleOrDefaultAsync(a => a.MentorId == mentor.Id);
        if (application == null)
        {
            ApplicationRequest newApplication = new ApplicationRequest()
            {
                Mentor = mentor,
                MentorId = mentor.Id,
                IpAddress = _ipAddress,
                CreatedDate = DateTime.Now,
                IsAccepted =false
            };
            mentor.ApplicationRequest = newApplication;
            mentor.ApplicationRequestId = newApplication.Id;
            await _context.ApplicationRequests.AddAsync(newApplication);
        }
        else
            application.ModifiedDate = DateTime.Now;

        return mentor;
    }
}