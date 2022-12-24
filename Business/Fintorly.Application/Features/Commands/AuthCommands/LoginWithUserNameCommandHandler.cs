using Fintorly.Application.Dtos.UserDtos;

namespace Fintorly.Application.Features.Commands.AuthCommands;

public class LoginWithUserNameCommandHandler : IRequestHandler<LoginWithUserNameCommand,IResult<UserAndTokenDto>>
{
    private IAuthRepository _authRepository;

    public LoginWithUserNameCommandHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<IResult<UserAndTokenDto>> Handle(LoginWithUserNameCommand request, CancellationToken cancellationToken)
    {
        return await _authRepository.LoginWithUserNameAsync(request);
    }
}