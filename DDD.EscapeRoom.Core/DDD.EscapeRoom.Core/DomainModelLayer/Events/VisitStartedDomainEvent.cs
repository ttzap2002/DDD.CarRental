﻿using DDD.EscapeRoom.Core.DomainModelLayer.Models;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;

namespace DDD.EscapeRoom.Core.DomainModelLayer.Events
{
    public class VisitStartedDomainEvent : DomainEvent
    {
        public Visit Visit { get; private set; }
        
        public VisitStartedDomainEvent(Visit visit)
        {
            this.Visit = visit;
        }
    }
}
