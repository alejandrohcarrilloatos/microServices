using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;
using System;
using System.Linq;

namespace PlatformService.Data
{
    public static class PrepDb 
    {
        public static void PrepPopulation(IApplicationBuilder app) {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDBContext>());

            }

        }

        private static void SeedData(AppDBContext context)
        {
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
