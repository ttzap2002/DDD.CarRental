using System;
using DDD.EscapeRoom.Core.DomainModelLayer.Models;
using DDD.SharedKernel.InfrastructureLayer;

namespace DDD.EscapeRoom.Core.DomainModelLayer.Interfaces
{
    public interface IEscapeRoomUnitOfWork : IUnitOfWork, IDisposable
    {
        IPlayerRepository PlayerRepository { get;  }
        IRoomRepository RoomRepository { get; }
        IVisitRepository VisitRepository { get; }
        ICommentRepository CommentRepository { get; }
    }
}
