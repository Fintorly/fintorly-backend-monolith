using Fintorly.Domain.Entities;

namespace Fintorly.Domain.ConfigureEntities;

//InterestedTokens
public class UserAndToken
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid TokenId { get; set; }
    public Token Token { get; set; }
}