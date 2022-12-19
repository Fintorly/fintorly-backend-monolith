namespace Fintorly.Application.Features.Queries.AuthQueries;

public class CheckCodeIsTrueByPhoneNumberQueryHandler : IRequestHandler<CheckCodeIsTrueByPhoneNumberQuery,IResult>
{
    private IAuthRepository _authRepository;

    public CheckCodeIsTrueByPhoneNumberQueryHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<IResult> Handle(CheckCodeIsTrueByPhoneNumberQuery request, CancellationToken cancellationToken)
    {
        return await _authRepository.CheckCodeIsTrueByPhoneAsync(request);
    }
}