using System;
using Fintorly.Domain.ConfigureEntities;
using Fintorly.Domain.Entities;

namespace Fintorly.Application.Interfaces.Context
{
    public interface IApplicationDbContext
    {
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Mentor> Mentors { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessagePicture> MessagePictures { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReviewComment> ReviewComments { get; set; }
        public DbSet<Tier> Tiers { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<AccessToken> AccessTokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VerificationCode> VerificationCodes { get; set; }
        public DbSet<UserAndOperationClaim> UserAndOperationClaims { get; set; }
        public DbSet<GroupAndUser> GroupAndUsers { get; set; }
        public DbSet<MentorAndCategory> MentorAndCategories { get; set; }
        public DbSet<MentorAndToken> MentorAndTokens { get; set; }
        public DbSet<MentorAndUser> MentorAndUsers { get; set; }
        public DbSet<MessageAndReaction> MessageAndReactions { get; set; }
        public DbSet<TierAndUser> TierAndUsers { get; set; }
        public DbSet<UserAndCategory> UserAndCategories { get; set; }
        public DbSet<UserAndToken> UserAndTokens { get; set; }
        public DbSet<ProfilePicture> ProfilePictures { get; set; }
    }
}

