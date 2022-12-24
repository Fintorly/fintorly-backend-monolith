using Fintorly.Application.Dtos.UserDtos;

namespace Fintorly.Application.Features.Commands.AuthCommands;

public class LoginWithUserNameCommandHandler : IRequestHandler<LoginWithUserNameCommand,IResult<UserAndTokenDto>>
{
    private IUserAuthRepository _authRepository;

    public LoginWithUserNameCommandHandler(IUserAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<IResult<UserAndTokenDto>> Handle(LoginWithUserNameCommand request, CancellationToken cancellationToken)
    {
        return await _authRepository.LoginWithUserNameAsync(request);
    }
}