﻿using DDD.EscapeRoom.Core.InfrastructureLayer.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.EscapeRoom.ConsoleTest
{
    public class TestUtils
    {
        public static EscapeRoomDbContext InitializeEscapeRoomContext()
        {
            // baza danych SQLite
            // wymagana instalacja pakietu Microsoft.EntityFrameworkCore.Sqlite
            var sqliteConnectionString = @"Data Source=EscapeRoom_DDD.db";
            var options = new DbContextOptionsBuilder<EscapeRoomDbContext>()
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug()))  // umożliwia m.in. podglądanie SQL generowanego przez EF
                .UseSqlite(sqliteConnectionString)
                .Options;

            //// baza danych MS SQL
            //// wymagana instalacja pakietu Microsoft.EntityFrameworkCore.SqlServer
            //var connectionString = @"Server=(localdb)\mssqllocaldb;Database=EscapeRoom_DDD;Trusted_Connection=True;";
            //var options = new DbContextOptionsBuilder<EscapeRoomDbContext>()
            //    .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug()))   // umożliwia m.in. podglądanie SQL generowanego przez EF
            //    .UseSqlServer(connectionString)
            //    .Options;

            var context = new EscapeRoomDbContext(options);

            return context;
        }
        

    }
}
