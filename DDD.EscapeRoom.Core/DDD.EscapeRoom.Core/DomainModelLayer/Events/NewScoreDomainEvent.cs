using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;

namespace DDD.EscapeRoom.Core.DomainModelLayer.Events
{
    public class NewScoreDomainEvent : DomainEvent
    {
        public Guid PlayerId { get; private set; }
        public int Minutes { get; set; }
        public DateTime Created { get; set; }
    }
}
