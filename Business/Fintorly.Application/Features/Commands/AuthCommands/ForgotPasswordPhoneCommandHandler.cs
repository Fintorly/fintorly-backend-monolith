using Fintorly.Application.Interfaces.Utils;
using Fintorly.Domain.Enums;

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
        {
            var result = await _mentorAuthRepository.ForgotPasswordPhoneAsync(request);
            if (result.Succeeded || result.ResultStatus == ResultStatus.Warning)
                return result;
        }
        return await _userAuthRepository.ForgotPasswordPhoneAsync(request);
    }
}