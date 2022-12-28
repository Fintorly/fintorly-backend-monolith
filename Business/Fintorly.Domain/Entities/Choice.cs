using System;
using Fintorly.Domain.Common;
using Fintorly.Domain.Enums;

namespace Fintorly.Domain.Entities
{
    public class Choice : BaseEntity, IEntity
    {
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
        public QuestionChoice Key { get; set; }
        public string Value { get; set; }
    }
}

