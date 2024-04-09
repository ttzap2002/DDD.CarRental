using DDD.EscapeRoom.Core.ApplicationLayer.Commands;
using DDD.EscapeRoom.Core.ApplicationLayer.Commands.Handlers;
using DDD.EscapeRoom.Core.ApplicationLayer.Dto;
using DDD.EscapeRoom.Core.ApplicationLayer.Queries;
using DDD.EscapeRoom.Core.ApplicationLayer.Queries.Handlers;
using DDD.EscapeRoom.Core.DomainModelLayer.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.EscapeRoom.ConsoleTest
{
    public class TestSuit
    {
        private  IServiceProvider _serviceProvide;

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
            long roomId = 11;
            long player1Id = 21;
            long player2Id = 22;
            long visit1Id = 31;
            long visit2Id = 32;
            long comment1Id = 41;
            long comment2Id = 42;

            // tworzymy pokój zagadek
            _commandHandler.Execute(new CreateRoomCommand()
            {
                RoomId = roomId,
                Name = "Czary mary",
                Level = RoomLevelCommand.Easy,
                Status = RoomStatusCommand.Free,
                UnitPrice = 100m
            });

            // pobieramy info o pokoju zagadek
            Console.WriteLine("Utworzono pokój zagadek");
            var rooms = _queryHandler.Execute(new GetAllRoomsQuery());
            foreach(var room in rooms)
            {
                Console.WriteLine($"Id pokoju: {room.Id}, Nazwa: {room.Name}, Rating: {room.AverageRating}");
            }

            // tworzymy 2 graczy
            _commandHandler.Execute(new CreatePlayerCommand()
            {
                PlayerId = player1Id,
                Name = "Jan Kowalski",
                Email = "jan.kowalski@gmail.com",
                Status = PlayerStatusCommand.Active
            });

            _commandHandler.Execute(new CreatePlayerCommand()
            {
                PlayerId = player2Id,
                Name = "Jan Nowak",
                Email = "janek@poczta.onet.pl",
                Status = PlayerStatusCommand.Active
            });

            // pobieramy info o graczach
            Console.WriteLine("Utworzono dwóch graczy");
            var players = _queryHandler.Execute(new GetAllPlayersQuery());
            foreach (var player in players)
            {
                Console.WriteLine($"Id gracza: {player.Id}, Nazwa: {player.Name}");
            }

            // gracz 1 rozpoczyna wizytę w pokoju
            _commandHandler.Execute(new StartVisitCommand()
            { 
                VisitId = visit1Id,
                Started = new DateTime().AddHours(1).AddMinutes(2),
                RoomId = roomId, 
                PlayerId = player1Id
            });

            // pobieramy info o wizytach
            Console.WriteLine("Gracz 1 rozpoczyna wizytę");
            var visits = _queryHandler.Execute(new GetAllVisitsQuery());
            foreach(var visit in visits )
            {
                Console.WriteLine($"Id wizyty: {visit.Id}, Id gracza: {visit.PlayerId}, Rozpoczęcie: {visit.Started}, Zakończenie: {visit.Finished}, Do zapłaty: {visit.Total_Amount}");
            }

            // gracz 1 kończy wizytę w pokoju
            _commandHandler.Execute(new StopVisitCommand()
            {
                VisitId = visit1Id,
                Finished = new DateTime().AddHours(1).AddMinutes(30)
            });

            // pobieramy info o wizytach
            Console.WriteLine("Gracz 1 kończy wizytę");
            visits = _queryHandler.Execute(new GetAllVisitsQuery());
            foreach (var visit in visits)
            {
                Console.WriteLine($"Id wizyty: {visit.Id}, Id gracza: {visit.PlayerId}, Rozpoczęcie: {visit.Started}, Zakończenie: {visit.Finished}, Do zapłaty: {visit.Total_Amount}");
            }

            // gracz 2 rozpoczyna wizytę w pokoju
            _commandHandler.Execute(new StartVisitCommand()
            {
                VisitId = visit2Id,
                Started = new DateTime().AddHours(3).AddMinutes(12),
                RoomId = roomId,
                PlayerId = player2Id
            });

            // pobieramy info o wizytach
            Console.WriteLine("Gracz 2 rozpoczyna wizytę");
            visits = _queryHandler.Execute(new GetAllVisitsQuery());
            foreach (var visit in visits)
            {
                Console.WriteLine($"Id wizyty: {visit.Id}, Id gracza: {visit.PlayerId}, Rozpoczęcie: {visit.Started}, Zakończenie: {visit.Finished}, Do zapłaty: {visit.Total_Amount}");
            }

            // gracz 2 kończy wizytę w pokoju
            _commandHandler.Execute(new StopVisitCommand()
            {
                VisitId = visit2Id,
                Finished = new DateTime().AddHours(4).AddMinutes(15)
            });

            // pobieramy info o wizytach
            Console.WriteLine("Gracz 2 kończy wizytę");
            visits = _queryHandler.Execute(new GetAllVisitsQuery());
            foreach (var visit in visits)
            {
                Console.WriteLine($"Id wizyty: {visit.Id}, Id gracza: {visit.PlayerId}, Rozpoczęcie: {visit.Started}, Zakończenie: {visit.Finished}, Do zapłaty: {visit.Total_Amount}");
            }

            // gracz 1 i 2 komentują wizytę w pokoju
            _commandHandler.Execute(new AddCommentCommand()
            { 
                CommentId = comment1Id,
                Created = new DateTime().AddHours(1).AddMinutes(35),
                Title = "aqq",
                Text = "ala ma kota",
                Rating = 3,
                PlayerId = player1Id,
                RoomId = roomId
            });

            _commandHandler.Execute(new AddCommentCommand()
            {
                CommentId = comment2Id,
                Created = new DateTime().AddHours(4).AddMinutes(40),
                Title = "komentarz",
                Text = "ola ma psa",
                Rating = 5,
                PlayerId = player2Id,
                RoomId = roomId
            });

            // pobieramy info o komentarzach
            Console.WriteLine("Gracze dodali komentarze do pokoju");
            var comments = _queryHandler.Execute(new GetAllCommentsForRoomQuery()
            {
                RoomId = roomId
            });

            foreach(var c in comments)
            {
                Console.WriteLine($"Id komentarza: {c.Id}, Id gracza: {c.PlayerId}, Tytuł: {c.Title}, Treść: {c.Text} Rating: {c.Rating}");
            }

            // pobieramy info o pokoju i ratingu
            Console.WriteLine("Informacja o ratingach");
            rooms = _queryHandler.Execute(new GetAllRoomsQuery());
            foreach (var room in rooms)
            {
                Console.WriteLine($"Id pokoju: {room.Id}, Nazwa: {room.Name}, Średni rating {room.AverageRating}");
            }
        }
    }
}
