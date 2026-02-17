

﻿using Castle.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using  Entity;

namespace Tests
{
    public class DatabaseFixture : IDisposable
    {
        public Store_329391924Context  Context { get; private set; }

        public DatabaseFixture()
        {

            var options = new DbContextOptionsBuilder<Store_329391924Context>()

                .UseSqlServer("Server=desktop-t8jm6mu; Database=Store_329391924Context; Integrated Security=True; TrustServerCertificate=True;")
                .Options;
            Context = new Store_329391924Context(options);
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
         
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}