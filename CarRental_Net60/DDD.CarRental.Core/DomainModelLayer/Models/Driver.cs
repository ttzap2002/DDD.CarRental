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
       

        // konstruktor na potrzeby serializacji
        protected Driver()
        { }
        public Driver(string licenceNumber, string firstName, string lastName, long ID)
            : base(ID)
        {
            
            LicenceNumber = licenceNumber;
            FirstName = firstName;
            LastName = lastName;
           
            this.AddDomainEvent(new CreateDriverDomainEvent(this.Id, this.FirstName.ToString(), this.LastName.ToString()));
        }

    }

}
