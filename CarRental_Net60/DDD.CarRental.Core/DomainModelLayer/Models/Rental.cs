using DDD.CarRental.Core.DomainModelLayer.Events;
using DDD.CarRental.Core.DomainModelLayer.Interfaces;
using DDD.SharedKernel.DomainModelLayer;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
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
            MoneyForRental = new Money(0);
        }

        public DateTime Started { get; set; }

        public DateTime? Finished { get; set; }

        public Money MoneyForRental { get; set; }

        
        public long CarId { get; protected set; }
        public long DriverId { get; protected set; }

        private IDiscountPolicy _policy;

        public void RegisterPolicy(IDiscountPolicy policy)
        {
            this._policy = policy ?? throw new ArgumentNullException("Empty discount policy");
        }

        public void StartRental(Car car)
        {
            car.CarStatus = Status.rental;
            this.AddDomainEvent(new StartRentalDomainEvent(this));
        }
        public void FinishRental(DateTime finished,Driver driver)
        {
            long minutes = (finished - Started).Minutes;
            this.Finished = finished;
            driver.FreeMinutes = this._policy.CalculateDiscount(minutes);

            this.AddDomainEvent(new FinishRentalDomainEvent(this));
        }

    }

}
