﻿using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.CarRental.Core.DomainModelLayer.Interfaces
{
    public interface IDiscountPolicy
    {
        string Name { get; }
        float CalculateDiscount(long numOfMinutes);
    }
}
