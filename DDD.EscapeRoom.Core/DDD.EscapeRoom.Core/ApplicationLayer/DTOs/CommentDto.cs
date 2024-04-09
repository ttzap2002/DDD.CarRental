using System;

namespace DDD.EscapeRoom.Core.ApplicationLayer.Dto
{
    public class CommentDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public DateTime Created { get; set; }

        public long RoomId { get; set; }
        public long PlayerId { get; set; }

    }
}