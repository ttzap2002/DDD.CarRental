using DDD.EscapeRoom.Core.DomainModelLayer.Models;
using DDD.SharedKernel.InfrastructureLayer;

namespace DDD.EscapeRoom.Core.DomainModelLayer.Interfaces
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Player GetPlayerByName(string name);
    }
}
