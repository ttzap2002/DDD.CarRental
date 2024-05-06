using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.CarRental.Core.DomainModelLayer.Models;

namespace DDD.CarRental.Core.ApplicationLayer.DTOs
{
    public class DistanceDTO : ValueObject
    {
        public float Value { get; set; }
        public Unit unit { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            switch (unit)
            {
                case Unit.meter:
                    yield return Value;
                    break;
                case Unit.mile:
                    yield return Value * 1609.344;
                    break;
                case Unit.kilometer:
                    yield return Value*1000;
                    break;
            }
        }
    }
}
