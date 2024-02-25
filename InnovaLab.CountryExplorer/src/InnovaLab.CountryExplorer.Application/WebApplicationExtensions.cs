using InnovaLab.CountryExplorer.Application.Exceptions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Serilog;

namespace InnovaLab.CountryExplorer.Application;

internal static class WebApplicationExtensions
{
    internal static WebApplication ConfigureApp(this WebApplication app)
    {
        app.UseCors();

        app.UseExceptionHandling(app.Services.GetRequiredService<IServiceScopeFactory>(), app.Environment);

        app.UseSwaggerWithUI(app.Services.GetRequiredService<IApiVersionDescriptionProvider>());

        app.UseCookiePolicy();

        app.UseSerilogRequestLogging();

        app.MapControllers();
        app.MapHealthChecks("/health", new() { Predicate = _ => false });
        app.MapHealthChecks("/health/ready", new() { Predicate = check => check.Tags.Contains("services") });

        return app;
    }
}
