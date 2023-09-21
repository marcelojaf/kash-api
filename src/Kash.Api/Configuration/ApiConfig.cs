using Microsoft.AspNetCore.Mvc;

namespace Kash.Api.Configuration
{
    /// <summary>
    /// Classe com configurações da API
    /// </summary>
    public static class ApiConfig
    {
        /// <summary>
        /// Adiciona configurações ao ServiceCollection
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApiConfig(this IServiceCollection services)
        {
            services.AddControllers();

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddEndpointsApiExplorer();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;

            });

            services.AddCors(options =>
            {
                options.AddPolicy("Development",
                    builder =>
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());


                options.AddPolicy("Production",
                    builder =>
                        builder
                            .WithMethods("GET")
                            .WithOrigins("http://cklabs.dev")
                            .SetIsOriginAllowedToAllowWildcardSubdomains()
                            .AllowAnyHeader());
            });

            return services;
        }

        /// <summary>
        /// Adiciona configurações ao AplicationBuilder
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseApiConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors("Development");
                
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseCors("Production");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }

    }
}
