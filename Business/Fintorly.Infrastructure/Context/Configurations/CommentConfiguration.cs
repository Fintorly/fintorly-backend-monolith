using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Content).IsRequired();
        builder.Property(a => a.North).IsRequired(false);
        builder.Property(a => a.South).IsRequired(false);
        builder.Property(a => a.FileName).IsRequired(false);
        builder.Property(a => a.FilePath).IsRequired(false);
        
        builder.HasOne<Token>(a => a.Token)
            .WithMany(a => a.Comments).HasForeignKey(a => a.TokenId).OnDelete(DeleteBehavior.Cascade);
        //Comment bir adet mentör'e sahip
        //Bir Mentör birden fazla Comments'a sahip
        //Foreign Key olarakte MentorId verdik
        builder.HasOne<Mentor>(a => a.Mentor).WithMany(a => a.Comments).HasForeignKey(a => a.MentorId);
            
        
        builder.ToTable("Comments");
    }
}