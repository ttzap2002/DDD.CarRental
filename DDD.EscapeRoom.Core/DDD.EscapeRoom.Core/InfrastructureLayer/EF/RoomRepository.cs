using DDD.EscapeRoom.Core.DomainModelLayer.Interfaces;
using DDD.EscapeRoom.Core.DomainModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DDD.EscapeRoom.Core.InfrastructureLayer.EF
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        public RoomRepository(EscapeRoomDbContext context)
            : base(context)
        { }

        public Room GetRoomByName(string name)
        {
            return _context.Rooms
                .Include(r => r.Scores)
                .Where(p => p.Name == name)
                .FirstOrDefault();
        }

        public Room GetRoomById(long id)
        {
            // zwraca agregat pokój (razem ze Scores)
            return _context.Rooms
                .Include(r => r.Scores)
                .Where(r => r.Id == id)
                .FirstOrDefault();
        }

        public IList<Room> GetAllRooms()
        {
            // zwraca listę wszystkich pokoji (razem ze Scores)
            return _context.Rooms
                .Include(r => r.Scores)
                .ToList();
        }

    }
}
