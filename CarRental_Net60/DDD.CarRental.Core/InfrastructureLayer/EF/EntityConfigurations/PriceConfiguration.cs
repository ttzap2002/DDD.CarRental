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
    public class PriceConfiguration : IEntityTypeConfiguration<PriceTable>
    {
        public void Configure(EntityTypeBuilder<PriceTable> priceConfiguration)
        {
            // ustawianie klucza głównego
            priceConfiguration.HasKey(c => c.Id);

            // klucz tabeli nie będzie generowany przez EF
            priceConfiguration.Property(v => v.Id).ValueGeneratedNever();

            priceConfiguration.OwnsOne(r => r.unitPrice);


        }


    }
}
