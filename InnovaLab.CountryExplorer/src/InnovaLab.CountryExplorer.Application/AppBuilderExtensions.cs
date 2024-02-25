using Serilog;
using InnovaLab.CountryExplorer.BusinessLogic.DependencyInjectionExtensions;
using InnovaLab.CountryExplorer.Application.Exceptions;

namespace InnovaLab.CountryExplorer.Application;

internal static class AppBuilderExtensions
{
    internal static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        ConfigurationManager config = builder.Configuration;

        builder.Host
            .UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));

        builder.Services
            .RegisterBusinessLogicDependencies(config["RestCountries:BaseUrl"], config["RetryCount"])
            .RegisterExceptionHandling()
            .RegisterSwagger(config["SwaggerDocTitle"])
            .RegisterApiVersioning()
            .AddHttpContextAccessor()
            .AddCors(options => {
                options.AddDefaultPolicy(builder => {
                    builder.WithOrigins(config["CorsPolicy:Local"])
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            })
            .AddControllers(options =>
                options
                    .AddRouteTransformation()
                );

        builder.ConfigureHealthChecks();

        return builder;
    }

    private static WebApplicationBuilder ConfigureHealthChecks(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddHealthChecks()
            .AddUrlGroup(new Uri(builder.Configuration["RestCountries:HealthUrl"]), name: "RestCountries", tags: ["services"]);

        return builder;
    }
}
