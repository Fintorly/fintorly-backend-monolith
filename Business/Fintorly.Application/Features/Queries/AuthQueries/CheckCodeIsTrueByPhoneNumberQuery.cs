namespace Fintorly.Application.Features.Queries.AuthQueries;

public class CheckCodeIsTrueByPhoneNumberQuery : IRequest<IResult>
{
    public string PhoneNumber { get; set; }
    public string VerificationCode { get; set; }
}