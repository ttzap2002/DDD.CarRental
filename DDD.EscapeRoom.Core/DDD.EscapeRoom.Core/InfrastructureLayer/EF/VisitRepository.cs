using DDD.EscapeRoom.Core.DomainModelLayer.Interfaces;
using DDD.EscapeRoom.Core.DomainModelLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DDD.EscapeRoom.Core.InfrastructureLayer.EF
{
    public class VisitRepository : Repository<Visit>, IVisitRepository
    {
        public VisitRepository(EscapeRoomDbContext context)
            : base(context)
        { }
    }
}
