using Fintorly.Domain.ConfigureEntities;
using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class MentorAndUserConfiguration:IEntityTypeConfiguration<MentorAndUser>
{
    public void Configure(EntityTypeBuilder<MentorAndUser> builder)
    {
        builder.HasKey(a => new { a.MentorId, a.UserId });

        builder.HasOne<User>(a => a.User).WithMany(a => a.MentorAndUsers).HasForeignKey(a => a.UserId);
        builder.HasOne<Mentor>(a => a.Mentor).WithMany(a => a.MentorAndUsers).HasForeignKey(a => a.MentorId);

        builder.ToTable("MentorAndUsers");
    }
}