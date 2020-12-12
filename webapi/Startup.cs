using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Npgsql;
using Serilog;
using webapi.Data;

namespace webapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment   = environment;
            Log.Logger    = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("PostgreSQLConnectionString");

            services.AddControllers();
            services.AddDbContext<PostgreSQLContext>(o => o.UseNpgsql(connectionString));

            if (Environment.IsDevelopment())
            {
                ExecutarMigrations(connectionString);
            }

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "webapi", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "webapi v1"));
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// Documentação: https://evolve-db.netlify.app/
        /// </summary>
        private void ExecutarMigrations(string connectionString)
        {
            try
            {
                Log.Information("Iniciando aplicação de migrations");
                var connection = new NpgsqlConnection(connectionString);
                var evolve = new Evolve.Evolve(connection)
                {
                    Locations = new List<string> 
                    { 
                        "Data/DB/Migrations", 
                        "Data/DB/Seeders" 
                    },
                    IsEraseDisabled = true
                };
                evolve.Migrate();
                Log.Information("Execução de migrations finalizada");
            }
            catch (Exception ex)
            {
                Log.Error("Não foi possível atualizar o banco de dados", ex);
                throw;
            }
        }

    }
}
