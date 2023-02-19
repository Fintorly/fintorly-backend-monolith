using System;
using System.Collections.Generic;
using Fintorly.Domain.Common;
using Fintorly.Domain.Enums;

namespace Fintorly.Domain.Entities;

public class Advertisement : BaseEntity, IEntity
{
    public string Title { get; set; }
    public string Content { get; set; }
    public decimal Price { get; set; }
    public PackageType PackageType { get; set; }
    public ICollection<Mentor> Mentors { get; set; }
    public Advertisement() => Id = Guid.NewGuid();
}