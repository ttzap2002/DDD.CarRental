using DDD.EscapeRoom.Core.DomainModelLayer.Events;
using DDD.EscapeRoom.Core.DomainModelLayer.Interfaces;
using DDD.SharedKernel.DomainModelLayer;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.EscapeRoom.Core.DomainModelLayer.Models
{
    public enum PlayerStatus
    {
        Active = 0,     // Gracz aktywny
        Banned = 1      // Gracz zbanowany
    }

    public class Player: Entity, IAggregateRoot
    {
        public string Name { get; protected set; }
        public Email Email { get; protected set; }
        public PlayerStatus Status { get; protected set; }

        // konstruktor na potrzeby serializacji
        protected Player()
        { }

        public Player(long playerId, string name, string email)
            : base(playerId)
        {
            if (String.IsNullOrEmpty(name)) throw new ArgumentNullException("Player name is null or empty");
            if (String.IsNullOrEmpty(email)) throw new ArgumentNullException("Email name is null or empty");

            Name = name;
            Email = new Email(email);
            Status = PlayerStatus.Active;

            this.AddDomainEvent(new PlayerCreatedDomainEvent(this.Id, this.Name.ToString(), this.Email.Value));
        }
    }
}
