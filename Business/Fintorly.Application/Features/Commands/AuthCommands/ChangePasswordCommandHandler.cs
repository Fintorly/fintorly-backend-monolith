namespace Fintorly.Application.Features.Commands.AuthCommands;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, IResult>
{
    private IUserAuthRepository _userAuthRepository;
    private IMentorAuthRepository _mentorAuthRepository;

    public ChangePasswordCommandHandler(IUserAuthRepository authRepository, IMentorAuthRepository mentorAuthRepository)
    {
        _userAuthRepository = authRepository;
        _mentorAuthRepository = mentorAuthRepository;
    }

    public async Task<IResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var userResult = await _userAuthRepository.ChangePasswordAsync(request);
        if (userResult.Succeeded)
            return userResult;
        var mentorResult =await _mentorAuthRepository.ChangePasswordAsync(request);
        return mentorResult;
    }
}