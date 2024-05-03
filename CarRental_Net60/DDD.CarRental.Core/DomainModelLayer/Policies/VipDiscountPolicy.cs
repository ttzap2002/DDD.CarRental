using DDD.CarRental.Core.DomainModelLayer.Interfaces;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.CarRental.Core.DomainModelLayer.Policies
{
    public class VipDiscountPolicy : IDiscountPolicy
    {
        public string Name { get; protected set; }

        public VipDiscountPolicy()
        {
            this.Name = "Vip discount policy";
        }

        public Money CalculateDiscount(Money total, long numOfMinutes, Money unitPrice)
        {
            decimal discountPercent = 0;
            if (numOfMinutes < 75)
                discountPercent = 0.015m;
            else if (numOfMinutes < 120)
                discountPercent = 0.025m;
            else
                discountPercent = 0.05m;
            return total.MultiplyBy(discountPercent);
        }
    }
}
