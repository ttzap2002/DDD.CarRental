using DDD.CarRental.Core.DomainModelLayer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.CarRental.Core.InfrastructureLayer.EF.EntityConfigurations
{
    public class PostitionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> positionConfiguration)
        {
            positionConfiguration.Property<long>("Id").IsRequired();

            // ustawianie klucza głównego
            positionConfiguration.HasKey("Id");

        }
    }

}
