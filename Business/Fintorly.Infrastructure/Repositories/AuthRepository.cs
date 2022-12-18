using System;
using AutoMapper;
using Fintorly.Domain.ConfigureEntities;
using System.Globalization;
using Fintorly.Domain.Entities;
using Fintorly.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Fintorly.Infrastructure.Context;
using Fintorly.Application.Interfaces.Utils;
using Fintorly.Domain.Common;
using Fintorly.Application.Features.Commands.AuthCommands;
using Fintorly.Domain.Utils;
using Fintorly.Application.Dtos.UserDtos;
using Fintorly.Application.Features.Commands.EmailCommands;

namespace Fintorly.Infrastructure.Repositories
{
    public class AuthRepository : GenericRepository<User>, IAuthRepository
    {
        FintorlyContext _context;
        IJwtHelper _jwtHelper;
        IMailService _mailService;
        IPhoneService _phoneService;
        IHttpContextAccessor _httpContextAccessor;
        IMapper _mapper;
        //IUserProfilePictureService _userProfilePictureService;
        //IPortfolioService _portfolioService;

        public AuthRepository(IMapper mapper, FintorlyContext context, IJwtHelper jwtHelper, IMailService mailService, IPhoneService phoneService, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _jwtHelper = jwtHelper;
            _mailService = mailService;
            _phoneService = phoneService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _context = context;
            // _userProfilePictureService = userProfilePictureService;
            // _portfolioService = portfolioService;
        }

        public async Task<IResult> ActiveEmailByActivationCodeAsync(UserEmailActiveCommand userEmailActiveCommand)
        {
            var verificationCode = await _context.VerificationCodes.SingleOrDefaultAsync(a => a.EmailAddress == userEmailActiveCommand.EmailAddress);
            if (verificationCode is null)
                return Result.Fail("Bu Mail adresi ile daha önce kayıt oluşturma işleminde bulunulmamıştır.");
            if (verificationCode.MailCode == userEmailActiveCommand.ActivationCode && verificationCode.VerificationCodeValidDate < DateTime.Now)
                return Result.Fail("Bu kodun geçerlilik süresi sona ermiştir");
            if (verificationCode.MailCode != userEmailActiveCommand.ActivationCode)
                return Result.Fail("Doğrulamada kodu doğru değildir.");
            verificationCode.IsMailConfirmed = true;
            _context.VerificationCodes.Update(verificationCode);
            await _context.SaveChangesAsync();
            return Result.Success($"{userEmailActiveCommand.EmailAddress} mail adresi başarıyla onaylanmıştır.");
        }

        public async Task<IResult> ActivePhoneByActivationCodeAsync(UserPhoneActiveCommand userPhoneActiveCommand)
        {
            //ValidationTool.Validate(new UserPhoneActiveCommandValidator(), userPhoneActiveCommand);

            var verificationCode = await _context.VerificationCodes.SingleOrDefaultAsync(a => a.PhoneNumber == userPhoneActiveCommand.PhoneNumber);
            if (verificationCode is null)
                return Result.Fail("Bu Telefon numarası ile daha önce kayıt oluşturma işleminde bulunulmamıştır.");
            if (verificationCode.PhoneCode == userPhoneActiveCommand.ActivationCode && verificationCode.VerificationCodeValidDate < DateTime.Now)
                return Result.Fail("Bu kodun geçerlilik süresi sona ermiştir");
            if (verificationCode.PhoneCode != userPhoneActiveCommand.ActivationCode)
                return Result.Fail("Doğrulamada kodu doğru değildir.");

            verificationCode.IsPhoneNumberConfirmed = true;
            _context.VerificationCodes.Update(verificationCode);
            await _context.SaveChangesAsync();
            return Result.Success($"{userPhoneActiveCommand.PhoneNumber} numaralı hesap başarıyla onaylanmıştır.");
        }

        public async Task<IResult> CheckCodeIsTrueByEmailAsync(string emailAddress, string code)
        {
            var user = await _context.Users.SingleOrDefaultAsync(a => a.EmailAddress == emailAddress);
            if (user is null)
                return Result.Fail("Böyle bir kullanıcı bulunamadı");

            var codeVerify = await _context.VerificationCodes.SingleOrDefaultAsync(a => a.EmailAddress == emailAddress);
            if (codeVerify is null)
                return Result.Fail("Bu Maile ait bir doğrulama bilgisi bulunamadı.");
            if (codeVerify.MailCode != code)
                return Result.Fail("Doğrulama Kodu Doğru değil..");
            if (codeVerify.VerificationCodeValidDate < DateTime.Now)
                return Result.Fail("Doğrulama kodu süresi geçmiştir lütfen tekrar deneyiniz");

            else
                return Result.Success("Doğrulama Başarılı.");
        }

        public async Task<IResult> CheckCodeIsTrueByPhoneAsync(string phoneNumber, string code)
        {
            var user = await _context.Users.SingleOrDefaultAsync(a => a.PhoneNumber == phoneNumber);
            if (user is null)
                return Result.Fail("Böyle bir kullanıcı bulunamadı");

            var codeVerify = await _context.VerificationCodes.SingleOrDefaultAsync(a => a.PhoneNumber == phoneNumber);
            if (codeVerify is null)
                return Result.Fail("Bu Telefon Numarasına ait bir doğrulama bilgisi bulunamadı.");
            if (codeVerify.PhoneCode != code)
                return Result.Fail("Doğrulama Kodu Doğru değil..");
            if (codeVerify.VerificationCodeValidDate < DateTime.Now)
                return Result.Fail("Doğrulama kodu süresi geçmiştir lütfen tekrar deneyiniz");
            else
                return Result.Success("Doğrulama Başarılı.");
        }

        public async Task<AccessToken> CreateAccessTokenAsync(User user)
        {
            var claims = await GetClaimsAsync(user);
            var accessToken = await _jwtHelper.CreateTokenAsync(user, claims);
            return accessToken;
        }

        public async Task<AccessToken> CreateAccessTokenAsync(Mentor mentor)
        {
            var accessToken = await _jwtHelper.CreateTokenAsync(mentor, null);
            return accessToken;
        }


        public async Task<IResult> UpdatePasswordAsync(UserChangePasswordCommand userChangePasswordCommand)
        {
            var user = await _context.Users.SingleOrDefaultAsync(a => a.Id == userChangePasswordCommand.UserId);
            if (user is null)
                return Result.Fail(Messages.General.NotFoundArgument("kullanıcı"));

            if (!HashingHelper.VerifyPasswordHash(userChangePasswordCommand.Password, user.PasswordHash, user.PasswordSalt))
                return Result.Fail("Girdiğiniz şifre eski şifreniz değil. Lütfen kontrol ediniz.");
            if (userChangePasswordCommand.NewPassword != userChangePasswordCommand.ReTypePassword)
                return Result.Fail("Şifreler aynı değil.");

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userChangePasswordCommand.NewPassword, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.ModifiedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return Result.Success("Şifre başarıyla değiştirildi.");
        }

        public async Task<IResult> ForgotPasswordEmailAsync(UserChangePasswordEmailCommand userChangePasswordEmailCommand)
        {
            var user = await _context.Users.SingleOrDefaultAsync(a => a.EmailAddress == userChangePasswordEmailCommand.EmailAddress);
            if (user is null)
                return Result.Fail(Messages.General.NotFoundArgument("kullanıcı"));
            if (userChangePasswordEmailCommand.Password != userChangePasswordEmailCommand.ReTypePassword)
                return Result.Fail("Şifreler aynı değil.");

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userChangePasswordEmailCommand.Password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.ModifiedDate = DateTime.Now;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return Result.Success("Şifre başarıyla değiştirildi.");
        }

        public async Task<IResult> ForgotPasswordPhoneAsync(UserChangePasswordPhoneCommand userChangePasswordCommand)
        {
            var user = await _context.Users.SingleOrDefaultAsync(a => a.PhoneNumber == userChangePasswordCommand.PhoneNumber);
            if (user is null)
                return Result.Fail(Messages.General.NotFoundArgument("kullanıcı"));
            if (userChangePasswordCommand.Password != userChangePasswordCommand.ReTypePassword)
                return Result.Fail("Şifreler aynı değil.");

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

        public async Task<IResult> LoginWithEmailAsync(LoginWithMailCommand loginWithMailCommand)
        {
            //ValidationTool.Validate(new LoginWithMailCommandValidator(), loginWithMailCommand);
            //Include(a => a.Portfolios).ThenInclude(c => c.PortfolioTokens)
            var user = await _context.Users.SingleOrDefaultAsync(a => a.EmailAddress == loginWithMailCommand.EmailAddress);

            if (user is null)
                return Result.Fail(Messages.General.NotFoundArgument("kullanıcı"));

            if (!user.IsEmailAddressVerified)
                return Result.Fail("Mail adresinizi doğrulayın.");

            if (HashingHelper.VerifyPasswordHash(loginWithMailCommand.Password, user.PasswordHash, user.PasswordSalt))
            {
                if (!user.IsActive)
                    return Result.Fail("Hesabınızı Aktif Etmek İçin Destek ile İletişime Geçiniz.");
                if (!user.IsEmailAddressVerified)
                    return Result.Fail("Hesabınızı Aktif Etmek İçin Mailinizi Doğrulamanız Gerekmektedir.");

                user.LastLogin = DateTime.Now;
                user.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                //var currentPortfolio = user.Portfolios.SingleOrDefault(a => a.Id == user.CurrentPortfolioId);
                //if (user.CurrentPortfolioId is 0 || currentPortfolio is null)
                //{
                //    var firstPortfolio = user.Portfolios.FirstOrDefault();
                //    if (firstPortfolio is not null)
                //        user.CurrentPortfolioId = firstPortfolio.Id;
                //    else if (user.CurrentPortfolioId == 0 || user.CurrentPortfolioId == null || currentPortfolio == null)
                //    {
                //        var result = (await _portfolioService.CreatePortfolioAsync(user.Id, "My Portfolio")).Data as PortfolioGetCommand;
                //        user.CurrentPortfolioId = result.Id;
                //        currentPortfolio = Mapper.Map<Portfolio>(result);
                //    }
                //}

                var accessToken = await CreateAccessTokenAsync(user);
                AccessToken userToken = new AccessToken
                {
                    UserId = user.Id,
                    Token = accessToken.Token,
                    //TokenExpiration = accessToken.TokenExpiration,
                    CreatedDate = DateTime.Now,
                    IpAddress = user.IpAddress
                };
                _context.Users.Update(user);
                await _context.AccessTokens.AddAsync(userToken);
                await _context.SaveChangesAsync();
                var userLoginCommand = new
                {
                    User = _mapper.Map<UserDto>(user),
                    Token = _mapper.Map<UserAndTokenDto>(userToken),
                };
                //userLoginCommand.User.PortfolioGetCommand = Mapper.Map<PortfolioGetCommand>(currentPortfolio);
                //userLoginCommand.User.CurrentPortfolioId = user.CurrentPortfolioId;

                return Result.Success(userLoginCommand);
            }
            return Result.Success("Lütfen bilgilerinizi kontrol ediniz.");
        }

        public async Task<IResult> LoginWithPhoneAsync(LoginWithPhoneCommand loginWithPhoneCommand)
        {
            //ValidationTool.Validate(new LoginWithPhoneCommandValidator(), loginWithPhoneCommand);
            //Include(a => a.Portfolios).ThenInclude(c => c.PortfolioTokens).ThenInclude(a => a.PortfolioOrders)
            var user = await _context.Users.SingleOrDefaultAsync(a => a.PhoneNumber == loginWithPhoneCommand.PhoneNumber);
            if (user is null)
                return Result.Fail(Messages.General.NotFoundArgument("kullanıcı"));

            if (HashingHelper.VerifyPasswordHash(loginWithPhoneCommand.Password, user.PasswordHash, user.PasswordSalt))
            {
                if (!user.IsActive)
                    return Result.Fail("Hesabınızı Aktif Etmek İçin Destek ile İletişime Geçiniz.");
                if (!user.IsEmailAddressVerified)
                    return Result.Fail("Hesabınızı Aktif Etmek İçin Mailinizi Doğrulamanız Gerekmektedir.");
                if (!user.IsPhoneNumberVerified)
                    //return Result.(ResultStatus.Error, "Hesabınızı Aktif Etmek İçin Telefon Numaranızı Doğrulamanız Gerekmektedir.");
                    user.LastLogin = DateTime.Now;
                user.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                //var currentPortfolio = user.Portfolios.SingleOrDefault(a => a.Id == user.CurrentPortfolioId);

                //if (user.CurrentPortfolioId is 0 || currentPortfolio is null)
                //{
                //    var firstPortfolio = user.Portfolios.FirstOrDefault();
                //    if (firstPortfolio is not null)
                //        user.CurrentPortfolioId = firstPortfolio.Id;
                //    else if (user.CurrentPortfolioId == 0 || user.CurrentPortfolioId == null || currentPortfolio == null)
                //    {
                //        var result = (await _portfolioService.CreatePortfolioAsync(user.Id, "My Portfolio")).Data as PortfolioGetCommand;
                //        user.CurrentPortfolioId = result.Id;
                //        currentPortfolio = Mapper.Map<Portfolio>(result);
                //    }
                //}

                var accessToken = await CreateAccessTokenAsync(user);
                AccessToken userToken = new AccessToken
                {
                    UserId = user.Id,
                    Token = accessToken.Token,
                    CreatedDate = DateTime.Now,
                    IpAddress = user.IpAddress
                };
                _context.Users.Update(user);
                await _context.AccessTokens.AddAsync(userToken);
                await _context.SaveChangesAsync();

                var userLoginCommand = new
                {
                    User = _mapper.Map<UserDto>(user),
                    Token = _mapper.Map<UserAndTokenDto>(userToken),
                };
                //userLoginCommand.User.PortfolioGetCommand = Mapper.Map<PortfolioGetCommand>(currentPortfolio);
                //userLoginCommand.User.CurrentPortfolioId = user.CurrentPortfolioId;
                return Result.Success(userLoginCommand);
            }
            return Result.Fail("Lütfen bilgilerinizi kontrol ediniz.");
        }

        public async Task<IResult> LoginWithUserNameAsync(LoginWithUserNameCommand loginWithUserNameCommand)
        {
            //ValidationTool.Validate(new LoginWithUserNameCommandValidator(), loginWithUserNameCommand);
            //.Include(a => a.Portfolios).ThenInclude(c => c.PortfolioTokens)

            var user = await _context.Users.SingleOrDefaultAsync(a => a.UserName == loginWithUserNameCommand.UserName);
            if (user is null)
                return Result.Fail(Messages.General.NotFoundArgument("kullanıcı"));

            if (HashingHelper.VerifyPasswordHash(loginWithUserNameCommand.Password, user.PasswordHash, user.PasswordSalt))
            {
                if (!user.IsActive)
                    return Result.Fail("Hesabınızı Aktif Etmek İçin Destek ile İletişime Geçiniz.");
                if (!user.IsEmailAddressVerified)
                    return Result.Fail("Hesabınızı Aktif Etmek İçin Mailinizi Doğrulamanız Gerekmektedir.");
                //if (!user.IsPhoneNumberVerified)
                //    return Result.(ResultStatus.Error, "Hesabınızı Aktif Etmek İçin Telefon Numaranızı Doğrulamanız Gerekmektedir.");
                user.LastLogin = DateTime.Now;
                user.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                //var currentPortfolio = user.Portfolios.SingleOrDefault(a => a.Id == user.CurrentPortfolioId);

                //if (user.CurrentPortfolioId is 0 || currentPortfolio is null)
                //{
                //    var firstPortfolio = user.Portfolios.FirstOrDefault();
                //    if (firstPortfolio is not null)
                //        user.CurrentPortfolioId = firstPortfolio.Id;
                //    else if (user.CurrentPortfolioId == 0 || user.CurrentPortfolioId == null || currentPortfolio == null)
                //    {
                //        var result = (await _portfolioService.CreatePortfolioAsync(user.Id, "My Portfolio")).Data as PortfolioGetCommand;
                //        user.CurrentPortfolioId = result.Id;
                //        currentPortfolio = Mapper.Map<Portfolio>(result);
                //    }
                //}


                var accessToken = await CreateAccessTokenAsync(user);
                AccessToken userToken = new AccessToken
                {
                    UserId = user.Id,
                    Token = accessToken.Token,
                    CreatedDate = DateTime.Now,
                    IpAddress = user.IpAddress
                };
                _context.Users.Update(user);
                await _context.AccessTokens.AddAsync(userToken);
                await _context.SaveChangesAsync();

                var userLoginCommand = new
                {
                    User = _mapper.Map<UserDto>(user),
                    Token = _mapper.Map<UserAndToken>(userToken),
                };
                //userLoginCommand.User.PortfolioGetCommand = Mapper.Map<PortfolioGetCommand>(currentPortfolio);
                //userLoginCommand.User.CurrentPortfolioId = user.CurrentPortfolioId;
                return Result.Success(userLoginCommand);
            }
            return Result.Fail("Lütfen bilgilerinizi kontrol ediniz.");
        }

        public async Task<IResult> RegisterAsync(RegisterCommand registerCommand)
        {
            //ValidationTool.Validate(new RegisterCommandValidator(), registerCommand);
            //var formattedString = String.Format("dd/MM/yyyy", registerCommand.Birthday);
            if (await _context.Users.SingleOrDefaultAsync(a => a.PhoneNumber == registerCommand.PhoneNumber || a.UserName == registerCommand.UserName || a.EmailAddress == registerCommand.EmailAddress) is not null)
                return Result.Fail("Bu kullanıcı mevcut");
            //var userVerifyCheck = await _context.VerificationCodes.SingleOrDefaultAsync(a => a.EmailAddress == registerCommand.EmailAddress && a.PhoneNumber == registerCommand.PhoneNumber);
            var userVerifyCheck = await _context.VerificationCodes.SingleOrDefaultAsync(a => a.EmailAddress == registerCommand.EmailAddress);
            if (userVerifyCheck is null)
                return Result.Fail("Böyle bir kayıt bulunamadı.");

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(registerCommand.Password, out passwordHash, out passwordSalt);
            var user = _mapper.Map<User>(registerCommand);
            user.Birthday = DateTime.Parse(registerCommand.Birth, new CultureInfo("es-ES"));
            user.UserName = user.UserName.ToLower();
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.IsEmailAddressVerified = true;
            user.IsPhoneNumberVerified = true;
            user.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            user.CreatedDate = DateTime.Now;
            user.IsActive = false;
            var accessToken = await CreateAccessTokenAsync(user);

            await _context.Users.AddAsync(user);
            //Portfolio portfolio = new Portfolio()
            //{
            //    User = user,
            //    UserId = user.Id,
            //    TotalPrice = 0,
            //    TotalPriceUser24Hour = 0,
            //    TotalPriceChange = 0,
            //    TotalPriceChangePercent = 0,
            //    Name = "My Portfolio",
            //    PortfolioTokens = new List<PortfolioToken>()
            //};
            //await _context.Portfolios.AddAsync(portfolio);
            //user.Portfolios.Add(portfolio);
            //await _context.SaveChangesAsync();
            //user.CurrentPortfolioId = portfolio.Id;
            AccessToken userToken = new AccessToken
            {
                UserId = user.Id,
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
            await _context.SaveChangesAsync();
            return Result.Success($"Hoşgeldiniz Sayın {user.FirstName} {user.LastName}.", new
            {
                User = _mapper.Map<UserDto>(user),
                Token = _mapper.Map<UserAndTokenDto>(userToken),
            });
        }

        public async Task<IResult> SendActivationCodeEmailAsync(string emailAddress)
        {
            var verification = await _context.VerificationCodes.SingleOrDefaultAsync(a => a.EmailAddress == emailAddress);
            if (verification is null)
                return Result.Fail("Lütfen kayıt olma ekranına geri dönüp tekrar deneyiniz.");

            verification.MailCode = VerificationCodeGenerator.Generate();
            verification.VerificationCodeValidDate = DateTime.Now.AddMinutes(3);
            verification.ModifiedDate = DateTime.Now;
            _context.VerificationCodes.Update(verification);
            await _context.SaveChangesAsync();
            await _mailService.SendEmail(new EmailSendCommand
            {
                ReceiverMail = emailAddress,
                Subject = "FahaX Doğrulama Kodu",
                Content = $@" Doğrulama kodunun süresi 3 dakika ile sınırlıdır. Doğrulama kodunuz: {verification.MailCode}"
            });
            return Result.Success("Doğrulama kodu başarıyla mail adresinize gönderildi. Kodun geçerlilik süresi 3 dakikadır.", verification);
        }

        public async Task<IResult> SendActivationCodePhoneAsync(string phoneNumber)
        {
            var phoneIsExist = await _context.VerificationCodes.SingleOrDefaultAsync(a => a.PhoneNumber == phoneNumber);
            if (phoneIsExist is null)
                return Result.Fail("Lütfen kayıt olma ekranına geri dönüp tekrar deneyiniz.");
            phoneIsExist.PhoneCode = VerificationCodeGenerator.Generate();
            phoneIsExist.VerificationCodeValidDate = DateTime.Now.AddMinutes(3);
            phoneIsExist.ModifiedDate = DateTime.Now;
            _context.VerificationCodes.Update(phoneIsExist);
            await _context.SaveChangesAsync();
            await _phoneService.SendPhoneVerificationCodeAsync(phoneNumber, phoneIsExist.PhoneCode);
            return Result.Success("Doğrulama kodu başarıyla telefon numaranıza gönderildi. Kodun geçerlilik süresi 3 dakikadır.", phoneIsExist);
        }

        public async Task<IResult> VerificationCodeAddAsync(string phoneNumber, string emailAddress)
        {
            var verification = _context.VerificationCodes.Where(a => a.PhoneNumber == phoneNumber || a.EmailAddress == emailAddress);
            if (verification.Count() > 0)
                foreach (var item in verification)
                    _context.VerificationCodes.Remove(item);

            VerificationCode verificationCode = new VerificationCode()
            {
                EmailAddress = emailAddress,
                PhoneNumber = phoneNumber,
                CreatedDate = DateTime.Now,
                IsMailConfirmed = false,
                IsPhoneNumberConfirmed = false,
            };

            await _context.VerificationCodes.AddAsync(verificationCode);
            await _context.SaveChangesAsync();
            return Result.Success("Kod eşleşmesi başarıyla oluşturuldu.");
        }
    }
}

