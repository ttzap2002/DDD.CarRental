using DDD.EscapeRoom.Core.ApplicationLayer.Commands.Handlers;
using DDD.EscapeRoom.Core.ApplicationLayer.DomainEventListeners;
using DDD.EscapeRoom.Core.ApplicationLayer.Dto;
using DDD.EscapeRoom.Core.ApplicationLayer.Mappers;
using DDD.EscapeRoom.Core.ApplicationLayer.Queries.Handlers;
using DDD.EscapeRoom.Core.DomainModelLayer.Events;
using DDD.EscapeRoom.Core.DomainModelLayer.Factories;
using DDD.EscapeRoom.Core.DomainModelLayer.Interfaces;
using DDD.EscapeRoom.Core.DomainModelLayer.Models;
using DDD.EscapeRoom.Core.DomainModelLayer.Services;
using DDD.EscapeRoom.Core.InfrastructureLayer;
using DDD.EscapeRoom.Core.InfrastructureLayer.EF;
using DDD.SharedKernel.ApplicationLayer;
using DDD.SharedKernel.DomainModelLayer;
using DDD.SharedKernel.InfrastructureLayer.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace DDD.EscapeRoom.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // create and configure DI container
            IServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // create TestSuit & run scenario test
            var testSuit = new TestSuit(serviceCollection);
            testSuit.Run();
        }

        static private void ConfigureServices(IServiceCollection serviceCollection)
        {
            // intializing and registering EscapeRoomDbContext
            var context = TestUtils.InitializeEscapeRoomContext();
            serviceCollection.AddSingleton(context);

            // registering command and query handlers
            serviceCollection.AddSingleton<CommandHandler>();
            serviceCollection.AddSingleton<QueryHandler>();

            // registering event publisher and handlers
            serviceCollection.AddSingleton<IDomainEventPublisher, SimpleEventPublisher>();
            serviceCollection.AddSingleton<IEventHandler<PlayerCreatedDomainEvent>, SendEmailWhenPlayerCreatedDomainEventHandler>();
            serviceCollection.AddSingleton<IEventHandler<VisitStartedDomainEvent>, EnterToRoomWhenVisitStartedDomainEventHandler>();

            // registering infrastructure services
            serviceCollection.AddSingleton<IEmailDispatcher, EmailDispatcher>();

            // registering Unit of Work and repos
            serviceCollection.AddSingleton<IEscapeRoomUnitOfWork, EscapeRoomUnitOfWork>();
            serviceCollection.AddSingleton<IPlayerRepository, PlayerRepository>();
            serviceCollection.AddSingleton<IRoomRepository, RoomRepository>();
            serviceCollection.AddSingleton<IVisitRepository, VisitRepository>();
            serviceCollection.AddSingleton<ICommentRepository, CommentRepository>();

            // registering domain model services, factories
            serviceCollection.AddSingleton<DiscountPolicyFactory>();
            serviceCollection.AddSingleton<VisitFactory>();
            serviceCollection.AddSingleton<IAddCommentService, AddCommentService>();
            serviceCollection.AddSingleton<Mapper>();
        }
    }
}
