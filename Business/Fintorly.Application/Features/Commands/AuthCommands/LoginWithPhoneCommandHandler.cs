using Fintorly.Application.Dtos.UserDtos;

namespace Fintorly.Application.Features.Commands.AuthCommands;

public class LoginWithPhoneCommandHandler : IRequestHandler<LoginWithPhoneCommand,IResult<UserAndTokenDto>>
{
    private IUserAuthRepository _authRepository;

    public LoginWithPhoneCommandHandler(IUserAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<IResult<UserAndTokenDto>> Handle(LoginWithPhoneCommand request, CancellationToken cancellationToken)
    {
        return await _authRepository.LoginWithPhoneAsync(request);
    }
}