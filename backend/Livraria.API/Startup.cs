using Livraria.API.Configuration;
using Livraria.Infra;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace Livraria.API
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

            services.AddMediatR(typeof(Startup));

            // CArrega a classe de configuração do jwt do appsettings.json
            var jwtSettingsSection = Configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSettingsSection);

            // Configurações DI
            services.AddDependencyInjection();

            // Configurações do JWT
            services.AddJwtConfiguration(Configuration);

            // Configurações EF e Identity Core
            services.AddLivrariaDbContext(Configuration);

            services.AddControllers();

            // CORS
            services.AddCors(options =>
            {
                options.AddPolicy("Total",
                    builder =>
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });

            // Configura o Swagger
            services.AddSwaggerConfiguration();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Livraria.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Total");

            // Aplica o JWT
            app.UseJwtConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            CreateRoles(serviceProvider).Wait();
        }

        /// <summary>
        /// Cria as Roles no Startup caso não existam
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var admExists = await roleManager.RoleExistsAsync("Administrador");
            if (!admExists)
                await roleManager.CreateAsync(new IdentityRole("Administrador"));

            var normalExists = await roleManager.RoleExistsAsync("Normal");
            if (!normalExists)
                await roleManager.CreateAsync(new IdentityRole("Normal"));
        }
    }
}
