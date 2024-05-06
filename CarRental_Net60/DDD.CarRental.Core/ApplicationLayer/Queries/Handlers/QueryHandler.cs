using System;
using System.Collections.Generic;
using System.Text;
using DDD.CarRental.Core.InfrastructureLayer.EF;
using DDD.CarRental.Core.ApplicationLayer.Mappers;
using DDD.CarRental.Core.ApplicationLayer.DTOs;
using DDD.CarRental.Core.DomainModelLayer.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Channels;

namespace DDD.CarRental.Core.ApplicationLayer.Queries.Handlers
{
    public class QueryHandler
    {
        private CarRentalDbContext _dbContext;
        private Mapper _mapper;

        public QueryHandler(CarRentalDbContext context, Mapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public List<CarDTO> Execute(GetAllCarsQuery query)
        {
            var cars = _dbContext.Cars
                .AsNoTracking()
                .ToList();

            List<CarDTO> result = cars.Select(r => this._mapper.Map(r)).ToList();
            return result;
        }


        public List<DriverDTO> Execute(GetAllDriverQuery query)
        {
            var drivers = _dbContext.Drivers
                .AsNoTracking()
                .ToList();

            // mapowanie obiektów biznesowych na transferowe warto powierzyć maperom 
            // (własnym - jak tutaj lub bibliotecznym, np. Automaper)
            List<DriverDTO> result = drivers.Select(r => this._mapper.Map(r)).ToList();
            return result;
        }

        public List<RentalDTO> Execute(GetAllRentalsQuery query)
        {
            var rentals = _dbContext.Rentals
                .AsNoTracking()
                .ToList();

            // mapowanie obiektów biznesowych na transferowe warto powierzyć maperom 
            // (własnym - jak tutaj lub bibliotecznym, np. Automaper)
            List<RentalDTO> result = rentals.Select(r => this._mapper.Map(r)).ToList();
            return result;
        }

        public List<CarDTO> Execute(GetAllFreeCarsQuery quey)
        {
            var cars = _dbContext.Cars
                .AsNoTracking()
                .Where(p => p.CarStatus == 0)
                .ToList();

            List<CarDTO> result = cars.Select(r => this._mapper.Map(r)).ToList();
            return result;
        }



        public CarDTO Execute(GetCarByRegistrationNumber query)
        {
            var Car = _dbContext.Cars.FirstOrDefault(x => x.RegistrationNumber == query.RegistrationNumber);

            // mapowanie obiektów biznesowych na transferowe warto powierzyć maperom 
            // (własnym - jak tutaj lub bibliotecznym, np. Automaper)
            if (Car == null)
            {
                return null;
            }
            return this._mapper.Map(Car);
        }

        public List<RentalDTO> Execute(GetAllRentalInTimeInterval query)
        {
            DateTime end = query.End;
            if (end == DateTime.MinValue)
            {
                end = DateTime.MaxValue;
            }
            var rental = _dbContext.Rentals.Where(x => x.Started >= query.Start && x.Started <= end).ToList();
            if (rental == null)
            {
                throw new Exception($"Could not find rentals between {query.Start} and {query.End}");
            }
            List<RentalDTO> rentalDto = rental.Select(x => this._mapper.Map(x)).ToList();
            return rentalDto;
        }


        public RentalDTO Execute(GetParticularRental query)
        {
            var rental = _dbContext.Rentals.FirstOrDefault(x => x.Id == query.rentalId);
            if (rental == null)
            {
                throw new Exception($"Could not find rental '{query.rentalId}'");
            }
            return this._mapper.Map(rental);
        }

        public DriverDTO Execute(GetParticularDriver query)
        {
            var driver = _dbContext.Drivers.FirstOrDefault(x => x.Id == query.ID);
            if (driver == null)
            {
                throw new Exception($"Could not find driver '{query.ID}'");
            }
            return this._mapper.Map(driver);
        }






    }
}
