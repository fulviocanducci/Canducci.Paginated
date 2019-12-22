using Microsoft.EntityFrameworkCore;

namespace UnitTest.Models
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
         optionsBuilder.UseSqlite($"Data Source=Contatos.db");
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         base.OnModelCreating(modelBuilder);
      }
   }
}
