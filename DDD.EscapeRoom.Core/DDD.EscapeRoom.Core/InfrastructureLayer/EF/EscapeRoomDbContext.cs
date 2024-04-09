using DDD.EscapeRoom.Core.DomainModelLayer.Models;
using DDD.EscapeRoom.Core.InfrastructureLayer.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

namespace DDD.EscapeRoom.Core.InfrastructureLayer.EF
{

    public class EscapeRoomDbContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public EscapeRoomDbContext(DbContextOptions<EscapeRoomDbContext> options) 
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CommentConfiguration());
            builder.ApplyConfiguration(new PlayerConfiguration());
            builder.ApplyConfiguration(new RoomConfiguration());
            builder.ApplyConfiguration(new VisitConfiguration());
            builder.ApplyConfiguration(new ScoreConfiguration());
        }
    }
}
