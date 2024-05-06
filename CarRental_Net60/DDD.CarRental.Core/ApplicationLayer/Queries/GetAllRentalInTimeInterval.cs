using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.CarRental.Core.ApplicationLayer.Queries
{
    public class GetAllRentalInTimeInterval
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
