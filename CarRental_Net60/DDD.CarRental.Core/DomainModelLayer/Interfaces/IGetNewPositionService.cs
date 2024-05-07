using DDD.CarRental.Core.DomainModelLayer.Models;
using DDD.SharedKernel.DomainModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DDD.CarRental.Core.DomainModelLayer.Interfaces
{
    public interface IGetNewPositionService : IDomainService
    {
        Position GetPosition(long id, Rental r, Car c);
    }
}
