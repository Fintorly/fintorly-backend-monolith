using Fintorly.Application.Interfaces.Utils;

namespace Fintorly.Application.Features.Commands.AuthCommands;

public class EmailActiveCommandHandler:IRequestHandler<EmailActiveCommand,IResult>
{
    private IUserAuthRepository _userAuthRepository;
    private IMentorAuthRepository _mentorAuthRepository;
    private ITokenResolver _tokenResolver;


    public EmailActiveCommandHandler(IUserAuthRepository userAuthRepository, IMentorAuthRepository mentorAuthRepository, ITokenResolver tokenResolver)
    {
        _userAuthRepository = userAuthRepository;
        _mentorAuthRepository = mentorAuthRepository;
        _tokenResolver = tokenResolver;
    }

    public async Task<IResult> Handle(EmailActiveCommand request, CancellationToken cancellationToken)
    {
        var isMentor = await _tokenResolver.GetIsMentorAsync();
        if (isMentor)
            return await _mentorAuthRepository.ActiveEmailByActivationCodeAsync(request);
        return await _userAuthRepository.ActiveEmailByActivationCodeAsync(request);
    }
}