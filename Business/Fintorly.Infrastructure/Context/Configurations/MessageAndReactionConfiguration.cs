using Fintorly.Domain.ConfigureEntities;
using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class MessageAndReactionConfiguration:IEntityTypeConfiguration<MessageAndReaction>
{
    public void Configure(EntityTypeBuilder<MessageAndReaction> builder)
    {
        builder.HasKey(a => new { a.MessageId, a.ReactionId });

        builder.HasOne<Message>(a => a.Message).WithMany(a => a.MessageAndReactions).HasForeignKey(a=>a.MessageId);
        builder.HasOne<Reaction>(a => a.Reaction).WithMany(a => a.MessageAndReactions).HasForeignKey(a => a.ReactionId);

        builder.ToTable("MessageAndReactions");
    }
}