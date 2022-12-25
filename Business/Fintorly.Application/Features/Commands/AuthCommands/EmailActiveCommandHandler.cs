using Fintorly.Application.Interfaces.Utils;

namespace Fintorly.Application.Features.Commands.AuthCommands;

public class EmailActiveCommandHandler : IRequestHandler<EmailActiveCommand, IResult>
{
    private IAuthRepository _authRepository;


    public EmailActiveCommandHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<IResult> Handle(EmailActiveCommand request, CancellationToken cancellationToken)
    {
        return await _authRepository.ActiveEmailByActivationCodeAsync(request);
    }
}