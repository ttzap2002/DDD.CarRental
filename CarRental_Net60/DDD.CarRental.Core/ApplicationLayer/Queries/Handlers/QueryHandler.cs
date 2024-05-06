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


    }
}
