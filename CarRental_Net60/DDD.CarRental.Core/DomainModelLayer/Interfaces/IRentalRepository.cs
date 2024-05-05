using DDD.CarRental.Core.DomainModelLayer.Models;
using DDD.SharedKernel.InfrastructureLayer;
using Microsoft.EntityFrameworkCore;

namespace DDD.CarRental.Core.DomainModelLayer.Interfaces
{
    public interface IRentalRepository : IRepository<Rental>
    {
        Rental GetRentalID(long rentalID);
    }

}
