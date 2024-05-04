using DDD.CarRental.Core.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.CarRental.Core.ApplicationLayer.Commands
{
    public class CreateCarCommand
    {
        public long ID { get; set; }
        public string RegistrationNumber { get; set; }
        public Position CurrentPosition { get; set; }
        public Distance TotalDistance { get; set; }
    }
}
