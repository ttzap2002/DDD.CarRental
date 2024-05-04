using DDD.SharedKernel.DomainModelLayer;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using System.Collections.Generic;

namespace DDD.CarRental.Core.DomainModelLayer.Models
{
    public class Car : Entity, IAggregateRoot
    {
        public long ID { get; set; }
        public string RegistrationNumber { get; set; }

        public Position CurrentPosition { get; set; }
        public Distance CurrentDistance { get; set; }
        public Distance TotalDistance { get; set; }
        public Status CarStatus { get; set; }
        public ICollection<Rental> CarRentals { get; set; }

        protected Car() { }

        public Car(long id, string registrationNumber, Position currentPosition, Distance totalDistance) : base(id)
        {
            RegistrationNumber = registrationNumber;
            CurrentPosition = currentPosition;
            CurrentDistance = new Distance(0,Unit.kilometer);
            TotalDistance = totalDistance;
            CarStatus = Status.free;
            CarRentals = new List<Rental>();
        }
    }

    public enum Status
    {
        free = 0, 
        reserved = 1, 
        rental = 2
    }

}
