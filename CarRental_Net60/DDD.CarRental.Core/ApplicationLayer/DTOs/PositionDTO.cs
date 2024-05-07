using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.CarRental.Core.DomainModelLayer.Models;

namespace DDD.CarRental.Core.ApplicationLayer.DTOs
{
    public class PositionDTO : ValueObject, ITransactionObject
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

        protected PositionDTO()
        { }
    }
}
