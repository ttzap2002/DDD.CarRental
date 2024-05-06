using DDD.CarRental.Core.ApplicationLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using DDD.CarRental.Core.DomainModelLayer.Models;

namespace DDD.CarRental.Core.ApplicationLayer.Mappers
{
    public class Mapper
    {
        // ToDo: zaimplementwać mapery obiektów biznesowych na transferowe

        public CarDTO Map(Car car)
        {
            return new CarDTO
            {
                Id = car.Id,
                RegistrationNumber = car.RegistrationNumber,
                CurrentPosition = car.CurrentPosition,
                CurrentDistance = car.CurrentDistance,
                TotalDistance = car.TotalDistance
            };
        }
    }
}
