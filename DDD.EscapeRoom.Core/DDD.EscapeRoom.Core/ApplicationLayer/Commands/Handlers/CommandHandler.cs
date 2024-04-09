
using DDD.EscapeRoom.Core.DomainModelLayer.Factories;
using DDD.EscapeRoom.Core.DomainModelLayer.Interfaces;
using DDD.EscapeRoom.Core.DomainModelLayer.Models;
using DDD.EscapeRoom.Core.DomainModelLayer.Services;
using DDD.EscapeRoom.Core.InfrastructureLayer.EF;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDD.EscapeRoom.Core.ApplicationLayer.Commands.Handlers
{
    public class CommandHandler
    {
        private IEscapeRoomUnitOfWork _unitOfWork;
        private VisitFactory _visitFactory;
        private DiscountPolicyFactory _discountPolicyFactory;
        private IAddCommentService _addCommentService;


        public CommandHandler(IEscapeRoomUnitOfWork unitOfWork, VisitFactory visitFactory, DiscountPolicyFactory discountPolicyFactory, IAddCommentService addCommentService)
        {
            _unitOfWork = unitOfWork;
            _visitFactory = visitFactory;
            _discountPolicyFactory = discountPolicyFactory;
            _addCommentService = addCommentService;
        }

        public void Execute(CreatePlayerCommand command)
        {
            Player player = this._unitOfWork.PlayerRepository.Get(command.PlayerId);
            if (player != null)
                throw new InvalidOperationException($"Player '{command.PlayerId}' already exists.");

            player = this._unitOfWork.PlayerRepository.GetPlayerByName(command.Name);
            if (player != null)
                throw new InvalidOperationException($"Player '{command.Name}' already exists.");

            // jeśli utworzenie obiektu (agregatu) jest proste,
            // wówczas tworzymy go bezpośrednio w serwisie aplikacyjnym
            player = new Player(command.PlayerId, command.Name, command.Email);

            this._unitOfWork.PlayerRepository.Insert(player);
            this._unitOfWork.Commit();
        }

        public void Execute(CreateRoomCommand command)
        {
            Room room = this._unitOfWork.RoomRepository.GetRoomById(command.RoomId);
            if (room != null)
                throw new InvalidOperationException($"Room '{command.RoomId}' already exists.");

            room = this._unitOfWork.RoomRepository.GetRoomByName(command.Name);
            if (room != null)
                throw new InvalidOperationException($"Room '{command.Name}' already exists.");

            // jeśli utworzenie obiektu (agregatu) jest proste,
            // wówczas tworzymy go bezpośrednio w serwisie aplikacyjnym
            room = new Room(command.RoomId, command.Name, (EscapeRoomLevel)command.Level, new Money(command.UnitPrice));

            this._unitOfWork.RoomRepository.Insert(room);
            this._unitOfWork.Commit();
        }

        public void Execute(StartVisitCommand command)
        {
            Room room = this._unitOfWork.RoomRepository.GetRoomById(command.RoomId)
                ?? throw new KeyNotFoundException($"Could not find room '{command.RoomId}'.");
            Player player = this._unitOfWork.PlayerRepository.Get(command.PlayerId)
                ?? throw new KeyNotFoundException($"Could not find player '{command.PlayerId}'.");

            // W przypadkach bardziej złożonych obiektów 
            // można skorzystać z dedykowanych fabryk
            Visit visit = this._visitFactory.Create(command.VisitId, command.Started, room, player);

            // utwórz politykę dyskontową i zarejestruj w wizycie
            IDiscountPolicy policy = this._discountPolicyFactory.Create(player);
            visit.RegisterPolicy(policy);

            // Uwaga: ta operacja dotyczy dwóch agreagatów: Visit i Room
            // Ustawianie pokoju na zajęty jest realizowane przez zdarzenia domenowe
            // Podczas tworzenia Visit wysyłane jest zdarzenie VisitCreatedDomainEvent
            // Zdarzenie to trafia do subskrybenta, którym jest m.in. klasa EnterToRoomWhenVisitStartedEventHandler
            // która pobiera Room i ustawia jego status

            this._unitOfWork.VisitRepository.Insert(visit);
            this._unitOfWork.Commit();
        }

        public void Execute(StopVisitCommand command)
        {
            Visit visit = this._unitOfWork.VisitRepository.Get(command.VisitId)
                ?? throw new KeyNotFoundException($"Could not find visit '{command.VisitId}'.");
            Room room = this._unitOfWork.RoomRepository.GetRoomById(visit.RoomId)
                ?? throw new KeyNotFoundException($"Could not find room '{visit.RoomId}'.");
            Player player = this._unitOfWork.PlayerRepository.Get(visit.PlayerId)
                ?? throw new KeyNotFoundException($"Could not find player '{visit.PlayerId}'.");
            
            // odnotuj zakończenie wizyty
            visit.StopVisit(command.Finished, room.UnitPrice);

            // ustaw pokój na wolny
            room.ExitTheRoom(player.Id, player.Name, visit.GetTimeInMinutes(), visit.Finished.Value);

            // zachowaj zamiany
            this._unitOfWork.Commit();

            // Uwaga: ta operacja dotyczy dwóch agreagatów, 
            // ale interakcja pomiędzy agragatami jest praktycznie zerowa,
            // stad nie ma potrzeby tworzenia dodatkowego serwisu domenowgo
            // Serwis aplikacyjny pełni tu rolę nadzorcy (orkiestrator) nad realizacją całego scenariusza
        }

        public void Execute(AddCommentCommand command)
        {
            // zadanie serwisu aplikacyjnego polega m.in. na pobraniu agreagatów
            Room room = this._unitOfWork.RoomRepository.GetRoomById(command.RoomId)
                ?? throw new KeyNotFoundException($"Could not find room '{command.RoomId}'.");
            Player player = this._unitOfWork.PlayerRepository.Get(command.PlayerId)
                ?? throw new KeyNotFoundException($"Could not find player '{command.PlayerId}'.");

            // jeśli logika biznesowa obejmuje kilka agregatów
            // warto "zatrudnić" serwis domenowy
            this._addCommentService.AddComment(command.CommentId, command.Title, command.Text, command.Rating, command.Created, room, player);

            // zapisz zmiany
            this._unitOfWork.Commit();
        }
    }
}
