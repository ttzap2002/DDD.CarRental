using Dapper;
using DDD.EscapeRoom.Core.ApplicationLayer.Dto;
using DDD.EscapeRoom.Core.ApplicationLayer.Mappers;
using DDD.EscapeRoom.Core.DomainModelLayer.Models;
using DDD.EscapeRoom.Core.InfrastructureLayer.EF;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDD.EscapeRoom.Core.ApplicationLayer.Queries.Handlers
{
    public class QueryHandler
    {
        private EscapeRoomDbContext _dbContext;
        private Mapper _mapper;

        public QueryHandler(EscapeRoomDbContext context, Mapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public List<PlayerDto> Execute(GetAllPlayersQuery query)
        {
            var players = _dbContext.Players
                .AsNoTracking()
                .ToList();

            // mapowanie obiektów biznesowych na transferowe warto powierzyć maperom 
            // (własnym - jak tutaj lub bibliotecznym, np. Automaper)
            List<PlayerDto> result = players.Select(r => this._mapper.Map(r)).ToList();

            return result;
        }

        public List<RoomDto> Execute(GetAllRoomsQuery query)
        {
            var rooms = _dbContext.Rooms
                .AsNoTracking()
                .Include(r => r.Scores)
                .ToList();

            // mapowanie obiektów biznesowych na transferowe warto powierzyć maperom 
            // (własnym - jak tutaj lub bibliotecznym, np. Automaper)
            List<RoomDto> result = rooms.Select(r => this._mapper.Map(r)).ToList();

            return result;
        }

        public List<VisitDto> Execute(GetAllVisitsQuery query)
        {
            string sql =
                $@"SELECT 
                    v.Id, 
                    v.Started, 
                    v.Finished, 
                    v.Total_Currency,
                    v.Total_Amount,
                    v.PlayerId, 
                    p.Name AS {nameof(VisitDto.PlayerName)}, 
                    v.RoomId, 
                    r.Name AS {nameof(VisitDto.RoomName)} 
                FROM Visits v 
                INNER JOIN Players p 
                    ON v.PlayerId = p.Id 
                INNER JOIN Rooms r 
                    ON v.RoomId = r.Id";

            var connectionString = _dbContext.Database.GetDbConnection().ConnectionString;
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                var visitViews = connection.Query<VisitDto>(sql)
                    .ToList();

                // update TimeInMinutes
                foreach (var v in visitViews)
                {
                    if (v.Finished.HasValue)
                        v.TimeInMinutes = (v.Finished.Value - v.Started).Minutes;
                }

                return visitViews;
            }
        }

        public List<CommentDto> Execute(GetAllCommentsForRoomQuery query)
        {
            var comments = _dbContext.Comments
                .AsNoTracking()
                .Where(c => c.RoomId == query.RoomId)
                .ToList();

            // mapowanie obiektów biznesowych na transferowe warto powierzyć maperom 
            // (własnym - jak tutaj lub bibliotecznym, np. Automaper)
            List<CommentDto> result = comments.Select(r => this._mapper.Map(r)).ToList();

            return result;
        }

        public PlayerDto Execute(GetPlayerQuery query)
        {
            Player player = _dbContext.Players
                .AsNoTracking()
                .Where(r => r.Id == query.PlayerId)
                .FirstOrDefault();

            if (player == null)
                throw new Exception($"Could not find room '{query.PlayerId}'");

            // mapowanie obiektów biznesowych na transferowe warto powierzyć maperom 
            // (własnym - jak tutaj lub bibliotecznym, np. Automaper)
            PlayerDto result = this._mapper.Map(player);

            return result;

        }

        public RoomDto Execute(GetRoomQuery query)
        {
            Room room = _dbContext.Rooms
                .AsNoTracking()
                .Include(r => r.Scores)
                .Where(r => r.Id == query.RoomId)
                .FirstOrDefault();

            if (room == null)
                throw new Exception($"Could not find room '{query.RoomId}'");

            // mapowanie obiektów biznesowych na transferowe warto powierzyć maperom 
            // (własnym - jak tutaj lub bibliotecznym, np. Automaper)
            RoomDto result = this._mapper.Map(room);

            return result;
        }

        public VisitDto Execute(GetVisitQuery query)
        {
            string sql =
                $@"SELECT 
                    v.Id, 
                    v.Started, 
                    v.Finished, 
                    v.Total_Currency,
                    v.Total_Amount,
                    v.PlayerId, 
                    p.Name AS {nameof(VisitDto.PlayerName)}, 
                    v.RoomId, 
                    r.Name AS {nameof(VisitDto.RoomName)} 
                FROM Visits v 
                INNER JOIN Players p 
                    ON v.PlayerId = p.Id 
                INNER JOIN Rooms r 
                    ON v.RoomId = r.Id
                WHERE v.Id = @visitId";

            var connectionString = _dbContext.Database.GetDbConnection().ConnectionString;
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                var visitDto = connection.QueryFirstOrDefault<VisitDto>(sql, new { visitId = query.VisitId});

                // update TimeInMinutes
                if (visitDto.Finished.HasValue) visitDto.TimeInMinutes = (visitDto.Finished.Value - visitDto.Started).Minutes;
                
                return visitDto;
            }
        }

        public CommentDto Execute(GetCommentQuery query)
        {
            Comment comment = _dbContext.Comments
                .AsNoTracking()
                .Where(r => r.Id == query.CommentId)
                .FirstOrDefault();

            if (comment == null)
                throw new Exception($"Could not find comment '{query.CommentId}'");

            // mapowanie obiektów biznesowych na transferowe warto powierzyć maperom 
            // (własnym - jak tutaj lub bibliotecznym, np. Automaper)
            CommentDto result = this._mapper.Map(comment);

            return result;
        }
    }
}
