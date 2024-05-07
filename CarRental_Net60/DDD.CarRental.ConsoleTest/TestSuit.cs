using DDD.CarRental.Core.ApplicationLayer.Commands;
using DDD.CarRental.Core.ApplicationLayer.Commands.Handlers;
using DDD.CarRental.Core.ApplicationLayer.DTOs;
using DDD.CarRental.Core.ApplicationLayer.Queries;
using DDD.CarRental.Core.ApplicationLayer.Queries.Handlers;
using DDD.CarRental.Core.DomainModelLayer.Models;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Unit = DDD.CarRental.Core.DomainModelLayer.Models.Unit;

namespace DDD.CarRental.ConsoleTest
{
    public class TestSuit
    {
        private IServiceProvider _serviceProvide;

        private CommandHandler _commandHandler;
        private QueryHandler _queryHandler;

        public TestSuit(IServiceCollection serviceCollection)
        {
            _serviceProvide = serviceCollection.BuildServiceProvider();
            _commandHandler = _serviceProvide.GetRequiredService<CommandHandler>();
            _queryHandler = _serviceProvide.GetRequiredService<QueryHandler>();
        }

        public void Run()
        {
            long carid = 11;
            long carid1 = 21;
            long driverid = 22;
            long driverid1 = 31;
            long rentalid = 32;
            long rentalid1 = 41;
            long posiotionid = 42;




            //tworzymy drivera
            _commandHandler.Execute(new CreateDriverCommand
            {
                DriverId = driverid,
                LicenceNumber = "Abc1233",
                FirstName = "first",
                LastName = "second",
            }); ;

            // pobieramy info o pokoju zagadek
            Console.WriteLine("Utworzono kierowcę");

            Position pos = new Position(
                10f, 12f, Core.DomainModelLayer.Models.Unit.mile);

            _commandHandler.Execute(new CreateCarCommand()
            {
                ID = carid1,
                TotalDistance = new Core.DomainModelLayer.Models.Distance(100, Core.DomainModelLayer.Models.Unit.kilometer),
                CurrentPosition = pos,
                RegistrationNumber = "FFGRKRTHTRRH",
            });

            Console.WriteLine("Utworzono auto");

            Position position = new(1,2, Unit.kilometer);

            _commandHandler.Execute(new RentCarCommand()
            {
                RentalId = rentalid,
                DriverId = driverid,
                CarId = carid1,
                Started = DateTime.Now,
                Position = position,
            });

            Console.WriteLine("Utworzono wypożyczenie");

            List<RentalDTO> rentalInParticularInterval = _queryHandler.Execute(new GetAllRentalInTimeInterval()
            { Start = DateTime.Now });

            foreach(var r in rentalInParticularInterval) 
            {
                Console.WriteLine(r.Started);
            }



            _commandHandler.Execute(new CreatePrice() { Id = 1, StartTime = new DateTime(2020, 10, 5), UnitPrice = new Price(0.01m, "zł") });

            _commandHandler.Execute(new ReturnCarCommand()
            {
                RentalId = rentalid,
                Finished = DateTime.Now.AddDays(1),
            });

        }
    }
}
