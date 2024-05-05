using DDD.CarRental.Core.DomainModelLayer.Interfaces;
using DDD.CarRental.Core.DomainModelLayer.Models;
using System.Linq;

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
    }
}
