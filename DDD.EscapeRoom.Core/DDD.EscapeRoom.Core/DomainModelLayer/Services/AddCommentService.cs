using DDD.EscapeRoom.Core.DomainModelLayer.Factories;
using DDD.EscapeRoom.Core.DomainModelLayer.Interfaces;
using DDD.EscapeRoom.Core.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.EscapeRoom.Core.DomainModelLayer.Services
{
    public class AddCommentService : IAddCommentService
    {
        private ICommentRepository _commentRepository;
        
        public AddCommentService(ICommentRepository commentRepository)
        {
            this._commentRepository = commentRepository;
        }

        // Jeśli operacja biznesowa dotyczy kilku agragatów, 
        // wówczas zaleca się stworzyć specjalizowaną klasę typu serwis domenowy 
        // i jej powierzyć realizację opercji biznesowej
        public void AddComment(long id, string title, string text, int rating, DateTime created, Room room, Player player)
        {
            // validation - banned players can not add comments
            if (player.Status == PlayerStatus.Banned) 
                throw new InvalidOperationException("Banned player can not add comments");

            // create new comment
            Comment comment = new Comment(id, title, text, rating, created, room.Id, player.Id);
            this._commentRepository.Insert(comment);

            // read current rating
            double sum = this._commentRepository.GetSumOfRating(room.Id);
            long count = this._commentRepository.GetNumOfRating(room.Id);

            // update average rating in room
            double avg = (sum + comment.Rating) / (count + 1);
            room.UpdateRating(avg);
        }
    }
}
