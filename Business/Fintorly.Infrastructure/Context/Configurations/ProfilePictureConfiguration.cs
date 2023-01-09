using System;
using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations
{
    public class ProfilePictureConfiguration : IEntityTypeConfiguration<ProfilePicture>
    {
        public void Configure(EntityTypeBuilder<ProfilePicture> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Title).IsRequired(false);
            builder.Ignore(a => a.IpAddress);
            builder.Ignore(a => a.OsType);
            builder.Ignore(a => a.PhoneModel);
            
            builder.HasMany<User>(a => a.Users).WithOne(a => a.ProfilePicture).HasForeignKey(a => a.ProfilePictureId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany<Mentor>(a => a.Mentors).WithOne(a => a.ProfilePicture).HasForeignKey(a => a.ProfilePictureId).OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("ProfilePictures");
        }
    }
}

