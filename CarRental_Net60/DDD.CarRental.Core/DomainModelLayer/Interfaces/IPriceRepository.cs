using DDD.CarRental.Core.DomainModelLayer.Models;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using DDD.SharedKernel.InfrastructureLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.CarRental.Core.DomainModelLayer.Interfaces
{
    public interface IPriceRepository: IRepository<Tariff>
    {
    
        void addTariff(Tariff tariff);
        Money getPrice(DateTime time);
    }
}
