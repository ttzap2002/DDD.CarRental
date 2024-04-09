using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.EscapeRoom.Core.ApplicationLayer.Commands
{
    public class StopVisitCommand
    {
        public long VisitId { get; set; }
        public DateTime Finished { get; set; }
    }
}
