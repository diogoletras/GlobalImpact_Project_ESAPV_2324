using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalImpact.Data;
using GlobalImpact.Enumerates;
using GlobalImpact.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace GlobalImpactTest.IClassFixture
{
    public class ApplicationDbContextFixture : IDisposable
    {
        public ApplicationDbContext DbContext { get; private set; }

        public ApplicationDbContextFixture()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlite(connection)
                    .Options;
            DbContext = new ApplicationDbContext(options);

            DbContext.Database.EnsureCreated();

            DbContext.SaveChanges();
        }

        public void Dispose() => DbContext.Dispose();
    }
}
