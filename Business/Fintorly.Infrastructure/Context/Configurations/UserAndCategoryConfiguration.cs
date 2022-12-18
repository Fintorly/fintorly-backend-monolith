using Fintorly.Domain.ConfigureEntities;
using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class UserAndCategoryConfiguration : IEntityTypeConfiguration<UserAndCategory>
{
    public void Configure(EntityTypeBuilder<UserAndCategory> builder)
    {
        builder.HasKey(a => new { a.CategoryId, a.UserId });
        
        builder.HasOne<User>(a => a.User).WithMany(a => a.UserAndCategories).HasForeignKey(a => a.UserId);
        builder.HasOne<Category>(a => a.Category).WithMany(a => a.UserAndCategories).HasForeignKey(a => a.CategoryId);
        
        builder.ToTable("UserAndCategories");
    }
}