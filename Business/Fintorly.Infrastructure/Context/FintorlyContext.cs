using System;
using MediatR;
using Fintorly.Application.Interfaces.Context;
using Fintorly.Domain.Common;
using Fintorly.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Fintorly.Infrastructure.Context.Configurations;
using Fintorly.Domain.Entities;
using Fintorly.Domain.ConfigureEntities;

namespace Fintorly.Infrastructure.Context
{
    public class FintorlyContext : DbContext, IApplicationDbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "microServiceContext";
        private readonly IMediator _mediator;

        public DbSet<Advertisement> Advertisements{ get; set; }
        public DbSet<Answer> Answers{ get; set; }
        public DbSet<Category>Categories{ get; set; }
        public DbSet<Comment> Comments{ get; set; }
        public DbSet<Connection> Connections{ get; set; }
        public DbSet<Group> Groups{ get; set; }
        public DbSet<Mentor> Mentors{ get; set; }
        public DbSet<Message> Messages{ get; set; }
        public DbSet<MessagePicture> MessagePictures{ get; set; }
        public DbSet<OperationClaim> OperationClaims{ get; set; }
        public DbSet<Question> Questions{ get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Reaction> Reactions{ get; set; }
        public DbSet<Report> Reports{ get; set; }
        public DbSet<ReviewComment> ReviewComments{ get; set; }
        public DbSet<Tier> Tiers{ get; set; }
        public DbSet<Token> Tokens{ get; set; }
        public DbSet<AccessToken> AccessTokens{ get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VerificationCode> VerificationCodes{ get; set; }
        public DbSet<UserAndOperationClaim> UserAndOperationClaims { get; set; }
        public DbSet<GroupAndUser> GroupAndUsers{ get; set; }
        public DbSet<MentorAndCategory> MentorAndCategories{ get; set; }
        public DbSet<MentorAndToken> MentorAndTokens{ get; set; }
        public DbSet<MentorAndUser> MentorAndUsers{ get; set; }
        public DbSet<MessageAndReaction> MessageAndReactions{ get; set; }
        public DbSet<TierAndUser> TierAndUsers{ get; set; }
        public DbSet<UserAndCategory> UserAndCategories{ get; set; }
        public DbSet<UserAndToken> UserAndTokens{ get; set; }
        public DbSet<ProfilePicture> ProfilePictures{ get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<PortfolioChartHistory> PortfolioChartHistories { get; set; }
        public DbSet<PortfolioToken> PortfolioTokens { get; set; }
        public DbSet<PortfolioTransaction> PortfolioTransactions { get; set; }
        public DbSet<ApplicationRequest> ApplicationRequests { get; set; }
        public DbSet<MentorAndOperationClaim> MentorAndOperationClaims { get; set; }
        public FintorlyContext()
        {
        }

        public FintorlyContext(DbContextOptions<FintorlyContext> options, IMediator mediator)
            : base(options) => _mediator = mediator;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ExtendConfigurations();
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                connectionString:
                @"Server=3.70.171.214;Database=Fintorly;User=sa;Password=bhdKs3WOp7;");

            base.OnConfiguring(optionsBuilder);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);
            await base.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}