﻿using DDD.EscapeRoom.Core.DomainModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.EscapeRoom.Core.InfrastructureLayer.EF.EntityConfigurations
{
    public class ScoreConfiguration : IEntityTypeConfiguration<Score>
    {
        public void Configure(EntityTypeBuilder<Score> scoreConfiguration)
        {
            // Obiekty typu Value Object są rozróżnialne na podstawie wartości swoich swoich atrybutów.
            // Nie ma zatem potrzeby aby posiadały unikalny identyfikator (Id).
            // Jeśli chcemy takie obiekty mapować do oddzielnych tabel, taki identyfikator jest jednak potrzebny (będzie pełnił rolę klucza).
            // Dodawanie identyfikatora do klasy Value Object nie jest eleganckim rozwiązaniem.
            // Na szczęście Entity Framework dzięki Fluent API pozwala na dodanie do tabeli ukrytego pola Id (tzw. shadow property).
            // Takie pole będzie istniało w tabeli w bazie danych, ale nie bedzie miało odpowiednika w klasach modelu. 

            // dodawanie pola Id
            scoreConfiguration.Property<long>("Id").IsRequired();

            // ustawianie klucza głównego
            scoreConfiguration.HasKey("Id");
        }
    }
}
