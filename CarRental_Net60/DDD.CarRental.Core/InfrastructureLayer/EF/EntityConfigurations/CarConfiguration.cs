using DDD.CarRental.Core.DomainModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.CarRental.Core.InfrastructureLayer.EF.EntityConfigurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> driverConfiguration)
        {
            // ustawianie klucza głównego
            driverConfiguration.HasKey(c => c.Id);

            // klucz tabeli nie będzie generowany przez EF
            driverConfiguration.Property(v => v.Id).ValueGeneratedNever();

            driverConfiguration.HasIndex(p => p.RegistrationNumber).IsUnique();


            // wykluczenie DomainsEvents z modelu relacyjnego - nie ma potrzeby zapisywania w bazie zdarzeń domenowych
            driverConfiguration.Ignore(c => c.DomainEvents);
        }

    }
}
