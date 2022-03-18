using AutoMapper;
using CommandService.Data;
using Newtonsoft.Json.Serialization;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
//using PlatformService.SyncDataServices.Http;

namespace CommandService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment _env { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //*** Para crear la migracion hay que forzar el uso del EF asi que comentamos la condicion de IsProduction
            string strCnn = Configuration.GetConnectionString("CommanderConnection");

            if (_env.IsProduction())
            {
                Console.WriteLine($"-->Usando base de datos SQL strCnn= {strCnn}");
                services.AddDbContext<AppDBContext>(opt => opt.UseSqlServer(strCnn));
            }
            else
            {
                Console.WriteLine($"-->Usando base de datos en de desarrollo strCnn= {strCnn}");
                // Configuramos el contexto
                services.AddDbContext<AppDBContext>(opt => opt.UseInMemoryDatabase("InMem"));

            }


            services.AddControllers().
                AddNewtonsoftJson( s => {
                    s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    });

            // Configuramos el automapeo de los DTOS
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Registramos la injeccion de dependencias, existen 3 tipos Singleton, Scoped y Transient
            services.AddScoped<ICommandRepo, CommandRepo>();

            services.AddHttpsRedirection( options => options.HttpsPort = 6001 );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Commander", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Commander v1"));
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            PrepDb.PrepPopulation(app, _env.IsProduction());
        }
    }
}
