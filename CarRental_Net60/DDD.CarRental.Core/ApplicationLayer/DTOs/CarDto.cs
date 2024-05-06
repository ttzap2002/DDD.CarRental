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
    public class CarDTO : Entity, IAggregateRoot
    {
        public string RegistrationNumber { get; set; }

        public Position CurrentPosition { get; set; }
        public Distance CurrentDistance { get; set; }
        public Distance TotalDistance { get; set; }
        public ICollection<Rental> CarRentals { get; set; }
    }

    public enum Status
    {
        free = 0,
        reserved = 1,
        rental = 2
    }
}
