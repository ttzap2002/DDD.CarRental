﻿using DDD.CarRental.Core.DomainModelLayer.Interfaces;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.CarRental.Core.DomainModelLayer.Policies
{
    internal class StandardDiscountPolicy : IDiscountPolicy
    {
        public string Name { get; protected set; }

        public StandardDiscountPolicy()
        {
            this.Name = "Standard discount policy";
        }

        public float CalculateDiscount(long numOfMinutes)
        {
            return numOfMinutes * (float)0.01;
        }
    }
}
