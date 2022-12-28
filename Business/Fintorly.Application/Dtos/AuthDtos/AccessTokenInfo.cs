using Fintorly.Domain.Entities;

namespace Fintorly.Application.Dtos.AuthDtos;

public class AccessTokenInfo
{
    public string Id { get; set; }
    public string EmailAddress { get; set; }
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
    public string IpAddress { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsMentor { get; set; }
    public List<OperationClaim> OperationClaims { get; set; }
}