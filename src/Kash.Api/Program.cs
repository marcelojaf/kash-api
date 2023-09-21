using Kash.Api.Configuration;
using Kash.Api.Data;
using Kash.Api.Data.Context;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services.AddDbContext<KashDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    x => x.MigrationsAssembly("Kash.Api.Data")));

builder.Services.AddIdentityConfiguration(builder.Configuration);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddApiConfig();

builder.Services.AddSwaggerConfig();

builder.Services.ResolveDependencies();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbInitialize = scope.ServiceProvider.GetRequiredService<IDBInitializer>();
    dbInitialize.Initialize();

    var identityDbInitialize = scope.ServiceProvider.GetRequiredService<IIdentityDbInitializer>();
    identityDbInitialize.Initialize();
}

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure
app.UseApiConfig(app.Environment);

app.UseSwaggerConfig(apiVersionDescriptionProvider);

app.MapControllers();

app.Run();

