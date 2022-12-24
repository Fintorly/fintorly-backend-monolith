using Fintorly.Application.Interfaces.Utils;

namespace Fintorly.Application.Features.Commands.AuthCommands;

public class ForgotPasswordPhoneCommandHandler:IRequestHandler<ForgotPasswordPhoneCommand,IResult>
{
    private IUserAuthRepository _userAuthRepository;
    private IMentorAuthRepository _mentorAuthRepository;
    private ITokenResolver _tokenResolver;
    public ForgotPasswordPhoneCommandHandler(IUserAuthRepository userAuthRepository, IMentorAuthRepository mentorAuthRepository, ITokenResolver tokenResolver)
    {
        _userAuthRepository = userAuthRepository;
        _mentorAuthRepository = mentorAuthRepository;
        _tokenResolver = tokenResolver;
    }

    public async Task<IResult> Handle(ForgotPasswordPhoneCommand request, CancellationToken cancellationToken)
    {
        var isMentor = await _tokenResolver.GetIsMentorAsync();
        if (isMentor)
            return await _mentorAuthRepository.ForgotPasswordPhoneAsync(request);
        return await _userAuthRepository.ForgotPasswordPhoneAsync(request);
    }
}