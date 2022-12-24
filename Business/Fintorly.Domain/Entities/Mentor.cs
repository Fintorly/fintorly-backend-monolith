using System;
using Fintorly.Domain.Common;
using Fintorly.Domain.ConfigureEntities;
using Fintorly.Domain.Enums;

namespace Fintorly.Domain.Entities
{
    public class Mentor : BaseEntity, IEntity
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Iban { get; set; }
        public string PaymentChannel { get; set; }
        public string IpAddress { get; set; }
        public bool IsPhoneNumberVerified { get; set; }
        public bool IsEmailAddressVerified { get; set; }
        public DateTime LastLogin { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool IsMentorVerified { get; set; }

        public int Rank { get; set; }
        public int TotalEarnedPrice { get; set; }
        public int TotalRefund { get; set; }
//Mentor Picture
        public ProfilePicture ProfilePicture { get; set; }

        public Guid ProfilePictureId { get; set; }
        //Mentorün ortalama puanı
        public double PointAverage { get; set; }

        //Total point
        public double TotalPoint { get; set; }

        //Mentor oylama sayısı
        public int TotalVote { get; set; }

        //Mentorun üyeleri
        public ICollection<MentorAndUser> MentorAndUsers { get; set; }

        //Mentörun yaptığı yorumlar
        public ICollection<Comment> Comments { get; set; }

        //Mentörün yorum ve tokenları
        public ICollection<MentorAndToken> MentorAndTokens { get; set; }

        //Kullanıcıların inceleme yorumları
        public ICollection<ReviewComment> ReviewComments { get; set; }

        //Mentörün paketleri
        public ICollection<Tier> Tiers { get; set; }

        //Mentörün sahip olduğu gruplar
        public ICollection<Group> Groups { get; set; }

        //mentörün mesajları
        public ICollection<Message> Messages { get; set; }

        //Mentörün cevapları
        public ICollection<Answer> Answers { get; set; }

        //Kullanıcıların yaptığı şikayetler
        public ICollection<Report> Reports { get; set; }

        //bağlantı
        public ICollection<Connection> Connections { get; set; }

        //validate tokens
        public ICollection<AccessToken> AccessTokens { get; set; }

        //interested tokens
        public ICollection<Token> InterestedTokens { get; set; }
        //mentörün ilgilendiği alanları kategorize etmek için
        public ICollection<MentorAndCategory> MentorAndCategories { get; set; }
        //Portfolio
        public ICollection<Portfolio> Portfolios { get; set; }
        public Guid CurrentPortfolioId { get; set; }
        //Mentörün aldığı reklamlar
        //Mentör yetkileri
        public ICollection<MentorAndOperationClaim> MentorAndOperationClaims { get; set; }
        public Guid AdvertisementId { get; set; }
        public Advertisement Advertisement { get; set; }

        public Mentor() => Id = Guid.NewGuid();

    }
}