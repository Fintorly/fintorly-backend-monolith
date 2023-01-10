using Fintorly.Infrastructure.Context;
using Fintorly.Infrastructure.Context.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Fintorly.Infrastructure.Extensions;

public static class DbContextOptionsExtension
{
    public static void ExtendConfigurations(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AdvertisementConfiguration());
        modelBuilder.ApplyConfiguration(new AnswerConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        modelBuilder.ApplyConfiguration(new ConnectionConfiguration());
        modelBuilder.ApplyConfiguration(new GroupAndUserConfiguration());
        modelBuilder.ApplyConfiguration(new GroupConfiguration());
        modelBuilder.ApplyConfiguration(new MentorAndCategoryConfiguration());
        modelBuilder.ApplyConfiguration(new MentorAndTokenConfiguration());
        modelBuilder.ApplyConfiguration(new MentorAndUserConfiguration());
        modelBuilder.ApplyConfiguration(new MentorConfiguration());
        modelBuilder.ApplyConfiguration(new MessageAndReactionConfiguration());
        modelBuilder.ApplyConfiguration(new MessageConfiguration());
        modelBuilder.ApplyConfiguration(new MessagePictureConfiguration());
        modelBuilder.ApplyConfiguration(new OperationClaimConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionConfiguration());
        modelBuilder.ApplyConfiguration(new ReactionConfiguration());
        modelBuilder.ApplyConfiguration(new ReportConfiguration());
        modelBuilder.ApplyConfiguration(new ReviewCommentConfiguration());
        modelBuilder.ApplyConfiguration(new TierConfiguration());
        modelBuilder.ApplyConfiguration(new TierAndUserConfiguration());
        modelBuilder.ApplyConfiguration(new TokenConfiguration());
        modelBuilder.ApplyConfiguration(new UserAndCategoryConfiguration());
        modelBuilder.ApplyConfiguration(new UserAndOperationClaimConfigurations());
        modelBuilder.ApplyConfiguration(new UserAndTokenConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new AccessTokenConfiguration());
        modelBuilder.ApplyConfiguration(new VerificationCodeConfiguration());
        modelBuilder.ApplyConfiguration(new ProfilePictureConfiguration());
        modelBuilder.ApplyConfiguration(new PortfolioConfiguration());
        modelBuilder.ApplyConfiguration(new PortfolioTransactionConfiguration());
        modelBuilder.ApplyConfiguration(new PortfolioChartHistoryConfiguration());
        modelBuilder.ApplyConfiguration(new PortfolioTokenConfiguration());
        modelBuilder.ApplyConfiguration(new MentorAndOperationClaimConfiguration());
        modelBuilder.ApplyConfiguration(new ChoiceConfiguration());
        modelBuilder.ApplyConfiguration(new ApplicationConfiguration());
        modelBuilder.ApplyConfiguration(new AdditionalFeatureConfiguration());
        modelBuilder.ApplyConfiguration(new PostConfiguration());
        modelBuilder.ApplyConfiguration(new StepConfiguration());
        modelBuilder.ApplyConfiguration(new ApplicationRequestConfiguration());
        modelBuilder.ApplyConfiguration(new AnalysisConfiguration());
        modelBuilder.ApplyConfiguration(new AnalysisConfiguration());
    }
}