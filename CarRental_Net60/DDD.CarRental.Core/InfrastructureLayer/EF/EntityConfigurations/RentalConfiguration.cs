﻿using DDD.CarRental.Core.DomainModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.CarRental.Core.InfrastructureLayer.EF.EntityConfigurations
{
    public class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        
        public void Configure(EntityTypeBuilder<Rental> rentalConfiguration)
        {
            // ustawianie klucza głównego
            rentalConfiguration.HasKey(c => c.Id);


            rentalConfiguration.Ignore(c => c.DomainEvents);

            rentalConfiguration.Property<long>("DriverId").IsRequired();

            rentalConfiguration.HasOne<Driver>()
               .WithMany()
               .HasForeignKey(a => a.DriverId);

            
            
            rentalConfiguration.Property<long>("CarId").IsRequired();

            rentalConfiguration.HasOne<Car>()
             .WithMany()
             .HasForeignKey(a => a.CarId);

            rentalConfiguration.OwnsOne(r => r.MoneyForRental);


        }
    }
}
