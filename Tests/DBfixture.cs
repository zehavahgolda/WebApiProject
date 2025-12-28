

﻿using Castle.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class DatabaseFixture : IDisposable
    {
        public MyShopContext Context { get; private set; }

        public DatabaseFixture()
        {

            // Set up the test database connection and initialize the context
            var options = new DbContextOptionsBuilder<MyShopContext>()

                .UseSqlServer("Data Source=srv2\\pupils;Initial Catalog=Store_329391924;Integrated Security=True;Trust Server Certificate=True; Pooling=False")
                .Options;
            Context = new MyShopContext(options);
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            // Clean up the test database after all tests are completed
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}