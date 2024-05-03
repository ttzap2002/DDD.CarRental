using DDD.SharedKernel.DomainModelLayer;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using System.Collections.Generic;

namespace DDD.CarRental.Core.DomainModelLayer.Models
{
    public class Driver : Entity, IAggregateRoot
    {
        public long ID { get; set; }
        public string LicenceNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public float FreeMinutes { get; set; }

        public ICollection<Rental> DriverRentals { get; set; }
    }

}
