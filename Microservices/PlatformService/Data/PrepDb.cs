using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;
using System;
using System.Linq;

namespace PlatformService.Data
{
    public static class PrepDb 
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd) {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDBContext>(), isProd);

            }

        }

        private static void SeedData(AppDBContext context, bool isProd)
        {
            //Si estamos en produccion con EF aplicamos la migraciones
            if(isProd){
                Console.WriteLine("---> Aplicando Migraciones EF...");
                try{
                    context.Database.Migrate();
                } catch (Exception ex) {
                    Console.WriteLine($"Could Not run migrations {ex.Message}");
                }
            }
            if (!context.Platforms.Any()) {
                Console.WriteLine("-----> Plantando datos...");
                context.Platforms.AddRange(
                    new Platform() { Name = ".Net", Publisher="Microsoft", Cost="Free" },
                    new Platform() { Name = "SQL Server Express", Publisher="Microsoft", Cost="Free" },
                    new Platform() { Name = "Kuberbetes", Publisher= "Cloud Native Computing Foundation", Cost="Free" }

                    );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Listo para recibir datos");
            }
        }
    }
}
