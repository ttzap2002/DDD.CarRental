using System;
using System.Collections.Generic;

namespace DDD.EscapeRoom.Core.ApplicationLayer.Dto
{
    public class VisitDto
    {
        public long Id { get; set; }
        public DateTime Started { get; set; }
        public DateTime? Finished { get; set; }
        public int TimeInMinutes { get; set; }
        public string Total_Currency { get; set; }
        public decimal Total_Amount { get; set; }
        public long PlayerId { get; set; }
        public string PlayerName { get; set; }
        public long RoomId { get; set; }
        public string RoomName { get; set; }
    }
}