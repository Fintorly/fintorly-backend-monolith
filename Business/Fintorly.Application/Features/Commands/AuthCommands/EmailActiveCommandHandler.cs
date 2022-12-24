namespace Fintorly.Application.Features.Commands.AuthCommands;

public class EmailActiveCommandHandler:IRequestHandler<EmailActiveCommand,IResult>
{
    private IUserAuthRepository _authRepository;

    public EmailActiveCommandHandler(IUserAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<IResult> Handle(EmailActiveCommand request, CancellationToken cancellationToken)
    {
        return await _authRepository.ActiveEmailByActivationCodeAsync(request);
    }
}