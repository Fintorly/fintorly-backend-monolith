using AutoMapper;
using Fintorly.Domain.ConfigureEntities;
using System.Globalization;
using Fintorly.Application.Dtos.PortfolioDtos;
using Fintorly.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Fintorly.Infrastructure.Context;
using Fintorly.Application.Interfaces.Utils;
using Fintorly.Domain.Common;
using Fintorly.Application.Features.Commands.AuthCommands;
using Fintorly.Domain.Utils;
using Fintorly.Application.Dtos.UserDtos;
using Fintorly.Application.Features.Commands.EmailCommands;
using Fintorly.Application.Features.Queries.AuthQueries;
using Fintorly.Domain.Enums;
using Fintorly.Infrastructure.Utilities;

namespace Fintorly.Infrastructure.Repositories
{
    public class UserAuthRepository : GenericRepository<User>, IUserAuthRepository
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

        public UserAuthRepository(IMapper mapper, FintorlyContext context, IJwtHelper jwtHelper,
            IMailService mailService,
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

        public async Task<IResult> CheckCodeIsTrueByPhoneAsync(
            CheckCodeIsTrueByPhoneNumberQuery codeIsTrueByPhoneNumberQuery)
        {
            var user = await _context.Users.SingleOrDefaultAsync(a =>
                a.PhoneNumber == codeIsTrueByPhoneNumberQuery.PhoneNumber);
            if (user is null)
                return Result.Fail("Böyle bir kullanıcı bulunamadı");

            var codeVerify =
                await _context.VerificationCodes.SingleOrDefaultAsync(a =>
                    a.PhoneNumber == codeIsTrueByPhoneNumberQuery.PhoneNumber);
            if (codeVerify is null)
                return Result.Fail("Bu Telefon Numarasına ait bir doğrulama bilgisi bulunamadı.", ResultStatus.Warning);
            if (codeVerify.PhoneCode != codeIsTrueByPhoneNumberQuery.VerificationCode)
                return Result.Fail("Doğrulama Kodu Doğru değil..", ResultStatus.Warning);
            if (codeVerify.VerificationCodeValidDate < DateTime.Now)
                return Result.Fail("Doğrulama kodu süresi geçmiştir lütfen tekrar deneyiniz", ResultStatus.Warning);

            return Result.Success("Doğrulama Başarılı.");
        }

        public async Task<IResult> CheckCodeIsTrueByEmailAsync(
            CheckCodeIsTrueByEmailAddressQuery codeIsTrueByEmailAddressQuery)
        {
            var user = await _context.Users.SingleOrDefaultAsync(a =>
                a.EmailAddress == codeIsTrueByEmailAddressQuery.EmailAddress);
            if (user is null)
                return Result.Fail("Böyle bir kullanıcı bulunamadı");

            var codeVerify = await _context.VerificationCodes.SingleOrDefaultAsync(a =>
                a.EmailAddress == codeIsTrueByEmailAddressQuery.EmailAddress);
            if (codeVerify is null)
                return Result.Fail("Bu Maile ait bir doğrulama bilgisi bulunamadı.", ResultStatus.Warning);
            if (codeVerify.MailCode != codeIsTrueByEmailAddressQuery.VerificationCode)
                return Result.Fail("Doğrulama Kodu Doğru değil..", ResultStatus.Warning);
            if (codeVerify.VerificationCodeValidDate < DateTime.Now)
                return Result.Fail("Doğrulama kodu süresi geçmiştir lütfen tekrar deneyiniz", ResultStatus.Warning);

            return Result.Success("Doğrulama Başarılı.");
        }

        public async Task<AccessToken> CreateAccessTokenAsync(User user)
        {
            var claims = await GetClaimsAsync(user);
            var accessToken = await _jwtHelper.CreateTokenAsync(user, claims, false);
            return accessToken;
        }

        public async Task<IResult> ChangePasswordAsync(ChangePasswordCommand userChangePasswordCommand)
        {
            var user = await _context.Users.SingleOrDefaultAsync(a => a.Id == userChangePasswordCommand.UserId);
            if (user is null)
                return Result.Fail("Böyle bir kullanıcı bulunamadı.");

            var isPasswordTrue = HashingHelper.VerifyPasswordHash(userChangePasswordCommand.Password, user.PasswordHash,
                user.PasswordSalt);
            if (!isPasswordTrue)
                return Result.Fail(ResultStatus.Warning);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userChangePasswordCommand.NewPassword, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.ModifiedDate = DateTime.Now;
            _context.Update(user);
            await _context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<IResult> ForgotPasswordEmailAsync(
            ForgotPasswordEmailCommand userChangePasswordEmailCommand)
        {
            var user = await _context.Users.SingleOrDefaultAsync(a =>
                a.EmailAddress == userChangePasswordEmailCommand.EmailAddress);
            if (user is null)
                return Result.Fail(Messages.General.NotFoundArgument("kullanıcı"));
            if (userChangePasswordEmailCommand.Password != userChangePasswordEmailCommand.ReTypePassword)
                return Result.Fail("Şifreler aynı değil.", ResultStatus.Warning);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userChangePasswordEmailCommand.Password, out passwordHash,
                out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.ModifiedDate = DateTime.Now;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return Result.Success("Şifre başarıyla değiştirildi.");
        }

        public async Task<IResult> ForgotPasswordPhoneAsync(ForgotPasswordPhoneCommand userChangePasswordCommand)
        {
            var user = await _context.Users.SingleOrDefaultAsync(a =>
                a.PhoneNumber == userChangePasswordCommand.PhoneNumber);
            if (user is null)
                return Result.Fail(Messages.General.NotFoundArgument("kullanıcı"));
            if (userChangePasswordCommand.Password != userChangePasswordCommand.ReTypePassword)
                return Result.Fail("Şifreler aynı değil.", ResultStatus.Warning);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userChangePasswordCommand.Password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.ModifiedDate = DateTime.Now;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Result.Success("Şifre başarıyla değiştirildi.");
        }

        public async Task<IEnumerable<OperationClaim>> GetClaimsAsync(User user)
        {
            var roles = await _context.OperationClaims.ToListAsync();
            var userRoles = _context.UserAndOperationClaims.Where(a => a.UserId == user.Id);
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
            var user = await _context.Users.SingleOrDefaultAsync(a =>
                a.EmailAddress == loginWithMailCommand.EmailAddress);

            if (user is null)
                return Result<UserAndTokenDto>.Fail(Messages.General.NotFoundArgument("kullanıcı"));

            if (HashingHelper.VerifyPasswordHash(loginWithMailCommand.Password, user.PasswordHash, user.PasswordSalt))
            {
                if (!user.IsActive)
                    return Result<UserAndTokenDto>.Fail("Hesabınızı Aktif Etmek İçin Destek ile İletişime Geçiniz.", ResultStatus.Warning);
                if (!user.IsEmailAddressVerified)
                    return Result<UserAndTokenDto>.Fail(
                        "Hesabınızı Aktif Etmek İçin Mailinizi Doğrulamanız Gerekmektedir.", ResultStatus.Warning);

                user.LastLogin = DateTime.Now;
                user.IpAddress = _ipAddress;

                //var currentPortfolio = user.Portfolios.SingleOrDefault(a => a.Id == user.CurrentPortfolioId);
                //if (currentPortfolio is null)
                //{
                //    currentPortfolio = user.Portfolios.FirstOrDefault();
                //    if (currentPortfolio is not null)
                //        user.CurrentPortfolioId = currentPortfolio.Id;
                //    else
                //    {
                //        var result =
                //            (await _portfolioRepository.CreatePortfolioAsync(user.Id, "My Portfolio"))
                //            .Data as Portfolio;
                //        user.CurrentPortfolioId = result.Id;
                //        currentPortfolio = result;
                //    }
                //}

                var accessToken = await CreateAccessTokenAsync(user);
                AccessToken userToken = new AccessToken
                {
                    UserId = user.Id,
                    Token = accessToken.Token,
                    CreatedDate = DateTime.Now,
                    IpAddress = user.IpAddress,
                    User = user
                };
                _context.Users.Update(user);
                await _context.AccessTokens.AddAsync(userToken);
                await _context.SaveChangesAsync();
                var userLoginCommand = new UserAndTokenDto
                {
                    TokenId = userToken.Id,
                    Token = userToken.Token,
                    UserId = user.Id,
                    User = _mapper.Map<UserDto>(user),
                    UserType = UserType.User,
                    CreatedDate = DateTime.Now,
                };
                //userLoginCommand.User.Portfolio = _mapper.Map<PortfolioDto>(currentPortfolio);
                //userLoginCommand.User.CurrentPortfolioId = user.CurrentPortfolioId;

                return Result<UserAndTokenDto>.Success(userLoginCommand);
            }

            return Result<UserAndTokenDto>.Fail(ResultStatus.Warning);
        }

        public async Task<IResult<UserAndTokenDto>> LoginWithPhoneAsync(LoginWithPhoneCommand loginWithPhoneCommand)
        {
            //ValidationTool.Validate(new LoginWithPhoneCommandValidator(), loginWithPhoneCommand);
            //Include(a => a.Portfolios).ThenInclude(c => c.PortfolioTokens).ThenInclude(a => a.PortfolioOrders)
            var user = await _context.Users.SingleOrDefaultAsync(
                a => a.PhoneNumber == loginWithPhoneCommand.PhoneNumber);
            if (user is null)
                return Result<UserAndTokenDto>.Fail(Messages.General.NotFoundArgument("kullanıcı"));

            if (HashingHelper.VerifyPasswordHash(loginWithPhoneCommand.Password, user.PasswordHash, user.PasswordSalt))
            {
                if (!user.IsActive)
                    return Result<UserAndTokenDto>.Fail("Hesabınızı Aktif Etmek İçin Destek ile İletişime Geçiniz.", ResultStatus.Warning);
                if (!user.IsEmailAddressVerified)
                    return Result<UserAndTokenDto>.Fail(
                        "Hesabınızı Aktif Etmek İçin Mailinizi Doğrulamanız Gerekmektedir.", ResultStatus.Warning);

                user.LastLogin = DateTime.Now;
                user.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                var currentPortfolio = user.Portfolios.SingleOrDefault(a => a.Id == user.CurrentPortfolioId);

                if (currentPortfolio is null)
                {
                    var firstPortfolio = user.Portfolios.FirstOrDefault();
                    if (firstPortfolio is not null)
                        user.CurrentPortfolioId = firstPortfolio.Id;
                    else
                    {
                        var result =
                            (await _portfolioRepository.CreatePortfolioAsync(user.Id, "My Portfolio"))
                            .Data as Portfolio;
                        user.CurrentPortfolioId = result.Id;
                        currentPortfolio = result;
                    }
                }

                var accessToken = await CreateAccessTokenAsync(user);
                AccessToken userToken = new AccessToken
                {
                    UserId = user.Id,
                    User = user,
                    Token = accessToken.Token,
                    CreatedDate = DateTime.Now,
                    IpAddress = user.IpAddress
                };
                _context.Users.Update(user);
                await _context.AccessTokens.AddAsync(userToken);
                await _context.SaveChangesAsync();
                var userLoginCommand = new UserAndTokenDto()
                {
                    User = _mapper.Map<UserDto>(user),
                    Token = userToken.Token,
                    TokenId = userToken.Id,
                    UserId = user.Id,
                    UserType = UserType.User,
                    CreatedDate = DateTime.Now
                };
                userLoginCommand.User.Portfolio = _mapper.Map<PortfolioDto>(currentPortfolio);
                userLoginCommand.User.CurrentPortfolioId = user.CurrentPortfolioId;
                return Result<UserAndTokenDto>.Success(userLoginCommand);
            }

            return Result<UserAndTokenDto>.Fail("Lütfen bilgilerinizi kontrol ediniz.", ResultStatus.Warning);
        }

        public async Task<IResult<UserAndTokenDto>> LoginWithUserNameAsync(
            LoginWithUserNameCommand loginWithUserNameCommand)
        {
            //ValidationTool.Validate(new LoginWithUserNameCommandValidator(), loginWithUserNameCommand);
            //.Include(a => a.Portfolios).ThenInclude(c => c.PortfolioTokens)

            var user = await _context.Users.SingleOrDefaultAsync(a => a.UserName == loginWithUserNameCommand.UserName);
            if (user is null)
                return Result<UserAndTokenDto>.Fail(Messages.General.NotFoundArgument("kullanıcı"));


            if (HashingHelper.VerifyPasswordHash(loginWithUserNameCommand.Password, user.PasswordHash,
                    user.PasswordSalt))
            {
                if (!user.IsActive)
                    return Result<UserAndTokenDto>.Fail("Hesabınızı Aktif Etmek İçin Destek ile İletişime Geçiniz.", ResultStatus.Warning);
                if (!user.IsEmailAddressVerified)
                    return Result<UserAndTokenDto>.Fail(
                        "Hesabınızı Aktif Etmek İçin Mailinizi Doğrulamanız Gerekmektedir.", ResultStatus.Warning);

                user.LastLogin = DateTime.Now;
                user.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                var currentPortfolio = user.Portfolios.SingleOrDefault(a => a.Id == user.CurrentPortfolioId);

                if (currentPortfolio is null)
                {
                    var firstPortfolio = user.Portfolios.FirstOrDefault();
                    if (firstPortfolio is not null)
                        user.CurrentPortfolioId = firstPortfolio.Id;
                    else
                    {
                        var result =
                            (await _portfolioRepository.CreatePortfolioAsync(user.Id, "My Portfolio"))
                            .Data as Portfolio;
                        user.CurrentPortfolioId = result.Id;
                        currentPortfolio = result;
                    }
                }


                var accessToken = await CreateAccessTokenAsync(user);
                AccessToken userToken = new AccessToken
                {
                    UserId = user.Id,
                    User = user,
                    Token = accessToken.Token,
                    CreatedDate = DateTime.Now,
                    IpAddress = user.IpAddress
                };
                _context.Users.Update(user);
                await _context.AccessTokens.AddAsync(userToken);
                await _context.SaveChangesAsync();

                var userLoginCommand = new UserAndTokenDto
                {
                    User = _mapper.Map<UserDto>(user),
                    Token = userToken.Token,
                    TokenId = accessToken.Id,
                    UserId = user.Id,
                    UserType = UserType.User,
                    CreatedDate = DateTime.Now
                };
                userLoginCommand.User.Portfolio = _mapper.Map<PortfolioDto>(currentPortfolio);
                userLoginCommand.User.CurrentPortfolioId = user.CurrentPortfolioId;
                return Result<UserAndTokenDto>.Success(userLoginCommand);
            }

            return Result<UserAndTokenDto>.Fail("Lütfen bilgilerinizi kontrol ediniz.", ResultStatus.Warning);
        }

        public async Task<IResult<UserAndTokenDto>> RegisterAsync(RegisterCommand registerCommand)
        {
            //ValidationTool.Validate(new RegisterCommandValidator(), registerCommand);
            //var formattedString = String.Format("dd/MM/yyyy", registerCommand.Birth);
            if (await _context.Users.SingleOrDefaultAsync(a =>
                    a.PhoneNumber == registerCommand.PhoneNumber || a.UserName == registerCommand.UserName ||
                    a.EmailAddress == registerCommand.EmailAddress) is not null)
                return Result<UserAndTokenDto>.Fail("Bu kullanıcı mevcut");
            //var userVerifyCheck = await _context.VerificationCodes.SingleOrDefaultAsync(a => a.EmailAddress == registerCommand.EmailAddress && a.PhoneNumber == registerCommand.PhoneNumber);
            var userVerifyCheck =
                await _context.VerificationCodes.SingleOrDefaultAsync(a =>
                    a.EmailAddress == registerCommand.EmailAddress);
            if (userVerifyCheck is null)
                return Result<UserAndTokenDto>.Fail("Böyle bir kayıt bulunamadı.", ResultStatus.Warning);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(registerCommand.Password, out passwordHash, out passwordSalt);
            var user = _mapper.Map<User>(registerCommand);
            user.Birthday = DateTime.Parse(registerCommand.Birth, new CultureInfo("es-ES"));
            user.UserName = user.UserName.ToLower();
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.IsEmailAddressVerified = userVerifyCheck.IsMailConfirmed;
            user.IsPhoneNumberVerified = userVerifyCheck.IsPhoneNumberConfirmed;
            user.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            user.CreatedDate = DateTime.Now;
            user.IsActive = true;
            var accessToken = await CreateAccessTokenAsync(user);
            user.Answers.ToList().AddRange(registerCommand.Answers);
            _context.Answers.AddRange(registerCommand.Answers);
            await _context.Users.AddAsync(user);
            Portfolio portfolio = new Portfolio()
            {
                User = user,
                UserId = user.Id,
                TotalPrice = 0,
                TotalPriceUser24Hour = 0,
                TotalPriceChange = 0,
                TotalPriceChangePercent = 0,
                Name = "My Portfolio",
                PortfolioTokens = new List<PortfolioToken>()
            };
            await _portfolioRepository.AddAsync(portfolio);
            user.Portfolios.Add(portfolio);
            user.CurrentPortfolioId = portfolio.Id;

            AccessToken userToken = new AccessToken
            {
                UserId = user.Id,
                User = user,
                Token = accessToken.Token,
                CreatedDate = DateTime.Now,
                IpAddress = user.IpAddress
            };
            await _context.AccessTokens.AddAsync(userToken);
            UserAndOperationClaim userOperationClaim = new UserAndOperationClaim
            {
                UserId = user.Id,
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
                user.ProfilePicture = randomPic;
                user.ProfilePictureId = randomPicId;
                //await _userProfilePictureService.AddAsync(user.Id, randomPic.Id);
            }

            var userAndTokenDto = new UserAndTokenDto
            {
                User = _mapper.Map<UserDto>(user),
                Token = userToken.Token,
                UserType = UserType.User,
                CreatedDate = DateTime.Now,
                TokenId = accessToken.Id,
                UserId = user.Id,
            };
            userAndTokenDto.User.Portfolio = _mapper.Map<PortfolioDto>(portfolio);
            userAndTokenDto.User.CurrentPortfolioId = portfolio.Id;
            await _context.SaveChangesAsync();
            return Result<UserAndTokenDto>.Success($"Hoşgeldiniz Sayın {user.FirstName} {user.LastName}.", userAndTokenDto);
        }
    }
}