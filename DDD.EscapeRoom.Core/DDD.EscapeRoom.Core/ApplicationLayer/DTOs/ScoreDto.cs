using System;

namespace DDD.EscapeRoom.Core.ApplicationLayer.Dto
{
    public class ScoreDto
    {
        public string Player { get; set; }
        public int TimeInMinutes { get; set; }
        public DateTime Created { get; set; }
    }
}