using Fintorly.Domain.ConfigureEntities;
using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class MentorAndCategoryConfiguration:IEntityTypeConfiguration<MentorAndCategory>
{
    public void Configure(EntityTypeBuilder<MentorAndCategory> builder)
    {
        builder.HasKey(a=>new {a.MentorId,a.CategoryId});

        builder.HasOne<Mentor>(a => a.Mentor).WithMany(a => a.MentorAndCategories).HasForeignKey(a => a.MentorId);
        builder.HasOne<Category>(a => a.Category).WithMany(a => a.MentorAndCategories).HasForeignKey(a => a.CategoryId);

        builder.ToTable("MentorAndCategories");
    }
}