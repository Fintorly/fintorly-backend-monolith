using Fintorly.Application.Dtos.UserDtos;
using Fintorly.Application.Interfaces.Context;

namespace Fintorly.Application.Features.Commands.AuthCommands;

public class LoginWithMailCommandHandler : IRequestHandler<LoginWithMailCommand,IResult<UserAndTokenDto>>
{
    private IAuthRepository _authRepository;

    public LoginWithMailCommandHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<IResult<UserAndTokenDto>> Handle(LoginWithMailCommand request, CancellationToken cancellationToken)
    {
        return await _authRepository.LoginWithEmailAsync(request);
    }
}