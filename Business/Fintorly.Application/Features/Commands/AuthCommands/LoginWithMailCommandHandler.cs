using Fintorly.Application.Dtos.UserDtos;
using Fintorly.Domain.Enums;

namespace Fintorly.Application.Features.Commands.AuthCommands;

public class LoginWithMailCommandHandler : IRequestHandler<LoginWithMailCommand, IResult<UserAndTokenDto>>
{
    private readonly IUserAuthRepository _userAuthRepository;
    private readonly IMentorAuthRepository _mentorAuthRepository;

    public LoginWithMailCommandHandler(IMentorAuthRepository mentorAuthRepository, IUserAuthRepository userAuthRepository)
    {
        _mentorAuthRepository = mentorAuthRepository;
        _userAuthRepository = userAuthRepository;
    }

    public async Task<IResult<UserAndTokenDto>> Handle(LoginWithMailCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _mentorAuthRepository.LoginWithEmailAsync(request);
        if (result.Succeeded || result.ResultStatus == ResultStatus.Warning)
            return result;
        return await _userAuthRepository.LoginWithEmailAsync(request);
    }
}