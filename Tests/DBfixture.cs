using Entity;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;

namespace Tests
{
    public class DatabaseFixture : IDisposable
    {
        public Store_329391924Context Context { get; private set; }

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<Store_329391924Context>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            Context = new Store_329391924Context(options);
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}