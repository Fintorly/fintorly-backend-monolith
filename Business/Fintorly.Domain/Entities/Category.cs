using System;
using System.Collections.Generic;
using Fintorly.Domain.Common;
using Fintorly.Domain.ConfigureEntities;

namespace Fintorly.Domain.Entities;

public class Category : BaseEntity, IEntity
{
    public string Name { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public ICollection<UserAndCategory> UserAndCategories { get; set; }
    public ICollection<MentorAndCategory> MentorAndCategories { get; set; }
    public Category() => Id = Guid.NewGuid();
}