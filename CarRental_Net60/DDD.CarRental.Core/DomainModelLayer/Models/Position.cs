using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace DDD.CarRental.Core.DomainModelLayer.Models
{
    public class Position : ValueObject
    {
    
        public float X { get; set; }
        public float Y { get; set; }

        public Unit Unit { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return X;
            yield return Y;
            yield return Unit;
        }

        protected Position()
        { }

        public Position(float x, float y, Unit un)
        {
            X = x;
            Y = y;
            Unit = un;
        }

        public Distance CalculateDistance(Position d)
        {
            float squaredResult = (d.X - X ) * (d.X - X) + (d.Y - Y) * (d.Y - Y);
            return new Distance((float)Math.Sqrt(squaredResult),Unit);
        }

    }

}
