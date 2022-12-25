using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class GroupConfiguration:IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Title).IsRequired();
        builder.Property(a => a.FilePath).IsRequired();
        builder.Property(a => a.FileName).IsRequired();
        builder.Ignore(a => a.IpAddress);
        builder.Ignore(a => a.OsType);
        builder.Ignore(a => a.PhoneModel);
        builder.HasOne<Mentor>(a => a.Mentor).WithMany(a => a.Groups).HasForeignKey(a => a.MentorId);
      
        // builder.HasMany<User>(a => a.Users).WithMany(a => a.Groups);
        builder.ToTable("Groups");
    }
}