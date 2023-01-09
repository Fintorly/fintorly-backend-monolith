using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Content).IsRequired();
        builder.Property(a => a.Content).HasMaxLength(200);

        builder.HasOne<Tier>(a => a.Tier).WithMany(a => a.Posts).HasForeignKey(a => a.TierId);
        builder.HasOne<Mentor>(a => a.Mentor).WithMany(a => a.Posts).HasForeignKey(a => a.MentorId);
        builder.ToTable("Posts");
    }
}