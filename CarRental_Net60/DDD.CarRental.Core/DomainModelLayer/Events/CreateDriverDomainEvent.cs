using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.CarRental.Core.DomainModelLayer.Events
{
    public class CreateDriverDomainEvent: DomainEvent
    {
        public long DriverId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }


        public CreateDriverDomainEvent(long id, string name, string email)
        {
            this.DriverId = id;
            this.FirstName = name;
            this.LastName = email;
        }
    }
}
