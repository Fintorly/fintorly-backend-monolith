using System;
using Fintorly.Domain.Common;
using Fintorly.Domain.Enums;

namespace Fintorly.Domain.Entities;

public class MessagePicture : BaseEntity, IEntity
{
    public string FilePath { get; set; }
    public string FileName { get; set; }
    public PictureType PictureType { get; set; }
    public Guid MessageId { get; set; }
    public Message Message { get; set; }
    public MessagePicture() => Id = Guid.NewGuid();
}
