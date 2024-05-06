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
    public class DriverDTO
    {
        public long Id { get; set; }
        public string LicenceNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public float FreeMinutes { get; set; }

    }
}
