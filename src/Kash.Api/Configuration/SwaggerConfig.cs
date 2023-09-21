using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json;

namespace Kash.Api.Configuration
{
    /// <summary>
    /// Classe de Configuração do Swagger
    /// </summary>
    public static class SwaggerConfig
    {
        /// <summary>
        /// Adiciona configurações ao ServiceCollection
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<SwaggerDefaultValues>();

                List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
                xmlFiles.ForEach(xmlFile => c.IncludeXmlComments(xmlFile));
            });

            return services;
        }

        /// <summary>
        /// Adiciona configurações ao AplicationBuilder
        /// </summary>
        /// <param name="app"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });

            return app;
        }
    }

    /// <summary>
    /// Classe que extende a opção de configuração do Swagger
    /// </summary>
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        /// <summary>
        /// Construtor padrão com provider
        /// </summary>
        /// <param name="provider"></param>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

        /// <summary>
        /// Configura as opções do Swagger
        /// </summary>
        /// <param name="options"></param>
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        /// <summary>
        /// Cria uma documentação mínima da API
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "Kash (Sistema de Controle Financeiro) - API",
                Version = description.ApiVersion.ToString(),
                Description = "API REST desenvolvida em ASP.NET 6 para gerenciar o back-end do sistema Kash.",
                Contact = new OpenApiContact() { Name = "Marcelo Amador", Email = "marcelo@cklabs.dev" },
                License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
            };

            if (description.IsDeprecated)
            {
                info.Description += " Esta versão está obsoleta!";
            }

            return info;
        }
    }

    /// <summary>
    /// Classe que implementa IOperationFilter do Swagger
    /// </summary>
    public class SwaggerDefaultValues : IOperationFilter
    {
        /// <summary>
        /// Aplica as configurações do filtro do Swagger
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;

            operation.Deprecated |= apiDescription.IsDeprecated();

            foreach (var responseType in context.ApiDescription.SupportedResponseTypes)
            {
                var responseKey = responseType.IsDefaultResponse ? "default" : responseType.StatusCode.ToString();
                var response = operation.Responses[responseKey];

                foreach (var contentType in response.Content.Keys)
                    if (responseType.ApiResponseFormats.All(x => x.MediaType != contentType))
                        response.Content.Remove(contentType);
            }

            if (operation.Parameters == null)
                return;

            foreach (var parameter in operation.Parameters)
            {
                var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

                parameter.Description ??= description.ModelMetadata.Description;

                if (parameter.Schema.Default == null && description.DefaultValue != null)
                {
                    var json = JsonSerializer.Serialize(description.DefaultValue, description.ModelMetadata.ModelType);
                    parameter.Schema.Default = OpenApiAnyFactory.CreateFromJson(json);
                }

                parameter.Required |= description.IsRequired;
            }
        }
    }
}
