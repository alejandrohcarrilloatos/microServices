using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Commander.Models;
using System;
using System.Linq;
using Commander.Data;
using Microsoft.EntityFrameworkCore;

namespace CommandService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<CommanderContext>(), isProd);
            }

        }

        private static void SeedData(CommanderContext context, bool isProd)
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
            if (!context.Commands.Any())
            {
                Console.WriteLine("-----> Sembrando datos de prueba ...");
                context.Commands.AddRange(
                    new Command() { HowTo = "How to Dummy 1", Line = "dotnet dummy 1", Platform = "Seed" },
                    new Command() { HowTo = "How to Dummy 2", Line = "dotnet dummy 2", Platform = "Seed" },
                    new Command() { HowTo = "How to Dummy 3", Line = "dotnet dummy 3", Platform = "Seed" }
                    //new Command() { HowTo = ".Net", Line = "Microsoft", Platform = "Free" },
                    //new Command() { HowTo = "SQL Server Express", Line = "Microsoft", Platform = "Free" },
                    //new Command() { HowTo = "Kuberbetes", Line = "Cloud Native Computing Foundation", Platform = "Free" }

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
