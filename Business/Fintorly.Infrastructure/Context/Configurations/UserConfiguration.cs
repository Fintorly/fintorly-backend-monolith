using System;
using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations
{
    public class UserConfiguration:IEntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.FirstName).IsRequired(true);
            builder.Property(a => a.LastName).IsRequired(true);
            builder.Property(a => a.UserName).IsRequired(true);
            builder.Property(a => a.PhoneNumber).IsRequired(false);
            builder.Property(a => a.EmailAddress).IsRequired(true);
            builder.Property(a => a.Birthday).IsRequired(true);
            builder.Property(a => a.Gender).IsRequired(true);

            builder.ToTable("Users");
        }
    }
}

