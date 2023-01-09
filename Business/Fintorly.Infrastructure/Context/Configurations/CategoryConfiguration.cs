using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class CategoryConfiguration:IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(a => a.Id);
        
        builder.Property(a => a.Name).IsRequired();
        builder.Ignore(a => a.IpAddress);
        builder.Ignore(a => a.OsType);
        builder.Ignore(a => a.PhoneModel);
        
        builder.ToTable("Categories");
    }
}