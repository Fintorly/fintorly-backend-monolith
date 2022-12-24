namespace Fintorly.Application.Features.Queries.AuthQueries;

public class CheckCodeIsTrueByPhoneNumberQueryHandler : IRequestHandler<CheckCodeIsTrueByPhoneNumberQuery,IResult>
{
    private IUserAuthRepository _authRepository;

    public CheckCodeIsTrueByPhoneNumberQueryHandler(IUserAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<IResult> Handle(CheckCodeIsTrueByPhoneNumberQuery request, CancellationToken cancellationToken)
    {
        return await _authRepository.CheckCodeIsTrueByPhoneAsync(request);
    }
}