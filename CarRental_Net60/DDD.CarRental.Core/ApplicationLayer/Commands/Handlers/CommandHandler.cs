using DDD.CarRental.Core.DomainModelLayer.Factories;
using DDD.CarRental.Core.DomainModelLayer.Interfaces;
using DDD.CarRental.Core.DomainModelLayer.Models;
using DDD.SharedKernel.InfrastructureLayer;
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

        public void Execute(RentCarCommand command)
        {
            
            Car c = _unitOfWork.CarRepository.Get(command.CarId);
            if (c == null)
                throw new Exception($"Car '{command.CarId}' didn't exists.");
            Driver d = _unitOfWork.DriverRepository.Get(command.DriverId);
            if (d == null)
                throw new Exception($"Driver '{command.DriverId}' didn't exists.");
            if (c.CarStatus != Status.free)
                throw new Exception($"This car is not avalaible");
            if (command.Started < DateTime.Now)
                throw new Exception($"Start data cannot be  avalaible");
            c.CarStatus = Status.reserved;
            string driveFirstLastName = $"{d.FirstName} {d.LastName}";
            Rental rental = new Rental
            {
                CarId = command.CarId,
                Started = DateTime.Now,
                DriverId = command.DriverId
            };
            _unitOfWork.RentalRepository.Insert(rental);
            _unitOfWork.Commit();
        }

        public void Execute(CreateDriverCommand command)
        {
            Driver driver = _unitOfWork.DriverRepository.GetDriver(command.DriverId);

            if (driver != null)
                throw new InvalidOperationException($"Driver '{command.DriverId}' already exists.");

            driver = _unitOfWork.DriverRepository.GetDriverByLicenceNumber(command.LicenceNumber);
            if (driver != null)
                throw new InvalidOperationException($"Driver with licence number '{command.LicenceNumber}' already exists.");


            driver = new Driver(command.LicenceNumber, command.FirstName, command.LastName, command.DriverId);

            _unitOfWork.DriverRepository.Insert(driver);
            _unitOfWork.Commit();
        }

    }
}

