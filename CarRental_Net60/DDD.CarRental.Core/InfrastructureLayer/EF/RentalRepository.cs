using DDD.CarRental.Core.DomainModelLayer.Calculation;
using DDD.CarRental.Core.DomainModelLayer.Interfaces;
using DDD.CarRental.Core.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace DDD.CarRental.Core.InfrastructureLayer.EF
{
    public class RentalRepository : Repository<Rental>, IRentalRepository
    {
        public RentalRepository(CarRentalDbContext context)
            : base(context)
        {
       
        }
        public Rental GetRentalID(long rentalID)
        {
            return _context.Rentals.Where(x => x.Id == rentalID).FirstOrDefault();
        }

        public int GetDriverRentalsCount(long driverID)
        {
            return _context.Rentals.Where(p=>p.DriverId == driverID).Count();   
        }

    }
}
