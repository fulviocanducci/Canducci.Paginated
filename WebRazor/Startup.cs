using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebRazor.Models;

namespace WebRazor
{
   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      public void ConfigureServices(IServiceCollection services)
      {
         services.AddEntityFrameworkSqlite();
         services.AddDbContext<DatabaseContext>(opt => { opt.UseSqlite("Data Source=Contatos.db"); });
         services.AddRazorPages();
      }

      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }
         else
         {
            app.UseExceptionHandler("/Error");
         }

         app.UseStaticFiles();
         app.UseRouting();
         app.UseAuthorization();
         app.UseEndpoints(endpoints =>
         {
            endpoints.MapRazorPages();
         });
      }
   }
}
