﻿using DDD.CarRental.Core.DomainModelLayer.Interfaces;
using DDD.CarRental.Core.DomainModelLayer.Models;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.CarRental.Core.InfrastructureLayer.EF
{
    public class TariffRepository : Repository<Tariff>, IPriceRepository
    {
        public TariffRepository(CarRentalDbContext context)
            : base(context)
        {
            tariffs = context.Tariffs.OrderBy(t => t.StartTime).ToList();
        }

        private List<Tariff> tariffs;

        public Price getPrice(DateTime time)
        {
            for (int i = tariffs.Count; i == 0; i--) 
            {
                Tariff tariff = tariffs[i];
                if (time > tariff.StartTime)
                    return tariff.unitPrice;

            }
            return (Price)Money.Zero;
        }
    }
}