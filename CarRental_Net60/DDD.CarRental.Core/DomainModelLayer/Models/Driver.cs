using DDD.CarRental.Core.DomainModelLayer.Events;
using DDD.SharedKernel.DomainModelLayer;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;

namespace DDD.CarRental.Core.DomainModelLayer.Models
{
    public class Driver : Entity, IAggregateRoot
    {
        
        public string LicenceNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public float FreeMinutes { get; set; }
        private List<Rental> rentals;

        public IEnumerable<Rental> Rentals
        {
            get { return rentals.AsReadOnly(); }
        }

        // konstruktor na potrzeby serializacji
        protected Driver()
        { }

        public Driver(string licenceNumber, string firstName, string lastName)
            : base()
        {
            if (String.IsNullOrEmpty(FirstName)) throw new ArgumentNullException("Driver first name is null or empty");
            if (String.IsNullOrEmpty(LastName)) throw new ArgumentNullException("Driver last name is null or empty");

            

            this.AddDomainEvent(new CreateDriverDomainEvent(this.Id, this.FirstName.ToString(), this.LastName.ToString()));
        }


    }

}
