using DDD.SharedKernel.DomainModelLayer;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.CarRental.Core.DomainModelLayer.Models
{
    public class Tariff : Entity, IAggregateRoot
    {
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Price unitPrice { get; set; }
    }
}
