using DDD.EscapeRoom.Core.DomainModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.EscapeRoom.Core.InfrastructureLayer.EF.EntityConfigurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> commentConfiguration)
        {
            // ustawianie klucza głównego
            commentConfiguration.HasKey(c => c.Id);

            // klucz tabeli nie będzie generowany przez EF
            commentConfiguration.Property(v => v.Id).ValueGeneratedNever();

            // wykluczenie DomainsEvents z modelu relacyjnego - nie ma potrzeby zapisywania w bazie zdarzeń domenowych
            commentConfiguration.Ignore(c => c.DomainEvents);

            // ustawienie obowiązkowości klucza obcego do tabeli Player
            commentConfiguration.Property<long>("PlayerId").IsRequired();

            // ustawienie związku 1:N pomiędzy tabelami Player i Visit
            commentConfiguration.HasOne<Player>()
                .WithMany()
                .IsRequired(false)
                .HasForeignKey("PlayerId");

            // ustawienie obowiązkowości klucza obcego do tabeli Room
            commentConfiguration.Property<long>("RoomId").IsRequired();

            // ustawienie związku 1:N pomiędzy tabelami Comment i Visit
            commentConfiguration.HasOne<Room>()
                .WithMany()
                .IsRequired(false)
                .HasForeignKey("RoomId");
        }
    }
}
