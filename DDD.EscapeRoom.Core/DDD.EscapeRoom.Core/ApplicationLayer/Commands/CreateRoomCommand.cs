using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.EscapeRoom.Core.ApplicationLayer.Commands
{
    public enum RoomLevelCommand
    {
        Beginner = 0,
        Easy,
        Normal,
        Hard,
    }

    public enum RoomStatusCommand
    {
        Free = 0,
        Busy = 1,
        Closed = 2
    }

    public class CreateRoomCommand
    {
        public long RoomId { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public RoomLevelCommand Level { get; set; }
        public RoomStatusCommand Status { get; set; }
    }
}
