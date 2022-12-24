using Fintorly.Application.Dtos.UserDtos;

namespace Fintorly.Application.Features.Commands.AuthCommands;

public class LoginWithMailCommandHandler : IRequestHandler<LoginWithMailCommand, IResult<UserAndTokenDto>>
{
    private readonly IUserAuthRepository _userAuthRepository;
    private readonly IMentorAuthRepository _mentorAuthRepository;

    public LoginWithMailCommandHandler(IMentorAuthRepository mentorAuthRepository)
    {
        _mentorAuthRepository = mentorAuthRepository;
    }

    public async Task<IResult<UserAndTokenDto>> Handle(LoginWithMailCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _userAuthRepository.LoginWithEmailAsync(request);
        if (result.Succeeded)
            return result;
        return await _mentorAuthRepository.LoginWithEmailAsync(request);
    }
}