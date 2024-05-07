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
                    yield return Value *1000;
                    break;
                case Unit.mile:
                    yield return Value * 1609344;
                    break;
                case Unit.kilometer:
                    yield return Value * 1000000;
                    break;
                case Unit.centimeter:
                    yield return Value * 10;
                    break;
                case Unit.millimeter:
                    yield return Value;
                    break;
                case Unit.inch:
                    yield return Value * 25.4;
                    break;
                case Unit.foot:
                    yield return Value * 304.8;
                    break;
                case Unit.yard:
                    yield return Value * 914.4;
                    break;
            }
        }

        private float CalculateToMM()
        {
            switch (DistanceUnit)
            {
                case Unit.meter:
                    return Value * 1000;
                case Unit.mile:
                    return Value * 1609344;
                case Unit.kilometer:
                    return Value * 1000000;
                case Unit.centimeter:
                    return Value * 10;
                case Unit.millimeter:
                    return Value;
                case Unit.inch:
                    return (float)(Value * 25.4);
                case Unit.foot:
                    return (float)(Value * 304.8);
                case Unit.yard:
                    return (float)(Value * 914.4);
            }
            throw new NotImplementedException();
        }

        public static Distance operator +(Distance m, Distance m2)
        {
            if (!AreCompatibleCurrencies(m, m2))
            {
                m.Value = m.CalculateToMM() / 1000000;
                m.DistanceUnit = Unit.kilometer;
                m2.Value = m2.CalculateToMM();
                m2.DistanceUnit = Unit.kilometer;
            }
            return new Distance(m.Value + m2.Value, m.DistanceUnit);
        }

        public static Distance operator -(Distance m, Distance m2)
        {
            if (!AreCompatibleCurrencies(m, m2))
            {
                m.Value = m.CalculateToMM()/1000000;
                m.DistanceUnit = Unit.kilometer;
                m2.Value = m2.CalculateToMM();
                m2.DistanceUnit = Unit.kilometer;
            }
            return new Distance(m.Value - m2.Value, m.DistanceUnit);
        }

        public Distance MultiplyBy(double multiplier)
        {
            return MultiplyBy((float)multiplier);
        }
        public Distance MultiplyBy(int multiplier)
        {
            return MultiplyBy((float)multiplier);
        }

        public Distance MultiplyBy(decimal multiplier)
        {
            return MultiplyBy((float)multiplier);
        }

        public Distance MultiplyBy(float multiplier)
        {
            return new Distance((Value * multiplier), DistanceUnit);
        }

        /// <summary>
        /// Currency is compatible if the same or either money object has zero value.
        /// </summary>
        private static bool AreCompatibleCurrencies(Distance m, Distance m2)
        {
            return IsZero((decimal)m.Value) || IsZero((decimal)m2.Value) || m.DistanceUnit.Equals(m2.DistanceUnit);
        }

        private static bool IsZero(decimal testedValue)
        {
            return decimal.Zero.CompareTo(testedValue) == 0;
        }

        public static bool operator <(Distance m, Distance m2)
        {
            return m.Value.CompareTo(m2.Value) < 0;
        }

        public static bool operator >(Distance m, Distance m2)
        {
            return m.Value.CompareTo(m2.Value) > 0;
        }

        public static bool operator >=(Distance m, Distance m2)
        {
            return m.Value.CompareTo(m2.Value) >= 0;
        }

        public static bool operator <=(Distance m, Distance m2)
        {
            return m.Value.CompareTo(m2.Value) <= 0;
        }

        public override string ToString()
        {
            return $"{Value}{DistanceUnit}";
        }
    }


    public enum Unit
    {
        meter = 0,
        kilometer = 1,
        centimeter = 2,
        millimeter = 3,
        mile = 4,
        inch = 5,
        foot = 6,
        yard = 7    

    }

}
