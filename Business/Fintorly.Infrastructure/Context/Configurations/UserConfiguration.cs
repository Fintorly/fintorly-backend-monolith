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
            builder.Property(a => a.Language).HasDefaultValue("0");
            builder.Property(a => a.IpAddress).IsRequired(false);
            builder.Property(a => a.OsType).IsRequired(false);
            builder.Property(a => a.PhoneModel).IsRequired(false);
            builder.Property(a => a.PaymentChannel).IsRequired(false);

            builder.HasOne<ProfilePicture>(a => a.ProfilePicture).WithMany(a => a.Users).HasForeignKey(a => a.ProfilePictureId);

            builder.ToTable("Users");
        }
    }
}

