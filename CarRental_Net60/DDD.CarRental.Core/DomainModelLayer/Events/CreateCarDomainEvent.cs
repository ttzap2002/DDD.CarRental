using DDD.CarRental.Core.DomainModelLayer.Models;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.CarRental.Core.DomainModelLayer.Events
{
    public class CreateCarDomainEvent : DomainEvent
    {
        public long CarId { get; private set; }
        public string RegistrationNumber { get; private set; }
        public Position CurrentPosition { get; private set; }
        public Distance CurrentDistance { get; private set; }
        public Distance TotalDistance { get; private set; }
        public Status CarStatus { get; private set; }


        public CreateCarDomainEvent(long carId, string registrationNumber, Position currentPosition, Distance currentDistance, Distance totalDistance, Status carStatus)
        {
            CarId = carId;
            RegistrationNumber = registrationNumber;
            CurrentPosition = currentPosition;
            CurrentDistance = currentDistance;
            TotalDistance = totalDistance;
            CarStatus = carStatus;
        }
    }
}
