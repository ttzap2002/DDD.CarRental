using DDD.EscapeRoom.Core.DomainModelLayer.Interfaces;
using DDD.SharedKernel.DomainModelLayer;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using System.Collections.Generic;
using System.Linq;

namespace DDD.EscapeRoom.Core.InfrastructureLayer.EF
{
    public class EscapeRoomUnitOfWork : IEscapeRoomUnitOfWork
    {
        private EscapeRoomDbContext _dbContext;
        private IDomainEventPublisher _eventPublisher;
        public IPlayerRepository PlayerRepository { get; protected set; }
        public IRoomRepository RoomRepository { get; protected set; }
        public IVisitRepository VisitRepository { get; protected set; }
        public ICommentRepository CommentRepository { get; protected set; }


        public EscapeRoomUnitOfWork(
            EscapeRoomDbContext context,
            IDomainEventPublisher eventPublisher,
            IPlayerRepository playerRepository,
            IRoomRepository roomRepository,
            IVisitRepository visitRepository,
            ICommentRepository commentRepository)
        {
            _dbContext = context;
            _eventPublisher = eventPublisher;
            PlayerRepository = playerRepository;
            RoomRepository = roomRepository;
            VisitRepository = visitRepository;
            CommentRepository = commentRepository;
        }

        public void Commit()
        {
            // select all changed entities
            var entities = _dbContext.ChangeTracker.Entries<Entity>()
                .Select(x => x.Entity);

            // select all events from entities
            var domainEvents = _dbContext.ChangeTracker.Entries<Entity>()
                .SelectMany(x => x.Entity.DomainEvents)
                .OrderBy(e => e.Created)
                .ToList();

            // publish event
            foreach (dynamic @event in domainEvents)
                _eventPublisher.Publish(@event);

            // remove events form lists
            foreach (var entity in entities)
                entity.RemoveAllDomainEvents();

            // save changes to database
            _dbContext.SaveChanges();
        }

        public void Dispose()
        { }
        public void RejectChanges()
        { }
    }
}
