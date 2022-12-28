using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class ReportConfiguration:IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Title).IsRequired();
        builder.Property(a => a.Content).IsRequired();
        builder.Property(a => a.AdminNote).IsRequired(false);
        builder.Property(a => a.FileName).IsRequired(false);
        builder.Property(a => a.FilePath).IsRequired(false);
        builder.Property(a => a.IsSolved).IsRequired();
        builder.Property(a => a.IpAddress).IsRequired(false);
        builder.Property(a => a.OsType).IsRequired(false);
        builder.Property(a => a.PhoneModel).IsRequired(false);
        
        builder.HasOne<User>(a => a.User).WithMany(a => a.Reports).HasForeignKey(a => a.UserId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne<Comment>(a => a.Comment).WithMany(a => a.Reports).HasForeignKey(a => a.CommentId);
        builder.HasOne<Mentor>(a => a.Mentor).WithMany(a => a.Reports).HasForeignKey(a => a.MentorId).OnDelete(DeleteBehavior.NoAction); ;
        
        builder.ToTable("Reports");
    }
}