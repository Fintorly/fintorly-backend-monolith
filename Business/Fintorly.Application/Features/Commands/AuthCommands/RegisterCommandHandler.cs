namespace Fintorly.Application.Features.Commands.AuthCommands;

public class RegisterCommandHandler:IRequestHandler<RegisterCommand,IResult>
{
    private IUserAuthRepository _authRepository;

    public RegisterCommandHandler(IUserAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<IResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await _authRepository.RegisterAsync(request);
    }
}