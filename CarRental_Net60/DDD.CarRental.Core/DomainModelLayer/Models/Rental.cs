using DDD.SharedKernel.DomainModelLayer;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Text;

namespace DDD.CarRental.Core.DomainModelLayer.Models
{
    public class Rental: Entity, IAggregateRoot
    {
        public int Id { get; set; }
        public DateTime Started { get; set; }

        public DateTime Finished { get; set; }

        public Car _Car { get; set; }
        public int CarId { get; set; }

        public Driver _Driver { get; set; }
        public int DriverId { get; set; }

    }

}
