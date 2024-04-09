using DDD.SharedKernel.DomainModelLayer;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DDD.EscapeRoom.Core.DomainModelLayer.Models
{
    public class Comment: Entity, IAggregateRoot
    {
        public string Title { get; protected set; }
        public string Text { get; protected set; }
        public int Rating { get; protected set; }
        public DateTime Created { get; protected set; }
        public long RoomId { get; protected set; }
        public long PlayerId { get; protected set; }

        // konstruktor na potrzeby serializacji
        protected Comment() 
        { }

        public Comment(long commentId, string title, string text, int rating, DateTime created, long roomId, long playerId)
            : base(commentId)
        {
            if (String.IsNullOrEmpty(title)) throw new ArgumentNullException("Comment title is null or empty");
            if (String.IsNullOrEmpty(text)) throw new ArgumentNullException("Comment text is null or empty");

            // moderacja
            Moderate(title, text);

            this.Title = title;
            this.Text = text;
            this.Created = created;
            this.Rating = rating;
            this.RoomId = roomId;
            this.PlayerId = playerId;
        }

        private void Moderate(string title, string text)
        {
            List<string> forbiddenWords = new List<string>() { "Pomidor" };

            if (forbiddenWords.Any(s => title.Contains(s))) throw new ArgumentException("Comment title contains forbidden words");
            if (forbiddenWords.Any(s => text.Contains(s))) throw new ArgumentException("Comment text contains forbidden words");
        }
    }
}