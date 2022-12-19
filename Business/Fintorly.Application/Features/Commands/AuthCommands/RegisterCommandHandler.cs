namespace Fintorly.Application.Features.Commands.AuthCommands;

public class RegisterCommandHandler:IRequestHandler<RegisterCommand,IResult>
{
    private IAuthRepository _authRepository;

    public RegisterCommandHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<IResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await _authRepository.RegisterAsync(request);
    }
}