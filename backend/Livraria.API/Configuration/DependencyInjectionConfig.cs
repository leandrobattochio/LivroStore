using Livraria.Infra.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace Livraria.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        /// <summary>
        /// Extensão para centralizar em um unico lugar as injeções de dependencia
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            return services;
        }

    }
}