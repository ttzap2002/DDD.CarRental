using DDD.CarRental.Core.DomainModelLayer.Interfaces;
using DDD.CarRental.Core.DomainModelLayer.Models;
using DDD.CarRental.Core.DomainModelLayer.Policies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.CarRental.Core.DomainModelLayer.Factories
{
    public class DiscountPolicyFactory
    {
        public IDiscountPolicy Create(int rentals)
        {
            IDiscountPolicy returner = new StandardDiscountPolicy();



            int rentalsCount = rentals; 

            if (rentalsCount>7 && rentalsCount % 2 == 0)
                returner = new VipDiscountPolicy();

            return returner;
        }
    }
}
