using DDD.CarRental.Core.DomainModelLayer.Models;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.CarRental.Core.DomainModelLayer.Events
{
    public class StartRentalDomainEvent : DomainEvent
    {
        public Rental rental { get; private set; }

        public StartRentalDomainEvent(Rental rent)
        {
            this.rental = rent;
        }
    }
}
