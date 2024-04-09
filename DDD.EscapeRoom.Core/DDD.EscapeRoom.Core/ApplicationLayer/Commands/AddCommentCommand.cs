using System;

namespace DDD.EscapeRoom.Core.ApplicationLayer.Commands
{
    public class AddCommentCommand
    {
        public long CommentId { get; set; }
        public DateTime Created { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public long PlayerId { get; set; }
        public long RoomId { get; set; }
    }
}
