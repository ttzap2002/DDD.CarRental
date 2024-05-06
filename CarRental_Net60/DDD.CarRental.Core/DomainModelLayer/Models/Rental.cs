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

        public void StartRental(Car car, Position position)
        {
            car.CarStatus = Status.rental;
            car.CurrentPosition = position;
            this.AddDomainEvent(new StartRentalDomainEvent(this));
        }
        public void FinishRental(Car car,Driver driver, DateTime Finish, Money unitPrice)
        {
            car.CarStatus = Status.free;
            this.Finished = Finish;
            long minutes = (long)(Finished - Started).Value.TotalMinutes - (long)driver.FreeMinutes;
            

            MoneyForRental = new Money(unitPrice.Amount*minutes);
            driver.FreeMinutes = this._policy.CalculateDiscount(minutes);
            this.AddDomainEvent(new FinishRentalDomainEvent(this));
        }

    }

}
