using DDD.EscapeRoom.Core.DomainModelLayer.Models;
using DDD.SharedKernel.InfrastructureLayer;
using System.Collections;
using System.Collections.Generic;

namespace DDD.EscapeRoom.Core.DomainModelLayer.Interfaces
{
    public interface IRoomRepository : IRepository<Room>
    {
        Room GetRoomByName(string name);
        Room GetRoomById(long id);
        IList<Room> GetAllRooms();
    }
}
