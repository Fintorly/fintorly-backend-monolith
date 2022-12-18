using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class TokenConfiguration:IEntityTypeConfiguration<Token>
{
    public void Configure(EntityTypeBuilder<Token> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Name).IsRequired();
        builder.Property(a => a.Url).IsRequired();

        builder.ToTable("Tokens");
    }
}