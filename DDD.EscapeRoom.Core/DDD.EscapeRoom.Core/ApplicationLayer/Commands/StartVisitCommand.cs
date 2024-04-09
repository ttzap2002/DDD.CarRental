using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.EscapeRoom.Core.ApplicationLayer.Commands
{
    public class StartVisitCommand
    {
        public long VisitId { get; set; }
        public long RoomId { get; set; }
        public long PlayerId { get; set; }
        public DateTime Started { get; set; }
    }
}
