using DDD.SharedKernel.DomainModelLayer.Implementations;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System;
using System.Collections.Generic;

namespace DDD.CarRental.Core.DomainModelLayer.Models
{
    public class Distance : ValueObject
    {
        public float Value { get; set; }
        public Unit DistanceUnit { get; set; }

        protected Distance() { }

        public Distance(float value, Unit unit)
        {
            Value = value;
            DistanceUnit = unit;
        }

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
