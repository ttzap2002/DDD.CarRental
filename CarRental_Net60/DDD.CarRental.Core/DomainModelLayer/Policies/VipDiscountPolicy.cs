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

        public float CalculateDiscount(long numOfMinutes)
        {
           
            float minutes = 0;

            if(numOfMinutes < 7200)
            {
                minutes = (float)numOfMinutes * (float)0.01;
            }
            else
            {
                minutes = (float)(Math.Log(numOfMinutes) * (float)4);
            }

            return minutes;
        }
    }
}
