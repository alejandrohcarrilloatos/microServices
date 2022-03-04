using AutoMapper;
using Commander.Data;
using CommandService.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;

namespace Commander
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configuramos el contexto
            services.AddDbContext<CommanderContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("CommanderConnection")));

            services.AddControllers().
                AddNewtonsoftJson( s => {
                    s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    });

            // Configuramos el automapeo de los DTOS
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Registramos la injeccion de dependencias, existen 3 tipos Singleton, Scoped y Transient
            services.AddScoped<ICommanderRepo, SqlCommanderRepo>();

            services.AddHttpsRedirection( options => options.HttpsPort = 6001 );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Commander", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            PrepDb.PrepPopulation(app);
        }
    }
}
