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
    }
}
