﻿using DDD.CarRental.Core.DomainModelLayer.Models;
using DDD.CarRental.Core.InfrastructureLayer.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

namespace DDD.CarRental.Core.InfrastructureLayer.EF
{

    public class CarRentalDbContext : DbContext
    {
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Rental> Rentals { get; set; }
       

        public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options) 
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new DriverConfiguration());
            
            // ToDo: konfiguracja pozostałych klas modelu
        }
    }
}
