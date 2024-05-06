using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.CarRental.Core.ApplicationLayer.Commands
{
    public class CreatePrice
    {
        public long Id { get; set; }

        public DateTime StartTime { get; set; }
        public Price UnitPrice { get; set; }
    }
}
