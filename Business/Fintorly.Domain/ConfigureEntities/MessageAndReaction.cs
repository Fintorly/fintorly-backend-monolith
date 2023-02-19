using System;
using Fintorly.Domain.Common;
using Fintorly.Domain.Entities;

namespace Fintorly.Domain.ConfigureEntities;
//Bir mesaja birden fazla reaction verilebilir
//Bir reaction birden fazla mesaja verilebilir
public class MessageAndReaction: IEntity
{
    public Guid MessageId { get; set; }
    public Message Message { get; set; }
    public Guid ReactionId { get; set; }
    public Reaction Reaction { get; set; }
}