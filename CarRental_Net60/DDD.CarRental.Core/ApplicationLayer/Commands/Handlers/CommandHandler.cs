using DDD.CarRental.Core.DomainModelLayer.Factories;
using DDD.CarRental.Core.DomainModelLayer.Interfaces;
using DDD.CarRental.Core.DomainModelLayer.Models;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using DDD.SharedKernel.InfrastructureLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Text;

namespace DDD.CarRental.Core.ApplicationLayer.Commands.Handlers
{
    public class CommandHandler
    {
        private DiscountPolicyFactory _discountPolicyFactory;
        private ICarRentalUnitOfWork _unitOfWork;
        public CommandHandler(ICarRentalUnitOfWork UnitOFWORK, DiscountPolicyFactory discountPolicyFactory)
        {
            _discountPolicyFactory = discountPolicyFactory;
            _unitOfWork = UnitOFWORK;
        }

        public void Execute(CreateCarCommand command)
        {
            if (command == null)
                throw new Exception("Command not added.");
            Car c = this._unitOfWork.CarRepository.Get(command.ID);
            if (c != null)
                throw new Exception($"Car {command.ID} already exist.");
            c = this._unitOfWork.CarRepository.GetCarByRegistrationNumber(command.RegistrationNumber);
            if (c != null)
                throw new Exception($"Car with registration {command.RegistrationNumber} already exist.");

            c = new Car(command.ID, command.RegistrationNumber, command.CurrentPosition, command.TotalDistance);

            this._unitOfWork.CarRepository.Insert(c);
            this._unitOfWork.Commit();
        }

        public void Execute(RentCarCommand command)
        {

            Car c = _unitOfWork.CarRepository.Get(command.CarId);
            if (c == null)
                throw new Exception($"Car '{command.CarId}' didn't exists.");
            Driver d = _unitOfWork.DriverRepository.Get(command.DriverId);
            if (d == null)
                throw new Exception($"Driver '{command.DriverId}' didn't exists.");
            if (_unitOfWork.RentalRepository.Get(command.RentalId) != null)
                throw new Exception($"Repository is currently exists.");
            if (c.CarStatus != Status.free)
                throw new Exception($"This car is not avalaible");

            int CountRentals = _unitOfWork.RentalRepository.GetDriverRentalsCount(command.DriverId);
            IDiscountPolicy policy = this._discountPolicyFactory.Create(CountRentals);

            Rental rental = new Rental(command.RentalId, command.Started, command.CarId, command.DriverId);

            rental.StartRental(c, c.CurrentPosition);
            rental.RegisterPolicy(policy);

            _unitOfWork.RentalRepository.Insert(rental);
            _unitOfWork.Commit();
        }
        public void Execute(CreateDriverCommand command)
        {
            Driver? driver = _unitOfWork.DriverRepository.GetDriver(command.DriverId);

            if (driver != null)
                throw new InvalidOperationException($"Driver '{command.DriverId}' already exists.");

            driver = _unitOfWork.DriverRepository.GetDriverByLicenceNumber(command.LicenceNumber);
            if (driver != null)
                throw new InvalidOperationException($"Driver with licence number '{command.LicenceNumber}' already exists.");


            driver = new Driver(command.LicenceNumber, command.FirstName, command.LastName, command.DriverId);

            _unitOfWork.DriverRepository.Insert(driver);
            _unitOfWork.Commit();
        }

        public void Execute(ReturnCarCommand command)
        {

            Rental r = _unitOfWork.RentalRepository.GetRentalID(command.RentalId);
            if (r == null)
                throw new Exception($"Rental with ID {command.RentalId} doesn't exist");

            Car c = _unitOfWork.CarRepository.Get(r.CarId);
            if (c == null)
                throw new Exception($"Car '{r.CarId}' didn't exists.");
            Driver d = _unitOfWork.DriverRepository.Get(r.DriverId);
            if (d == null)
                throw new Exception($"Driver '{r.DriverId}' didn't exists.");
            
            if (c.CarStatus != Status.rental)
                throw new Exception($"This car is not rented");
            Money m = _unitOfWork.PriceRepository.getPrice(r.Started);
            Position p = _unitOfWork.RentalRepository.GetFinishedPosition(command.RentalId);

            r.FinishRental(c,d,command.Finished, m, p);
            _unitOfWork.Commit();
        }

        public void Execute(CreatePrice command)
        {
            Tariff t = _unitOfWork.PriceRepository.Get(command.Id);
            if (t != null)
                throw new Exception($"Tarrif with ID {command.Id} already exist");

            t = new Tariff(command.Id, command.StartTime, command.UnitPrice);
            _unitOfWork.PriceRepository.Insert(t);
            _unitOfWork.PriceRepository.addTariff(t);
            _unitOfWork.Commit();
        }
    }
}

