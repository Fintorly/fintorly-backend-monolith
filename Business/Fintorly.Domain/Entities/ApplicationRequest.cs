using System;
using Fintorly.Domain.Common;

namespace Fintorly.Domain.Entities;

public class ApplicationRequest : BaseEntity, IEntity
{
    public Guid MentorId { get; set; }
    public Mentor Mentor { get; set; }
    public string RejectionReason { get; set; }
    public bool IsAccepted { get; set; }
    public DateTime AcceptedDate { get; set; }
    public Guid? AdminId { get; set; }

    public ApplicationRequest() => Id = Guid.NewGuid();
}