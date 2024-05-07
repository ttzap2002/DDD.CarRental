using DDD.CarRental.Core.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.CarRental.Core.DomainModelLayer.Calculation
{
    public static class UnitConverter
    {

        private static readonly Dictionary<Unit,float> table = new Dictionary<Unit, float>() 
        {
            { Unit.meter, 1.0f }, // Base unit
            { Unit.kilometer, 1000.0f },
            { Unit.centimeter, 0.01f },
            { Unit.millimeter, 0.001f },
            { Unit.mile, 1609.344f },
            { Unit.inch, 0.0254f },    
            { Unit.foot , 1609.344f },     
            { Unit.yard, 0.9144f }


        };

        public static (float,Unit) Converter(float val, Unit unit, Unit targetUnit) 
        {
            float valToMeter = table[unit] * val;
            float finalVal = valToMeter / table[targetUnit];
            return (finalVal,targetUnit);
        }

    }
}
