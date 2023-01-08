using Fintorly.Domain.Common;

namespace Fintorly.Domain.Entities;

public class PostPicture : BaseEntity, IEntity
{
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public Guid PostId { get; set; }
    public Post Post { get; set; }
}