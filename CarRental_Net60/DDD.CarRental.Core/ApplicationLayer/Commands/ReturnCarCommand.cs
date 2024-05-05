using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.CarRental.Core.ApplicationLayer.Commands
{
    public class ReturnCarCommand
    {
        public DateTime Finished { get; set; }
        public long DriverId { get; set; }
        public long CarId { get; set; }
        public long RentalId { get; set; }
    }
}
