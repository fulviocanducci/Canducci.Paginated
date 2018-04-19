using Microsoft.EntityFrameworkCore;
using System;

namespace UnitTestCanducciPaginationTest.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
            //Database.EnsureCreated();
        }

        public DbSet<People> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "");
            optionsBuilder.UseSqlite($"Data Source={dir}\\Contatos.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            base.OnModelCreating(modelBuilder);
        }
    }
}
