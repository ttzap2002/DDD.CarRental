﻿using DDD.CarRental.Core.DomainModelLayer.Models;
using DDD.SharedKernel.InfrastructureLayer;

namespace DDD.CarRental.Core.DomainModelLayer.Interfaces
{
    public interface IDriverRepository : IRepository<Driver>
    {
         Driver GetDriver(long id);
         Driver GetDriverByLicenceNumber(string licenceNumber);
    }

}
