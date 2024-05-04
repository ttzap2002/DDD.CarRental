using DDD.CarRental.Core.DomainModelLayer.Factories;
using DDD.CarRental.Core.DomainModelLayer.Interfaces;
using DDD.CarRental.Core.DomainModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.CarRental.Core.ApplicationLayer.Commands.Handlers
{
    public class CommandHandler
    {
        private DiscountPolicyFactory _discountPolicyFactory;
        private ICarRentalUnitOfWork _unitOfWork;
        public CommandHandler(DiscountPolicyFactory discountPolicyFactory)
        {
            _discountPolicyFactory = discountPolicyFactory;
        }

        /*
        public void Execute(RentCarCommand command)
        {
            
            Car c = _unitOfWork.CarRepository.GetCar(command.CarId);
            if (c == null)
                throw new Exception($"Car '{command.CarId}' didn't exists.");
            Driver d = _dbContext.Drivers.Find(command.DriverId);
            if (d == null)
                throw new Exception($"Driver '{command.DriverId}' didn't exists.");
            Rental r = _dbContext.Rentals.FirstOrDefault(x => x.IDCar == command.CarId && x.Finished == null && x.Car.CarStatus == CarStatus.BUSY);
            if (r != null)
                throw new Exception($"Another driver already has used this car");
            c.CarStatus = CarStatus.BUSY;
            string driveFirstLastName = $"{d.FirstName} {d.LastName}";
            rental = new Rental
            {
                IDCar = command.CarId,
                RentalId = command.RentId,
                Started = DateTime.Now,
                IdDriver = command.DriverId

            };
            _dbContext.Rentals.Add(rental);
            var driverReadModel = new RentalReadModel
            {
                CarId = command.CarId,
                RentalId = command.RentId,
                Created = DateTime.Now,
                DriverId = command.DriverId,
                StartXPosition = c.XPosition,
                StratYPosition = c.YPosition,
                _Driver = driveFirstLastName,
                RegistrationNumber = c.RegistrationNumber,
                Total = 0
            };
            _dbContext.RentalReadModels.Add(driverReadModel);
            _dbContext.SaveChanges();

        }
        */


    }
}

