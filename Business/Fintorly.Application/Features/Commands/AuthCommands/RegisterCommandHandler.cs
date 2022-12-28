using Fintorly.Application.Dtos.UserDtos;
using Fintorly.Application.Interfaces.Utils;

namespace Fintorly.Application.Features.Commands.AuthCommands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, IResult<UserAndTokenDto>>
{
    private readonly IUserAuthRepository _userAuthRepository;
    private readonly IMentorAuthRepository _mentorAuthRepository;

    public RegisterCommandHandler(IUserAuthRepository userAuthRepository,
        IMentorAuthRepository mentorAuthRepository)
    {
        _userAuthRepository = userAuthRepository;
        _mentorAuthRepository = mentorAuthRepository;
    }

    public async Task<IResult<UserAndTokenDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (request.IsMentor)
            return await _mentorAuthRepository.RegisterAsync(request);
        return await _userAuthRepository.RegisterAsync(request);
    }
}