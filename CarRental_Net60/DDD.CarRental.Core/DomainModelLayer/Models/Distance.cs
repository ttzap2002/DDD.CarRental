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
            switch (DistanceUnit)
            {
                case Unit.meter:
                    yield return Value;
                    break;
                case Unit.mile:
                    yield return Value * 1609.344;
                    break;
                case Unit.kilometer:
                    yield return Value * 1000;
                    break;
            }
        }
    }


    public enum Unit
    {
        meter,
        kilometer,
        mile
    }

}
