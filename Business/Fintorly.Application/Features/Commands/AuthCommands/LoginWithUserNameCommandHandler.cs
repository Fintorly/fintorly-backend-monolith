using Fintorly.Application.Dtos.UserDtos;
using Fintorly.Domain.Enums;

namespace Fintorly.Application.Features.Commands.AuthCommands;

public class LoginWithUserNameCommandHandler:IRequestHandler<LoginWithUserNameCommand,IResult<UserAndTokenDto>>
{
    private readonly IUserAuthRepository _userAuthRepository;
    private readonly IMentorAuthRepository _mentorAuthRepository;

    public LoginWithUserNameCommandHandler(IMentorAuthRepository mentorAuthRepository)
    {
        _mentorAuthRepository = mentorAuthRepository;
    }

    public async Task<IResult<UserAndTokenDto>> Handle(LoginWithUserNameCommand request, CancellationToken cancellationToken)
    { 
        var result = await _mentorAuthRepository.LoginWithUserNameAsync(request);
        if (result.Succeeded || result.ResultStatus == ResultStatus.Warning)
            return result;
        return await _mentorAuthRepository.LoginWithUserNameAsync(request);
    }
}