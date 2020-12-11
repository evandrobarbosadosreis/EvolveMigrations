using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Npgsql;
using webapi.Data;

namespace webapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment   = environment;
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
                // TODO: LOG
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
            }
            catch
            {
                throw;
            }
        }

    }
}
