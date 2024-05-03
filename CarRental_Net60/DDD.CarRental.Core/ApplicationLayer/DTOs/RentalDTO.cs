using DDD.CarRental.Core.DomainModelLayer.Models;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using DDD.SharedKernel.DomainModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.CarRental.Core.ApplicationLayer.DTOs
{
    public class RentalDTO : Entity, IAggregateRoot
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
