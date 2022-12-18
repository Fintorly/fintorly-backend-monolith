using Fintorly.Domain.ConfigureEntities;
using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class GroupAndUserConfiguration:IEntityTypeConfiguration<GroupAndUser>
{
    public void Configure(EntityTypeBuilder<GroupAndUser> builder)
    {
        builder.HasKey(a => new { a.GroupId, a.UserId });
        
        
        builder.HasOne<Group>(a => a.Group).WithMany(a => a.GroupAndUsers).HasForeignKey(a => a.GroupId);
        builder.HasOne<User>(a => a.User).WithMany(a => a.GroupAndUsers).HasForeignKey(a => a.UserId);

        builder.ToTable("GroupAndUsers");
    }
}