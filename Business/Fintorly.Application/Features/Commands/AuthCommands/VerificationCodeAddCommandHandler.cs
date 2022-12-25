namespace Fintorly.Application.Features.Commands.AuthCommands;

public class VerificationCodeAddCommandHandler : IRequestHandler<VerificationCodeAddCommand, IResult>
{
    private IAuthRepository _authRepository;

    public VerificationCodeAddCommandHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<IResult> Handle(VerificationCodeAddCommand request, CancellationToken cancellationToken)
    {
        return await _authRepository.VerificationCodeAddAsync(request);
    }
}