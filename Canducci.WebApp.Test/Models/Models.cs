using Microsoft.EntityFrameworkCore;
using System;
namespace Canducci.WebApp.Test.Models
{
    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<People> People { get; set; }

    }
}
