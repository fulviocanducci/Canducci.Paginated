using Microsoft.EntityFrameworkCore;

namespace WebRazor.Models
{
   public class DatabaseContext : DbContext
   {
      public DatabaseContext(DbContextOptions<DatabaseContext> options)
         : base(options)
      {
         //Database.EnsureCreated();
      }

      public DbSet<People> People { get; set; }

      //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      //{
      //   optionsBuilder.UseSqlite($"Data Source={dir}\\Contatos.db");
      //}

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         base.OnModelCreating(modelBuilder);
      }
   }
}
