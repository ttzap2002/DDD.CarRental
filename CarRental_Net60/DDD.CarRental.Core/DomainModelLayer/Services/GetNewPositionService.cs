using DDD.CarRental.Core.DomainModelLayer.Calculation;
using DDD.CarRental.Core.DomainModelLayer.Interfaces;
using DDD.CarRental.Core.DomainModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.CarRental.Core.DomainModelLayer.Services
{
    public class GetNewPositionService : IGetNewPositionService
    {
        public Position GetPosition(long id, Rental r, Car c)
        {
            Rental rent = r;
            Car car = c;
            Position position = car.CurrentPosition;
            Position newposition = new Position(21,3, position.Unit);

            Random rand = new Random();

            float parameter = 10000;
            float coefficient = UnitConverter.Converter(parameter, Unit.meter, car.CurrentPosition.Unit).Item1;

            newposition.X = position.X + (float)(rand.NextDouble()) * (float)coefficient;
            newposition.Y = position.Y + (float)(rand.NextDouble()) * (float)coefficient;

            return newposition;
        }
    }
}
