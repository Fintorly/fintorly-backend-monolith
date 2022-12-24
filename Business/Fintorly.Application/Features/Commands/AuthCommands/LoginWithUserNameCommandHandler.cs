using Fintorly.Application.Dtos.UserDtos;

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
        var result = await _userAuthRepository.LoginWithUserNameAsync(request);
        if (result.Succeeded)
            return result;
        return await _mentorAuthRepository.LoginWithUserNameAsync(request);
    }
}