using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class ConnectionConfiguration:IEntityTypeConfiguration<Connection>
{
    public void Configure(EntityTypeBuilder<Connection> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.ConnectionId).IsRequired();
        builder.Property(a => a.IsConnected).IsRequired();
        builder.Property(a => a.ConnectionStartDate).IsRequired();
        builder.Property(a => a.ConnectionEndDate).IsRequired();

        builder.HasOne<User>(a => a.User).WithMany(a => a.Connections).HasForeignKey(a => a.UserId);
        builder.HasOne<Mentor>(a => a.Mentor).WithMany(a => a.Connections).HasForeignKey(a => a.MentorId);

        builder.ToTable("Connections");
    }
}