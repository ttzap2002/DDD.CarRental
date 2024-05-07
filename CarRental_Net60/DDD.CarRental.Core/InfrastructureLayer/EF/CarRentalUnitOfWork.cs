using DDD.CarRental.Core.DomainModelLayer.Interfaces;
using DDD.SharedKernel.DomainModelLayer;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DDD.CarRental.Core.InfrastructureLayer.EF
{
    public class CarRentalUnitOfWork : ICarRentalUnitOfWork
    {
        private CarRentalDbContext _dbContext;
        private IDomainEventPublisher _eventPublisher;
        public ICarRepository CarRepository { get; protected set; }
        public IDriverRepository DriverRepository { get; protected set; }
        public IRentalRepository RentalRepository { get; protected set; }

        public IPriceRepository PriceRepository { get; protected set; }


        public CarRentalUnitOfWork(
            CarRentalDbContext context,
            IDomainEventPublisher eventPublisher,
            ICarRepository carRepository,
            IDriverRepository driverRepository,
            IRentalRepository rentalRepository,
            IPriceRepository priceRepository)
        {
            _dbContext = context;
            _eventPublisher = eventPublisher;
            CarRepository = carRepository;
            DriverRepository = driverRepository;
            RentalRepository = rentalRepository;
            PriceRepository = priceRepository;
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
        {
            _dbContext.Dispose();
        }
        public void RejectChanges()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Modified || entry.State == EntityState.Added)
                {
                    entry.State = EntityState.Unchanged;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.Reload();
                }
            }
        }
    }
}
