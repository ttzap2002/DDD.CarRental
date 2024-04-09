using DDD.EscapeRoom.Core.DomainModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.EscapeRoom.Core.InfrastructureLayer.EF.EntityConfigurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> roomConfiguration)
        {
            // ustawianie klucza głównego
            roomConfiguration.HasKey(r => r.Id);

            // klucz tabeli nie będzie generowany przez EF
            roomConfiguration.Property(r => r.Id).ValueGeneratedNever();

            // nazwa pokoju ma być unikalna
            roomConfiguration.HasIndex(r => r.Name).IsUnique();

            // wykluczenie DomainsEvents z modelu relacyjnego
            roomConfiguration.Ignore(c => c.DomainEvents);

            // mechanizm OwnOne dodaje nowe pola do wyjściowej tabeli 
            // dla porównania zob. mapowanie dla klasy Score, która jest również typu Value object - tam jest tworzona nowa tabela
            roomConfiguration.OwnsOne(r => r.UnitPrice);
        }
    }
}
