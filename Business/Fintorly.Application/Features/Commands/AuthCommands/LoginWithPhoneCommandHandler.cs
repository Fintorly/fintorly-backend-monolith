using Fintorly.Application.Dtos.UserDtos;
using Fintorly.Domain.Enums;

namespace Fintorly.Application.Features.Commands.AuthCommands;

public class LoginWithPhoneCommandHandler:IRequestHandler<LoginWithPhoneCommand,IResult<UserAndTokenDto>>
{
    private readonly IUserAuthRepository _userAuthRepository;
    private readonly IMentorAuthRepository _mentorAuthRepository;

    public LoginWithPhoneCommandHandler(IMentorAuthRepository mentorAuthRepository)
    {
        _mentorAuthRepository = mentorAuthRepository;
    }

    public async Task<IResult<UserAndTokenDto>> Handle(LoginWithPhoneCommand request, CancellationToken cancellationToken)
    {
        var result = await _mentorAuthRepository.LoginWithPhoneAsync(request);
        if (result.Succeeded || result.ResultStatus == ResultStatus.Warning)
            return result;
        return await _mentorAuthRepository.LoginWithPhoneAsync(request);
    }
}