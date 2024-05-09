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
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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
            long carid = 1;
            long driverid = 1;
            long rentalid = 1;
            long posiotionid = 1;

            void CreateDriver(long id, string licence, string firstname, string lastname)
            {
                _commandHandler.Execute(new CreateDriverCommand
                {
                    DriverId = id,
                    LicenceNumber = licence,
                    FirstName = firstname,
                    LastName = lastname,
                }); ;
            }

            void RentCar(long rentID, long drivId, long cId, DateTime started)
            {
                _commandHandler.Execute(new RentCarCommand()
                {
                    RentalId = rentID,
                    DriverId = drivId,
                    CarId = cId,
                    Started = started,
                });
            }

            void CreateCar(long id, Distance totalDistance, Position currentPosition, string registrationNumber)
            {
                _commandHandler.Execute(new CreateCarCommand()
                {
                    ID = id,
                    TotalDistance = totalDistance,
                    CurrentPosition = currentPosition,
                    RegistrationNumber = registrationNumber,
                });
            }

            void ReturnCar(int rentalId, DateTime finished)
            {
                _commandHandler.Execute(new ReturnCarCommand()
                {
                    RentalId = rentalId,
                    Finished = finished
                });
            }

            CreateDriver(driverid++, "AFDS-1", "first", "second");
            CreateDriver(driverid++, "AF1S-1", "Jan", "Lowak");
            CreateDriver(driverid++, "AFSA-41", "Mateusz", "Strojek");
            CreateDriver(driverid++, "BAIE-1", "ABC", "XYZ");
            CreateDriver(driverid++, "CAADG-23", "Karol", "Wiśniewski");
            CreateDriver(driverid++, "ADHD-1", "Radosław", "Mocarski");
            CreateDriver(driverid, "ADA2-32", "Tomasz", "Zapart");

            Console.WriteLine("Utworzono kierowców");

            Position pos = new Position(10f, 12f, Core.DomainModelLayer.Models.Unit.mile);
            Position pos1 = new Position(10.0002f, 12.001f, Core.DomainModelLayer.Models.Unit.kilometer);
            Position pos2 = new Position(-1f, -41f, Core.DomainModelLayer.Models.Unit.mile);
            Position pos3 = new Position(-31f, 12f, Core.DomainModelLayer.Models.Unit.meter);
            Position pos4 = new Position(2f, -12f, Core.DomainModelLayer.Models.Unit.mile);

            CreateCar(1, new Distance(100, Core.DomainModelLayer.Models.Unit.kilometer),pos, "Abc1233");
            CreateCar(2, new Distance(200000, Core.DomainModelLayer.Models.Unit.centimeter),pos1, "KSA1233");
            CreateCar(3, new Distance(2020, Core.DomainModelLayer.Models.Unit.kilometer), pos2, "KT1233");
            CreateCar(4, new Distance(0, Core.DomainModelLayer.Models.Unit.kilometer), pos3, "KDA1233");
            CreateCar(5, new Distance(21, Core.DomainModelLayer.Models.Unit.kilometer), pos4, "K11233");

            Console.WriteLine("Utworzono auta");


            _commandHandler.Execute(new CreatePrice() { Id = 1, StartTime = new DateTime(2020, 10, 5), UnitPrice = new Price(0.01m, "zł") });
            _commandHandler.Execute(new CreatePrice() { Id = 2, StartTime = new DateTime(2020, 10, 5), UnitPrice = new Price(0.05m, "zł") });
            _commandHandler.Execute(new CreatePrice() { Id = 3, StartTime = new DateTime(2020, 10, 5), UnitPrice = new Price(0.10m, "zł") });
            _commandHandler.Execute(new CreatePrice() { Id = 4, StartTime = new DateTime(2020, 10, 5), UnitPrice = new Price(0.20m, "zł") });

            RentCar(rentID:1, drivId:1, cId:1, DateTime.Now);

            Console.WriteLine("Utworzono wypożyczenie");

            ReturnCar(rentalId: 1, finished: DateTime.Now.AddDays(1));

            Console.WriteLine("Zwrócono wypożyczenie");

            RentCar(rentID:2, drivId:2, cId:2, DateTime.Now);
            ReturnCar(rentalId:2, finished: DateTime.Now.AddDays(2));

            RentCar(rentID:3, drivId:3, cId:3, DateTime.Now);
            RentCar(rentID:4, drivId:2, cId:4, DateTime.Now);
            RentCar(rentID:5, drivId:4, cId:5, DateTime.Now);

            // Sprawdzenie czy polityki działają
            RentCar(rentID: 6, drivId: 1, cId: 1, DateTime.Now);
            ReturnCar(rentalId: 6, finished: DateTime.Now.AddDays(1));
            RentCar(rentID: 7, drivId: 1, cId: 1, DateTime.Now);
            ReturnCar(rentalId: 7, finished: DateTime.Now.AddDays(1));
            RentCar(rentID: 8, drivId: 1, cId: 1, DateTime.Now);
            ReturnCar(rentalId: 8, finished: DateTime.Now.AddDays(1));
            RentCar(rentID: 9, drivId: 1, cId: 1, DateTime.Now);
            ReturnCar(rentalId: 9, finished: DateTime.Now.AddDays(1));
            RentCar(rentID: 10, drivId: 1, cId: 1, DateTime.Now);
            ReturnCar(rentalId: 10, finished: DateTime.Now.AddDays(1));
            RentCar(rentID: 11, drivId: 1, cId: 1, DateTime.Now);
            ReturnCar(rentalId: 11, finished: DateTime.Now.AddDays(1));
            RentCar(rentID: 12, drivId: 1, cId: 1, DateTime.Now);
            ReturnCar(rentalId: 12, finished: DateTime.Now.AddDays(1));
            RentCar(rentID: 13, drivId: 1, cId: 1, DateTime.Now);
            ReturnCar(rentalId: 13, finished: DateTime.Now.AddDays(1));
            RentCar(rentID: 14, drivId: 1, cId: 1, DateTime.Now);
            ReturnCar(rentalId: 14, finished: DateTime.Now.AddDays(1));
            RentCar(rentID: 15, drivId: 1, cId: 1, DateTime.Now);
            ReturnCar(rentalId: 15, finished: DateTime.Now.AddDays(1));
            RentCar(rentID: 16, drivId: 1, cId: 1, DateTime.Now);
            ReturnCar(rentalId: 16, finished: DateTime.Now.AddDays(1));
            RentCar(rentID: 17, drivId: 1, cId: 1, DateTime.Now);
            ReturnCar(rentalId: 17, finished: DateTime.Now.AddDays(1));

            //List<RentalDTO> rentalInParticularInterval = _queryHandler.Execute(new GetAllRentalInTimeInterval()
            //{ Start = DateTime.Now });

            //foreach(var r in rentalInParticularInterval) 
            //{
            //    Console.WriteLine(r.Started);
            //}

            Distance d = new(1, Unit.meter);
            Distance d1 = new(3, Unit.centimeter);
            Distance d2 = new(6, Unit.kilometer);

            
            // Test dodawania dystansu
            Console.WriteLine((d + d1 + d2).Value.ToString());

            void PrintResult(List<ITransactionObject> lista) 
                {
                    foreach (var item in lista)
                        Console.WriteLine(item.ToString());
                }

                List<ITransactionObject> car = _queryHandler.Execute(new GetAllCarsQuery()).Cast<ITransactionObject>().ToList();
                List<ITransactionObject> drivers = _queryHandler.Execute(new GetAllDriverQuery()).Cast<ITransactionObject>().ToList();
                List<ITransactionObject> rentals = _queryHandler.Execute(new GetAllRentalsQuery()).Cast<ITransactionObject>().ToList();

                PrintResult(car);
                PrintResult(drivers);
                PrintResult(rentals);
                CarDTO car1 = _queryHandler.Execute(new GetCarByRegistrationNumber() { RegistrationNumber = "KDA1233" });
                Console.WriteLine(car1);
                CarDTO car2 = _queryHandler.Execute(new GetCarByRegistrationNumber() { RegistrationNumber = "KDA4433" });
                Console.WriteLine(car2);

                List<ITransactionObject> allFreeCars = _queryHandler.Execute(new GetAllFreeCarsQuery()).Cast<ITransactionObject>().ToList();
                Console.WriteLine("====FREE CARS====");
                PrintResult(allFreeCars);
                DriverDTO driver = _queryHandler.Execute(new GetParticularDriver() { ID = 3});
                Console.WriteLine(driver);
                RentalDTO rental = _queryHandler.Execute(new GetParticularRental() { rentalId = 2 });
                Console.WriteLine(rental);
                List<ITransactionObject> AllRentalsInTime = _queryHandler.Execute(new GetAllRentalInTimeInterval()

                { Start = new DateTime(2020,10,10) , End = DateTime.Now  }).Cast<ITransactionObject>().ToList();
                Console.WriteLine("====rentals time bounded====");
                PrintResult(AllRentalsInTime);



        }
    }
}
