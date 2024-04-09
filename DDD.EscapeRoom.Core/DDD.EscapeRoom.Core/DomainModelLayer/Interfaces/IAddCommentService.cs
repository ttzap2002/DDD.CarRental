using DDD.EscapeRoom.Core.DomainModelLayer.Models;
using DDD.SharedKernel.DomainModelLayer;
using System;

namespace DDD.EscapeRoom.Core.DomainModelLayer.Interfaces
{
    public interface IAddCommentService : IDomainService
    {
        void AddComment(long id, string title, string text, int rating, DateTime created, Room room, Player player);
    }
}