using DDD.EscapeRoom.Core.DomainModelLayer.Events;
using DDD.EscapeRoom.Core.DomainModelLayer.Interfaces;
using DDD.SharedKernel.DomainModelLayer;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.EscapeRoom.Core.DomainModelLayer.Models
{
    public class Visit: Entity, IAggregateRoot
    {
        public DateTime Started { get; protected set; }
        public DateTime? Finished { get; protected set; }
        public Money Total { get; protected set; }
        public long PlayerId { get; protected set; }
        public long RoomId { get; protected set; }
        
        private IDiscountPolicy _policy;

        // konstruktor na potrzeby serializacji
        protected Visit()
        { }

        public Visit(long visitId, long roomId, long playerId, DateTime started)
            : base(visitId)
        {
            this.Started = started;
            this.RoomId = roomId;
            this.PlayerId = playerId;
            this.Total = Money.Zero;
            
            this.AddDomainEvent(new VisitStartedDomainEvent(this));
        }

        public void RegisterPolicy(IDiscountPolicy policy)
        {
            this._policy = policy ?? throw new ArgumentNullException("Empty discount policy");
        }

        public void StopVisit(DateTime finished, Money unitPrice)
        {
            // simple date walidation
            if (finished < Started)
                throw new ArgumentException($"Exit date and time is earlier than enter date and time.");

            // set visit status
            this.Finished = finished;

            // calculate total
            var timeInMinutes = (this.Finished.Value - this.Started).Minutes;
            Total = unitPrice.MultiplyBy(timeInMinutes);

            // apply discount policy and recalculate total
            if (this._policy != null)
            {
                Money discount = this._policy.CalculateDiscount(this.Total, timeInMinutes, unitPrice);
                Total = (discount > Total) ? Money.Zero : Total - discount;
            }

            // publish event
            this.AddDomainEvent(new VisitFishedDomainEvent(this));
        }

        public int GetTimeInMinutes()
        {
            if (!this.Finished.HasValue) throw new InvalidOperationException("Not finished visit");

            return (this.Finished.Value - this.Started).Minutes;
        }
    }
}
