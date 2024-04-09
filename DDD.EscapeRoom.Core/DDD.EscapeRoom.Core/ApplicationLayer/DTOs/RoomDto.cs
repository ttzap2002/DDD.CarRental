using System;
using System.Collections.Generic;

namespace DDD.EscapeRoom.Core.ApplicationLayer.Dto
{
    public enum RoomStatusDto
    {
        Free = 0,
        Busy = 1,
        Closed = 2
    }

    public enum EscapeRoomLevelDto
    {
        Beginner = 0,
        Easy,
        Normal,
        Hard,
    }

    public class RoomDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public double AverageRating { get; set; }
        public EscapeRoomLevelDto Level { get; set; }
        public RoomStatusDto Status { get; set; }
        public List<ScoreDto> Scores { get; set; }
    }
}