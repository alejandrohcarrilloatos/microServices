using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using CommandService.Models;
using System;
using System.Linq;
using CommandService.Data;
using Microsoft.EntityFrameworkCore;

namespace CommandService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDBContext>(), isProd);
            }

        }

        private static void SeedData(AppDBContext context, bool isProd)
        {
            //Si estamos en produccion con EF aplicamos la migraciones
            //if (isProd) {
                Console.WriteLine("---> Aplicando Migraciones EF...");
                try {
                    context.Database.Migrate();
                }
                catch (Exception ex) {
                    Console.WriteLine($"Could Not run migrations {ex.Message}");
                }
            //}
            if (!context.Platforms.Any())
            {
                Console.WriteLine("-----> Sembrando Platfoms de prueba ...");
                context.Platforms.AddRange(
                    new Platform() { Id = 1, Name = "Platform dummy 1", ExternalId =  1 },
                    new Platform() { Id = 2, Name = "Platform dummy 2", ExternalId = 2 },
                    new Platform() { Id = 3, Name = "Platform dummy 3", ExternalId = 3 }
                    //new Platform() { Id = 3, Name = "Platform dummy 3", ExternalId = 3 },
                    //new Platform() { Id = 3, Name = "Platform dummy 3", ExternalId = 3 },
                    //new Platform() { Id = 3, Name = "Platform dummy 3", ExternalId = 3 }

                    );
                context.SaveChanges();
            }

            if (!context.Commands.Any())
            {
                Console.WriteLine("-----> Sembrando datos de prueba ...");
                context.Commands.AddRange(
                    new Command() { HowTo = "How to Dummy 1", Commandline = "dotnet dummy 1", PlatformId = 1 },
                    new Command() { HowTo = "How to Dummy 2", Commandline = "dotnet dummy 2", PlatformId = 1 },
                    new Command() { HowTo = "How to Dummy 3", Commandline = "dotnet dummy 3", PlatformId = 1 }
                    //new Command() { HowTo = ".Net", Commandline = "Microsoft", Platform = "Free" },
                    //new Command() { HowTo = "SQL Server Express", Commandline = "Microsoft", Platform = "Free" },
                    //new Command() { HowTo = "Kuberbetes", Commandline = "Cloud Native Computing Foundation", Platform = "Free" }

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
