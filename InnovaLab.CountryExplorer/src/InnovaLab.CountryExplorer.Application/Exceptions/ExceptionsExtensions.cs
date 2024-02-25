using Microsoft.AspNetCore.Diagnostics;
using InnovaLab.CountryExplorer.BusinessLogic.CustomExceptions;

namespace InnovaLab.CountryExplorer.Application.Exceptions;

public static class ExceptionsExtensions
{
    private static readonly Type s_handlerOpenType = typeof(IExceptionHandler<>);

    private static readonly Type s_handlerDefaultType = s_handlerOpenType.MakeGenericType(typeof(Exception));

    public static IApplicationBuilder UseExceptionHandling(
        this IApplicationBuilder app,
        IServiceScopeFactory scopeFactory,
        IWebHostEnvironment hostEnvironment)
    {
        app.UseExceptionHandler(new ExceptionHandlerOptions
        {
            AllowStatusCode404Response = true,
            ExceptionHandler = async context => {
                IExceptionHandlerFeature exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                {
                    Exception exception = exceptionHandlerFeature?.Error;

                    using IServiceScope exceptionHandlerScope = scopeFactory.CreateScope();
                    IServiceProvider serviceProvider = exceptionHandlerScope.ServiceProvider;

                    Type[] typeArgs = [exception?.GetType()];

                    var handler = serviceProvider.GetService(s_handlerOpenType.MakeGenericType(typeArgs)) ??
                                     serviceProvider.GetRequiredService(s_handlerDefaultType);

                    await ((IExceptionHandler)handler).HandleExceptionAsync(exception, context, hostEnvironment);
                }
            }
        });

        return app;
    }

    public static IServiceCollection RegisterExceptionHandling(this IServiceCollection services)
    {
        services.AddTransient<IExceptionHandler<Exception>, DefaultExceptionHandler>();
        services.AddTransient<IExceptionHandler<NotFoundException>, NotFoundExceptionHandler>();

        return services;
    }
}
