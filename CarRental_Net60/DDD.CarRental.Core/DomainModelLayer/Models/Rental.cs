using DDD.CarRental.Core.DomainModelLayer.Interfaces;
using DDD.SharedKernel.DomainModelLayer;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Text;

namespace DDD.CarRental.Core.DomainModelLayer.Models
{
    public class Rental: Entity, IAggregateRoot
    {
        public Rental(long id,DateTime started,long carId, long driverId) :base(id)
        {
            Started = started;
            CarId = carId;
            DriverId = driverId;
        }

        public DateTime Started { get; set; }

        public DateTime Finished { get; set; }

        public Car _Car { get; set; }
        public long CarId { get; set; }

        public Driver _Driver { get; set; }
        public long DriverId { get; set; }

        private IDiscountPolicy _policy;

        public void RegisterPolicy(IDiscountPolicy policy)
        {
            this._policy = policy ?? throw new ArgumentNullException("Empty discount policy");
        }

        public void FinishRental(DateTime finished)
        {
            long minutes = (finished - Started).Minutes;
            this.Finished = finished;
            _Driver.FreeMinutes = this._policy.CalculateDiscount(minutes);


            //this.AddDomainEvent(new VisitFishedDomainEvent(this));
        }

    }

}
