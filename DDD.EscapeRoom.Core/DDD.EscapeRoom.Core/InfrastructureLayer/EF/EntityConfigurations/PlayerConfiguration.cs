using DDD.EscapeRoom.Core.DomainModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.EscapeRoom.Core.InfrastructureLayer.EF.EntityConfigurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> playerConfiguration)
        {
            // ustawianie klucza głównego
            playerConfiguration.HasKey(player => player.Id);

            // klucz tabeli nie będzie generowany przez EF
            playerConfiguration.Property(p => p.Id).ValueGeneratedNever();

            // nazwa gracza ma być unikalna
            playerConfiguration.HasIndex(p => p.Name).IsUnique();

            // wykluczenie DomainsEvents z modelu relacyjnego - nie ma potrzeby zapisywania w bazie zdarzeń domenowych
            playerConfiguration.Ignore(c => c.DomainEvents);

            // mechanizm OwnOne dodaje nowe pola do wyjściowej tabeli 
            // dla porównania zob. mapowanie dla klasy Score, która jest również typu Value object - tam jest tworzona nowa tabela
            playerConfiguration.OwnsOne(r => r.Email);
        }
    }
}
