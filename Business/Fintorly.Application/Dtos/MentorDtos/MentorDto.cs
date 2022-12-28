using Fintorly.Application.Dtos.PortfolioDtos;
using Fintorly.Domain.Enums;

namespace Fintorly.Application.Dtos.MentorDtos;

public class MentorDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public Gender Gender { get; set; }
    public DateTime Birthday { get; set; }
    public bool IsPhoneNumberVerified { get; set; }
    public bool IsEmailAddressVerified { get; set; }
    public PortfolioDto Portfolio { get; set; }
    public Guid CurrentPortfolioId { get; set; }
    public DateTime LastLogin { get; set; }
}