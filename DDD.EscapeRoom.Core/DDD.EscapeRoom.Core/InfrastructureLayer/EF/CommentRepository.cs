using DDD.EscapeRoom.Core.DomainModelLayer.Interfaces;
using DDD.EscapeRoom.Core.DomainModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DDD.EscapeRoom.Core.InfrastructureLayer.EF
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(EscapeRoomDbContext context)
            : base(context)
        { }

        public double GetSumOfRating(long roomId)
        {
            return _context.Comments
                .Where(x => x.RoomId == roomId)
                .Sum(x => x.Rating);
        }

        public long GetNumOfRating(long roomId)
        {
            return _context.Comments
                .Where(x => x.RoomId == roomId)
                .Count();
        }
    }
}
