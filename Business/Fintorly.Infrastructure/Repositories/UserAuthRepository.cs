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
        private readonly FintorlyContext _context;
        private readonly IJwtHelper _jwtHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProfilePictureRepository _profilePictureRepository;
        private readonly IMapper _mapper;

        //IUserProfilePictureService _userProfilePictureService;
        IPortfolioRepository _portfolioRepository;
        private string _ipAddress;

        public UserAuthRepository(IMapper mapper, FintorlyContext context, IJwtHelper jwtHelper,
            IHttpContextAccessor httpContextAccessor,
            IPortfolioRepository portfolioRepository, IProfilePictureRepository profilePictureRepository) : base(
            context)
        {
            _jwtHelper = jwtHelper;
            _httpContextAccessor = httpContextAccessor;
            _portfolioRepository = portfolioRepository;
            _profilePictureRepository = profilePictureRepository;
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
            var user = await _context.Users.SingleOrDefaultAsync(a =>
                a.EmailAddress == codeIsTrueByEmailAddressQuery.EmailAddress);
            if (user is null)
                return await Result.FailAsync("Böyle bir kullanıcı bulunamadı");

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
                return await Result.FailAsync("Böyle bir kullanıcı bulunamadı.");

            var isPasswordTrue = HashingHelper.VerifyPasswordHash(userChangePasswordCommand.Password, user.PasswordHash,
                user.PasswordSalt);
            if (!isPasswordTrue)
                return await Result.FailAsync(ResultStatus.Warning);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userChangePasswordCommand.NewPassword, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.ModifiedDate = DateTime.Now;
            _context.Update(user);
            await _context.SaveChangesAsync();
            return await Result.SuccessAsync();
        }

        public async Task<IResult> ForgotPasswordEmailAsync(
            ForgotPasswordEmailCommand userChangePasswordEmailCommand)
        {
            var user = await _context.Users.SingleOrDefaultAsync(a =>
                a.EmailAddress == userChangePasswordEmailCommand.EmailAddress);
            if (user is null)
                return await Result.FailAsync(Messages.General.NotFoundArgument("kullanıcı"));
            if (userChangePasswordEmailCommand.Password != userChangePasswordEmailCommand.ReTypePassword)
                return await Result.FailAsync("Şifreler aynı değil.", ResultStatus.Warning);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userChangePasswordEmailCommand.Password, out passwordHash,
                out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.ModifiedDate = DateTime.Now;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return await Result.SuccessAsync("Şifre başarıyla değiştirildi.");
        }

        public async Task<IResult> ForgotPasswordPhoneAsync(ForgotPasswordPhoneCommand userChangePasswordCommand)
        {
            var user = await _context.Users.SingleOrDefaultAsync(a =>
                a.PhoneNumber == userChangePasswordCommand.PhoneNumber);
            if (user is null)
                return await Result.FailAsync(Messages.General.NotFoundArgument("kullanıcı"));
            if (userChangePasswordCommand.Password != userChangePasswordCommand.ReTypePassword)
                return await Result.FailAsync("Şifreler aynı değil.", ResultStatus.Warning);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userChangePasswordCommand.Password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.ModifiedDate = DateTime.Now;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return await Result.SuccessAsync("Şifre başarıyla değiştirildi.");
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
            //.Include(a => a.Portfolios).ThenInclude(c => c.PortfolioTokens).ThenInclude(a=>a.PortfolioTransactions)
            var user = await _context.Users.Include(a => a.Portfolios).ThenInclude(c => c.PortfolioTokens).ThenInclude(a=>a.PortfolioTransactions).SingleOrDefaultAsync(a =>
                a.EmailAddress == loginWithMailCommand.EmailAddress);

            if (user is null)
                return await Result<UserAndTokenDto>.FailAsync(Messages.General.NotFoundArgument("kullanıcı"));

            if (HashingHelper.VerifyPasswordHash(loginWithMailCommand.Password, user.PasswordHash, user.PasswordSalt))
            {
                if (!user.IsActive)
                    return await Result<UserAndTokenDto>.FailAsync("Hesabınızı Aktif Etmek İçin Destek ile İletişime Geçiniz.",
                        ResultStatus.Warning);
                if (!user.IsEmailAddressVerified)
                    return await Result<UserAndTokenDto>.FailAsync(
                        "Hesabınızı Aktif Etmek İçin Mailinizi Doğrulamanız Gerekmektedir.", ResultStatus.Warning);

                user.LastLogin = DateTime.Now;
                user.IpAddress = _ipAddress;

                var currentPortfolio = user.Portfolios.SingleOrDefault(a => a.Id == user.CurrentPortfolioId);
                if (currentPortfolio is null)
                {
                    currentPortfolio = user.Portfolios.FirstOrDefault();
                    if (currentPortfolio is not null)
                        user.CurrentPortfolioId = currentPortfolio.Id;
                    else
                    {
                        var result = (await _portfolioRepository.CreatePortfolioAsync(user)).Data as Portfolio;
                        
                        user.CurrentPortfolioId = result.Id;
                        currentPortfolio = result;
                    }
                }

                var accessToken =await AccessTokenAddAsync(user);

                _context.Users.Update(user);
                await _context.AccessTokens.AddAsync(accessToken);
                await _context.SaveChangesAsync();
                var userLoginCommand = new UserAndTokenDto
                {
                    TokenId = accessToken.Id,
                    Token = accessToken.Token,
                    UserId = user.Id,
                    User = _mapper.Map<UserDto>(user),
                    UserType = UserType.User,
                    CreatedDate = DateTime.Now,
                };
                userLoginCommand.User.Portfolio = _mapper.Map<PortfolioDto>(currentPortfolio);
                userLoginCommand.User.CurrentPortfolioId = user.CurrentPortfolioId;

                return await Result<UserAndTokenDto>.SuccessAsync(userLoginCommand);
            }

            return await Result<UserAndTokenDto>.FailAsync(ResultStatus.Warning);
        }

        public async Task<IResult<UserAndTokenDto>> LoginWithPhoneAsync(LoginWithPhoneCommand loginWithPhoneCommand)
        {
            //ValidationTool.Validate(new LoginWithPhoneCommandValidator(), loginWithPhoneCommand);
            //.Include(a => a.Portfolios).ThenInclude(c => c.PortfolioTokens).ThenInclude(a=>a.PortfolioTransactions)
            var user = await _context.Users.Include(a => a.Portfolios).ThenInclude(c => c.PortfolioTokens).ThenInclude(a=>a.PortfolioTransactions).SingleOrDefaultAsync(
                a => a.PhoneNumber == loginWithPhoneCommand.PhoneNumber);
            if (user is null)
                return await Result<UserAndTokenDto>.FailAsync(Messages.General.NotFoundArgument("kullanıcı"));

            if (HashingHelper.VerifyPasswordHash(loginWithPhoneCommand.Password, user.PasswordHash, user.PasswordSalt))
            {
                if (!user.IsActive)
                    return await Result<UserAndTokenDto>.FailAsync("Hesabınızı Aktif Etmek İçin Destek ile İletişime Geçiniz.",
                        ResultStatus.Warning);
                if (!user.IsEmailAddressVerified)
                    return await Result<UserAndTokenDto>.FailAsync(
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
                        var result = (await _portfolioRepository.CreatePortfolioAsync(user)).Data as Portfolio;
                        
                        user.CurrentPortfolioId = result.Id;
                        currentPortfolio = result;
                    }
                }

                var accessToken =await AccessTokenAddAsync(user);

                _context.Users.Update(user);
                await _context.AccessTokens.AddAsync(accessToken);
                await _context.SaveChangesAsync();
                var userLoginCommand = new UserAndTokenDto()
                {
                    User = _mapper.Map<UserDto>(user),
                    Token = accessToken.Token,
                    TokenId = accessToken.Id,
                    UserId = user.Id,
                    UserType = UserType.User,
                    CreatedDate = DateTime.Now
                };
                userLoginCommand.User.Portfolio = _mapper.Map<PortfolioDto>(currentPortfolio);
                userLoginCommand.User.CurrentPortfolioId = user.CurrentPortfolioId;
                return await Result<UserAndTokenDto>.SuccessAsync(userLoginCommand);
            }

            return await Result<UserAndTokenDto>.FailAsync("Lütfen bilgilerinizi kontrol ediniz.", ResultStatus.Warning);
        }

        public async Task<IResult<UserAndTokenDto>> LoginWithUserNameAsync(
            LoginWithUserNameCommand loginWithUserNameCommand)
        {
            //ValidationTool.Validate(new LoginWithUserNameCommandValidator(), loginWithUserNameCommand);
            //.Include(a => a.Portfolios).ThenInclude(c => c.PortfolioTokens).ThenInclude(a=>a.PortfolioTransactions)

            var user = await _context.Users.Include(a => a.Portfolios).ThenInclude(c => c.PortfolioTokens).ThenInclude(a=>a.PortfolioTransactions).SingleOrDefaultAsync(a => a.UserName == loginWithUserNameCommand.UserName);
            if (user is null)
                return await Result<UserAndTokenDto>.FailAsync(Messages.General.NotFoundArgument("kullanıcı"));

            if (HashingHelper.VerifyPasswordHash(loginWithUserNameCommand.Password, user.PasswordHash,
                    user.PasswordSalt))
            {
                if (!user.IsActive)
                    return await Result<UserAndTokenDto>.FailAsync("Hesabınızı Aktif Etmek İçin Destek ile İletişime Geçiniz.",
                        ResultStatus.Warning);
                if (!user.IsEmailAddressVerified)
                    return await Result<UserAndTokenDto>.FailAsync(
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
                        var result = (await _portfolioRepository.CreatePortfolioAsync(user)).Data as Portfolio;

                        user.CurrentPortfolioId = result.Id;
                        currentPortfolio = result;
                    }
                }

                var accessToken =await AccessTokenAddAsync(user);
                _context.Users.Update(user);
                await _context.AccessTokens.AddAsync(accessToken);
                await _context.SaveChangesAsync();

                var userLoginCommand = new UserAndTokenDto
                {
                    User = _mapper.Map<UserDto>(user),
                    Token = accessToken.Token,
                    TokenId = accessToken.Id,
                    UserId = user.Id,
                    UserType = UserType.User,
                    CreatedDate = DateTime.Now
                };
                userLoginCommand.User.Portfolio = _mapper.Map<PortfolioDto>(currentPortfolio);
                userLoginCommand.User.CurrentPortfolioId = user.CurrentPortfolioId;
                return await Result<UserAndTokenDto>.SuccessAsync(userLoginCommand);
            }

            return await Result<UserAndTokenDto>.FailAsync("Lütfen bilgilerinizi kontrol ediniz.", ResultStatus.Warning);
        }

        public async Task<IResult<UserAndTokenDto>> RegisterAsync(RegisterCommand registerCommand)
        {
            //ValidationTool.Validate(new RegisterCommandValidator(), registerCommand);
            //var formattedString = String.Format("dd/MM/yyyy", registerCommand.Birth);
            if (await _context.Users.AnyAsync(a =>
                    a.PhoneNumber == registerCommand.PhoneNumber || a.UserName == registerCommand.UserName ||
                    a.EmailAddress == registerCommand.EmailAddress))
                return await Result<UserAndTokenDto>.FailAsync("Bu kullanıcı mevcut");
            //var userVerifyCheck = await _context.VerificationCodes.SingleOrDefaultAsync(a => a.EmailAddress == registerCommand.EmailAddress && a.PhoneNumber == registerCommand.PhoneNumber);
            var userVerifyCheck =
                await _context.VerificationCodes.SingleOrDefaultAsync(a =>
                    a.EmailAddress == registerCommand.EmailAddress);
            if (userVerifyCheck is null)
                return await Result<UserAndTokenDto>.FailAsync("Böyle bir kayıt bulunamadı.", ResultStatus.Warning);

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
            await _context.Users.AddAsync(user);

            var accessToken = await AccessTokenAddAsync(user);
            if (registerCommand.Answers.Count > 0)
            {
                var answers = _mapper.Map<List<Answer>>(registerCommand.Answers);
                foreach (var answer in answers)
                {
                    answer.User = user;
                    answer.UserId = user.Id;
                }

                user.Answers.ToList().AddRange(answers);
                _context.Answers.AddRange(answers);
            }

            await OperationClaimAddAsync(user);
            user = await PictureAddAsync(user);
            var portfolio = await PortfolioAddAsync(user);
            user.Portfolios.Add(portfolio);
            user.CurrentPortfolioId = portfolio.Id;

            await _context.SaveChangesAsync();
            var userAndTokenDto = new UserAndTokenDto
            {
                User = _mapper.Map<UserDto>(user),
                Token = accessToken.Token,
                UserType = UserType.User,
                CreatedDate = DateTime.Now,
                TokenId = accessToken.Id,
                UserId = user.Id,
            };
            userAndTokenDto.User.Portfolio = _mapper.Map<PortfolioDto>(portfolio);
            userAndTokenDto.User.CurrentPortfolioId = portfolio.Id;

            return await Result<UserAndTokenDto>.SuccessAsync($"Hoşgeldiniz Sayın {user.FirstName} {user.LastName}.",
                userAndTokenDto);
        }

        private async Task<AccessToken> AccessTokenAddAsync(User user)
        {
            var accessToken = await CreateAccessTokenAsync(user);
            AccessToken userToken = new AccessToken
            {
                UserId = user.Id,
                User = user,
                Token = accessToken.Token,
                CreatedDate = DateTime.Now,
                IpAddress = user.IpAddress
            };
            await _context.AccessTokens.AddAsync(userToken);
            return userToken;
        }

        private async Task<Portfolio> PortfolioAddAsync(User user)
        {
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
            return portfolio;
        }

        private async Task OperationClaimAddAsync(User user)
        {
            var operationClaims = await _context.OperationClaims.ToListAsync();
            UserAndOperationClaim userOperationClaim = new UserAndOperationClaim
            {
                UserId = user.Id,
                OperationClaimId = operationClaims.SingleOrDefault(a => a.Name == "User")!.Id
            };
            await _context.UserAndOperationClaims.AddAsync(userOperationClaim);
        }

        private async Task<User> PictureAddAsync(User user)
        {
            var pictures = await _context.ProfilePictures.ToListAsync();
            if (pictures.Count > 0)
            {
                var randomPicId = pictures[0].Id;
                if (pictures.Count != 1)
                    randomPicId = pictures[(Random.Shared.Next(0, pictures.Count))].Id;
                var randomPic = pictures.SingleOrDefault(a => a.Id == randomPicId);
                user.ProfilePicture = randomPic;
                user.ProfilePictureId = randomPicId;
            }
            return user;
        }
    }
}