using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Canducci.Console.Test
{
    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PeopleList: List<People>
    {
        public PeopleList()
        {
            Add(new People { Id = 1, Name = "Test 1" });
            Add(new People { Id = 2, Name = "Test 2" });
            Add(new People { Id = 3, Name = "Test 3" });
            Add(new People { Id = 4, Name = "Test 4" });
            Add(new People { Id = 5, Name = "Test 5" });
            Add(new People { Id = 6, Name = "Test 6" });
            Add(new People { Id = 7, Name = "Test 7" });
            Add(new People { Id = 8, Name = "Test 8" });
            Add(new People { Id = 9, Name = "Test 9" });
            Add(new People { Id = 10, Name = "Test 10" });
        }
    }

    public class DatabaseContext: DbContext
    {
        public DatabaseContext()
        {
            Database.EnsureCreated();        
        }

        public DbSet<People> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\netcoreapp2.0\\","");
            optionsBuilder.UseSqlite($"Data Source={dir}\\Contatos.db");
        }
    }
}
