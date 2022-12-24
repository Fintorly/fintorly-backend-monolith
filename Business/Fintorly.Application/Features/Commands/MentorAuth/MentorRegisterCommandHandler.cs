namespace Fintorly.Application.Features.Commands.MentorAuth;

public class MentorRegisterCommandHandler : IRequestHandler<MentorRegisterCommand, IResult>
{
    private readonly IMentorAuthRepository _mentorAuthRepository;

    public MentorRegisterCommandHandler(IMentorAuthRepository mentorAuthRepository)
    {
        _mentorAuthRepository = mentorAuthRepository;
    }

    public async Task<IResult> Handle(MentorRegisterCommand request, CancellationToken cancellationToken)
    {
        return await _mentorAuthRepository.RegisterAsync(request);
    }
}