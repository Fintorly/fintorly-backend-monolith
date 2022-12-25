using System;
using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations
{
	public class ChoiceConfiguration:IEntityTypeConfiguration<Choice>
	{
        public void Configure(EntityTypeBuilder<Choice> builder)
        {
	        builder.HasKey(a => a.Id);
	        builder.Property(a => a.Key).IsRequired();
	        builder.Property(a => a.Value).IsRequired();
	        
	        builder.HasOne<Question>(a => a.Question).WithMany(a => a.Choices).HasForeignKey(a => a.QuestionId);
	        builder.ToTable("Choices");
        }
    }
}

