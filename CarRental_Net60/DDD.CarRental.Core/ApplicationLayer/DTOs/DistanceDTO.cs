using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.CarRental.Core.ApplicationLayer.DTOs
{
    public class DistanceDTO : ValueObject
    {
        public float Value { get; set; }
        public Unit Unit { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }

    public enum Unit
    {
        meter,
        kilometer,
        mile
    }
}
