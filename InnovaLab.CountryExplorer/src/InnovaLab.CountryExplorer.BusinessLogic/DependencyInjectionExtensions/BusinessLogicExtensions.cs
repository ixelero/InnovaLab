using System.Reflection;
using InnovaLab.CountryExplorer.BusinessLogic.Clients;
using InnovaLab.CountryExplorer.BusinessLogic.Mapster;
using InnovaLab.CountryExplorer.BusinessLogic.Services;
using InnovaLab.CountryExplorer.BusinessLogic.Services.Contracts;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Refit;

namespace InnovaLab.CountryExplorer.BusinessLogic.DependencyInjectionExtensions;

public static class BusinessLogicExtensions
{
    public static IServiceCollection RegisterBusinessLogicDependencies(
        this IServiceCollection services,
        string restCountriesBaseUrl,
        string retryCount)
    {
        services
            .RegisterServices()
            .RegisterValidators()
            .RegisterHttpClients(restCountriesBaseUrl, retryCount);

        RegisterMapster();

        return services;
    }

    private static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services
            .AddScoped<ICountryService, CountryService>();

        return services;
    }

    private static IServiceCollection RegisterValidators(this IServiceCollection services)
    {
        // services.AddValidatorsFromAssemblyContaining<{AnyValidatorName}>();

        return services;
    }

    private static void RegisterMapster()
    {
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetAssembly(typeof(MapsterConfigRegister)) ??
                                              throw new InvalidOperationException());
    }

    private static IServiceCollection RegisterHttpClients(
        this IServiceCollection services,
        string restCountriesBaseUrl,
        string retryCount)
    {
        services.AddRefitClient<IRestCountriesApi>()
            .ConfigureHttpClient(
                httpClient => httpClient.BaseAddress =
                    new Uri(restCountriesBaseUrl))
            .AddPolicyHandler(
                HttpPolicyExtensions.HandleTransientHttpError()
                    .RetryAsync(int.Parse(retryCount)));

        return services;
    }
}
