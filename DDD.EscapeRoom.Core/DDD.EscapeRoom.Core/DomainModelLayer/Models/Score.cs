using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Collections.Generic;

namespace DDD.EscapeRoom.Core.DomainModelLayer.Models
{
    public class Score: ValueObject
    {
        public string Player { get; protected set; }
        public int TimeInMinutes { get; protected set; }
        public DateTime Created {get; protected set;}

        // konstruktor na potrzeby serializacji
        protected Score()
        { }

        

        public Score(long playerId, string playerName, int timeInMinutes, DateTime created)
        {
            this.Player = playerName + " (" + playerId + ")";
            this.TimeInMinutes = timeInMinutes;
            this.Created = created;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Player.ToUpper();
            yield return TimeInMinutes;
            yield return Created;
        }

        public int Compare(Score s)
        {
            return this.TimeInMinutes.CompareTo(s.TimeInMinutes);
        }

        public static bool operator <(Score m, Score m2)
        {
            return m.TimeInMinutes.CompareTo(m2.TimeInMinutes) < 0;
        }

        public static bool operator >(Score m, Score m2)
        {
            return m.TimeInMinutes.CompareTo(m2.TimeInMinutes) > 0;
        }

        public static bool operator >=(Score m, Score m2)
        {
            return m.TimeInMinutes.CompareTo(m2.TimeInMinutes) >= 0;
        }

        public static bool operator <=(Score m, Score m2)
        {
            return m.TimeInMinutes.CompareTo(m2.TimeInMinutes) <= 0;
        }
    }
}