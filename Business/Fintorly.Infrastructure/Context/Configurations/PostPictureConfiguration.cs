using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class PostPictureConfiguration : IEntityTypeConfiguration<PostPicture>
{
    public void Configure(EntityTypeBuilder<PostPicture> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasOne<Post>(a => a.Post).WithMany(a => a.PostPictures).HasForeignKey(a => a.PostId);

        builder.ToTable("PostPictures");
    }
}