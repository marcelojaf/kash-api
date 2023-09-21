using Kash.Api.Business.Interfaces;
using Kash.Api.Business.Services;
using Kash.Api.Data;
using Kash.Api.Data.Context;
using Kash.Api.Data.Repository;
using Kash.Api.Extensions;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Kash.Api.Configuration
{
    /// <summary>
    /// Classe onde serão feitas as injeções de dependência
    /// </summary>
    public static class DependencyInjectionConfig
    {
        /// <summary>
        /// Resolve as dependências necessárias
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<KashDbContext>();
            services.AddScoped<IDBInitializer, DBInitializer>();
            services.AddScoped<IIdentityDbInitializer, IdentityDBInitializer>();

            services.AddScoped<IBancoRepository, BancoRepository>();
            services.AddScoped<ITipoContaRepository, TipoContaRepository>();
            services.AddScoped<IContaRepository, ContaRepository>();

            services.AddScoped<IBancoService, BancoService>();
            services.AddScoped<ITipoContaService, TipoContaService>();
            services.AddScoped<IContaService, ContaService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUSer>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }
    }
}

