using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class MessageConfiguration:IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Content).IsRequired();
        builder.Property(a => a.IpAddress).IsRequired(false);
        builder.Property(a => a.OsType).IsRequired(false);
        builder.Property(a => a.PhoneModel).IsRequired(false);
        builder.Property(a => a.IpAddress).IsRequired(false);
        builder.Property(a => a.OsType).IsRequired(false);
        builder.Property(a => a.PhoneModel).IsRequired(false);
        
        builder.HasOne<Group>(a => a.Group).WithMany(a => a.Messages).HasForeignKey(a => a.GroupId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne<Mentor>(a => a.Mentor).WithMany(a => a.Messages).HasForeignKey(a => a.MentorId).OnDelete(DeleteBehavior.NoAction);
        builder.ToTable("Messages");
    }
}