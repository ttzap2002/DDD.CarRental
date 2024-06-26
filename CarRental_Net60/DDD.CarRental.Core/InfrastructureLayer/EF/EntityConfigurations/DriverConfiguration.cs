﻿using DDD.CarRental.Core.DomainModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.CarRental.Core.InfrastructureLayer.EF.EntityConfigurations
{
    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> driverConfiguration)
        {
            // ustawianie klucza głównego
            driverConfiguration.HasKey(c => c.Id);

            // klucz tabeli nie będzie generowany przez EF
            driverConfiguration.Property(v => v.Id).ValueGeneratedNever();

           
        }
    }

    
}
