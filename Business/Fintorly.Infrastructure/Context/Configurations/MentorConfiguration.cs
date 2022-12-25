using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class MentorConfiguration:IEntityTypeConfiguration<Mentor>
{
    public void Configure(EntityTypeBuilder<Mentor> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.TotalPoint).IsRequired();
        builder.Property(a => a.PointAverage).IsRequired();
        builder.Property(a => a.TotalVote).IsRequired();
        builder.Property(a => a.Rank).HasDefaultValue("0");
        builder.Property(a => a.FirstName).IsRequired();
        builder.Property(a => a.LastName).IsRequired();
        builder.Property(a => a.UserName).IsRequired();
        builder.Property(a => a.PhoneNumber).IsRequired(false);
        builder.Property(a => a.EmailAddress).IsRequired();
        builder.Property(a => a.Iban).IsRequired();
        builder.Property(a => a.Birthday).IsRequired();
        builder.Property(a => a.Gender).IsRequired();
        builder.Property(a => a.IsMentorVerified).IsRequired();
        builder.Property(a => a.IpAddress).IsRequired(false);
        builder.Property(a => a.OsType).IsRequired(false);
        builder.Property(a => a.PhoneModel).IsRequired(false);
        //builder.HasMany<Group>(a => a.Groups).WithOne(a => a.Mentor).HasForeignKey(a => a.MentorId);
        builder.HasOne<Advertisement>(a => a.Advertisement).WithMany(a => a.Mentors).HasForeignKey(a => a.AdvertisementId);

        builder.ToTable("Mentors");
    }
    
}