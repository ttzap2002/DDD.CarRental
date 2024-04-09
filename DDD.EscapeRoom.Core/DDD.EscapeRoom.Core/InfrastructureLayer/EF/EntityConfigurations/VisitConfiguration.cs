using DDD.EscapeRoom.Core.DomainModelLayer.Models;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.EscapeRoom.Core.InfrastructureLayer.EF.EntityConfigurations
{

    public class VisitConfiguration : IEntityTypeConfiguration<Visit>
    {
        public void Configure(EntityTypeBuilder<Visit> visitConfiguration)
        {
            // ustawianie klucza głównego
            visitConfiguration.HasKey(v => v.Id);

            // klucz tabeli nie będzie generowany przez EF
            visitConfiguration.Property(v => v.Id).ValueGeneratedNever();
            
            // wykluczenie DomainsEvents z modelu relacyjnego - nie ma potrzeby zapisywania w bazie zdarzeń domenowych
            visitConfiguration.Ignore(v => v.DomainEvents);

            // ustawienie obowiązkowości klucza obcego do tabeli Player
            visitConfiguration.Property<long>("PlayerId").IsRequired();

            // ustawienie związku 1:N pomiędzy tabelami Player i Visit
            visitConfiguration.HasOne<Player>()
                .WithMany()
                .IsRequired(false)
                .HasForeignKey("PlayerId");

            // ustawienie obowiązkowości klucza obcego do tabeli Room
            visitConfiguration.Property<long>("RoomId").IsRequired();

            // ustawienie związku 1:N pomiędzy tabelami Room i Visit
            visitConfiguration.HasOne<Room>()
                .WithMany()
                .IsRequired(false)
                .HasForeignKey("RoomId");

            // mechanizm OwnOne dodaje nowe pola do wyjściowej tabeli 
            // dla porównania zob. mapowanie dla klasy Score, która jest również typu Value object - tam jest tworzona nowa tabela
            visitConfiguration.OwnsOne(v => v.Total);
        }
    }
}
