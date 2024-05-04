using DDD.CarRental.Core.DomainModelLayer.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.CarRental.Core.ApplicationLayer.Commands.Handlers
{
    public class CommandHandler
    {
        private DiscountPolicyFactory _discountPolicyFactory;

        public CommandHandler(DiscountPolicyFactory discountPolicyFactory)
        {
            _discountPolicyFactory = discountPolicyFactory;
        }

    }
}
