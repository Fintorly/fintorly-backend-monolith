using Fintorly.Application.Interfaces.Context;

namespace Fintorly.Application.Features.Commands.AuthCommands;

public class LoginWithMailCommandHandler : IRequestHandler<LoginWithMailCommand,IResult>
{
    private IAuthRepository _authRepository;

    public LoginWithMailCommandHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<IResult> Handle(LoginWithMailCommand request, CancellationToken cancellationToken)
    {
        return await _authRepository.LoginWithEmailAsync(request);
    }
}