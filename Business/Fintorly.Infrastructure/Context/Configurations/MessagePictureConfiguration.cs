using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class MessagePictureConfiguration:IEntityTypeConfiguration<MessagePicture>
{
    public void Configure(EntityTypeBuilder<MessagePicture> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.FileName).IsRequired();
        builder.Property(a => a.FilePath).IsRequired();
        builder.Property(a => a.PictureType).IsRequired();
        builder.Property(a => a.IpAddress).IsRequired(false);
        builder.Property(a => a.OsType).IsRequired(false);
        builder.Property(a => a.PhoneModel).IsRequired(false);
        
        builder.HasOne<Message>(a => a.Message).WithMany(a => a.MessagePictures).HasForeignKey(a => a.MessageId);
        builder.ToTable("MessagePictures");
    }
}