using DDD.EscapeRoom.Core.DomainModelLayer.Interfaces;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.EscapeRoom.Core.DomainModelLayer.Events
{
    public class PlayerCreatedDomainEvent : DomainEvent
    {
        public long PlayerId { get; private set; }
        public string Name { get; private set; }

        public string Email { get; private set; }


        public PlayerCreatedDomainEvent(long id, string name, string email)
        {
            this.PlayerId = id;
            this.Name = name;
            this.Email = email;
        }
            

    }
}
