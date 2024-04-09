using DDD.EscapeRoom.Core.DomainModelLayer.Models;
using DDD.SharedKernel.InfrastructureLayer;
using System;

namespace DDD.EscapeRoom.Core.DomainModelLayer.Interfaces
{
    public interface ICommentRepository: IRepository<Comment>
    {
        double GetSumOfRating(long roomId);
        long GetNumOfRating(long roomId);
    }
}
