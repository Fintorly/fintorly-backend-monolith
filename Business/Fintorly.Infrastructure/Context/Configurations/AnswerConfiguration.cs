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

        builder.HasOne<Mentor>(a => a.Mentor).WithMany(a => a.Answers).HasForeignKey(a => a.MentorId);
        builder.HasOne<User>(a => a.User).WithMany(a => a.Answers).HasForeignKey(a => a.UserId);
        builder.HasOne<Question>(a => a.Question).WithMany(a => a.Answers).HasForeignKey(a => a.QuestionId);
        builder.ToTable("Answers");
    }
    
    
    
}