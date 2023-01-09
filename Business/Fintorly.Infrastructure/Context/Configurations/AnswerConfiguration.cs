using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class AnswerConfiguration:IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Content).IsRequired();
        builder.Property(a => a.IpAddress).IsRequired(false);
        builder.Property(a => a.OsType).IsRequired(false);
        builder.Property(a => a.PhoneModel).IsRequired(false);
        
        builder.HasOne<Mentor>(a => a.Mentor).WithMany(a => a.Answers).HasForeignKey(a => a.MentorId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne<User>(a => a.User).WithMany(a => a.Answers).HasForeignKey(a => a.UserId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne<Question>(a => a.Question).WithMany(a => a.Answers).HasForeignKey(a => a.QuestionId);
   
        builder.ToTable("Answers");
    }
}