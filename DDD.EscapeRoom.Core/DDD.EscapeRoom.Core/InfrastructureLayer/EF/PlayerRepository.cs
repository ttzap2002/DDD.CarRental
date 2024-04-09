using DDD.EscapeRoom.Core.DomainModelLayer.Interfaces;
using DDD.EscapeRoom.Core.DomainModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DDD.EscapeRoom.Core.InfrastructureLayer.EF
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    { 
        public PlayerRepository(EscapeRoomDbContext context)
            :base(context)
        { }

        public Player GetPlayerByName(string name)
        {
            return _context.Players
                .Where(p => p.Name == name)
                .FirstOrDefault();
        }
    }
}
