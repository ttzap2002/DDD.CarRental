using DDD.EscapeRoom.Core.ApplicationLayer.Dto;
using DDD.EscapeRoom.Core.DomainModelLayer.Models;
using System.Linq;

namespace DDD.EscapeRoom.Core.ApplicationLayer.Mappers
{
    public class Mapper
    {
        public CommentDto Map(Comment comment)
        {
            return new CommentDto()
            {
                Id = comment.Id,
                Created = comment.Created,
                Rating = comment.Rating,
                Text = comment.Text,
                Title = comment.Title,
                RoomId = comment.RoomId,
                PlayerId = comment.PlayerId,
            };
        }

        public PlayerDto Map(Player p)
        {
            return new PlayerDto()
            {
                Id = p.Id,
                Name = p.Name,
                Email = p.Email.Value,
                Status = (PlayerStatusDto)p.Status,
            };
        }

        public RoomDto Map(Room room)
        {
            return new RoomDto()
            {
                Id = room.Id,
                Name = room.Name,
                AverageRating = room.AverageRating,
                Level = (EscapeRoomLevelDto)room.Level,
                UnitPrice = room.UnitPrice.Amount,
                Status = (RoomStatusDto)room.Status,
                Scores = room.Scores.Select(r => Map(r)).ToList(),
            };
        }

        public ScoreDto Map(Score s)
        {
            return new ScoreDto()
            {
                Created = s.Created,
                TimeInMinutes = s.TimeInMinutes,
                Player = s.Player,
            };
        }
    }

    

}
