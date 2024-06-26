﻿using DDD.CarRental.Core.DomainModelLayer.Models;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using DDD.SharedKernel.DomainModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.CarRental.Core.ApplicationLayer.DTOs
{
    public class RentalDTO : ITransactionObject
    {
        public long Id { get; set; }
        public DateTime Started { get; set; }

        public DateTime? Finished { get; set; }
        public long CarId { get; set; }
        public long DriverId { get; set; }
        public Money MoneyForRental { get; set; }

        public override string ToString()
        {
            return $"Rental {Id} started in {Started} will cost {MoneyForRental}";
        }
    }
}
