using Fintorly.Application.Interfaces.Utils;
using Fintorly.Domain.Enums;

namespace Fintorly.Application.Features.Commands.AuthCommands;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, IResult>
{
    private IUserAuthRepository _userAuthRepository;
    private IMentorAuthRepository _mentorAuthRepository;
    private ITokenResolver _tokenResolver;

    public ChangePasswordCommandHandler(IUserAuthRepository authRepository, IMentorAuthRepository mentorAuthRepository,
        ITokenResolver tokenResolver)
    {
        _userAuthRepository = authRepository;
        _mentorAuthRepository = mentorAuthRepository;
        _tokenResolver = tokenResolver;
    }

    public async Task<IResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var isMentor = await _tokenResolver.GetIsMentorAsync();
        if (isMentor)
        {
            var result = await _mentorAuthRepository.ChangePasswordAsync(request);
            if (result.Succeeded || result.ResultStatus == ResultStatus.Warning)
                return result;
        }

        return await _userAuthRepository.ChangePasswordAsync(request);
    }
}