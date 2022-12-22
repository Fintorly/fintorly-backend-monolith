using Fintorly.Domain.Common;
using Fintorly.Domain.ConfigureEntities;
using Fintorly.Domain.Enums;

namespace Fintorly.Domain.Entities;

public class User : BaseEntity, IEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public Gender Gender { get; set; }
    public DateTime Birthday { get; set; }
    public string PaymentChannel { get; set; }
    public bool IsPhoneNumberVerified { get; set; }
    public bool IsEmailAddressVerified { get; set; }
    public DateTime LastLogin { get; set; }
    public string? IpAddress { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }
    public Guid ProfilePictureId { get; set; }
    public ProfilePicture ProfilePicture { get; set; }
    //Kullanıcının ilgilendiği tokenlar
    public ICollection<UserAndToken> InterestedTokens { get; set; }
    //Kullanıcının abonelik aldığı mentörler
    public ICollection<MentorAndUser> MentorAndUsers { get; set; }
    //Kullanıcının cevapları
    public ICollection<Answer> Answers { get; set; }
    //Kullanıcnın mentörlere yaptığı yorumlar
    public ICollection<ReviewComment> ReviewComments { get; set; }
    //Kullanıcının bulunduğu gruplar
    public ICollection<GroupAndUser> GroupAndUsers { get; set; }
    public ICollection<UserAndOperationClaim> UserAndOperationClaims { get; set; }
    public ICollection<Report> Reports { get; set; }
    //Kullanıcının sahip olduğu paketler
    public ICollection<TierAndUser> TierAndUsers { get; set; }
    //Bağlantı
    public ICollection<Connection> Connections { get; set; }
    //Tokens
    public ICollection<AccessToken> AccessTokens { get; set; }
    //Kullanıcının ilgilendiği alanları kategorize etmek için
    public ICollection<UserAndCategory> UserAndCategories { get; set; }
    //Portfolio
    public ICollection<Portfolio> Portfolios { get; set; }


    public User() => Id = Guid.NewGuid();
}