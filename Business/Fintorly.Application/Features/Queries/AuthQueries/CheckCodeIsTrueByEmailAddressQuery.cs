namespace Fintorly.Application.Features.Queries.AuthQueries;

public class CheckCodeIsTrueByEmailAddressQuery : IRequest<IResult>
{
    public string EmailAddress { get; set; }
    public string VerificationCode { get; set; }
}