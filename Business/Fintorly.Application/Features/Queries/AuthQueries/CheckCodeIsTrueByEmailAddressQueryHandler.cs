namespace Fintorly.Application.Features.Queries.AuthQueries;

public class CheckCodeIsTrueByEmailAddressQueryHandler : IRequestHandler<CheckCodeIsTrueByEmailAddressQuery, IResult>
{
    private IUserAuthRepository _authRepository;

    public CheckCodeIsTrueByEmailAddressQueryHandler(IUserAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<IResult> Handle(CheckCodeIsTrueByEmailAddressQuery request, CancellationToken cancellationToken)
    {
        return await _authRepository.CheckCodeIsTrueByEmailAsync(request);
    }
}