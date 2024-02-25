using InnovaLab.CountryExplorer.Application.RouteTransformers;
using InnovaLab.CountryExplorer.Application.Swagger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;

namespace InnovaLab.CountryExplorer.Application;

public static class ApiExtensions
{
    public static MvcOptions AddRouteTransformation(
        this MvcOptions options)
    {
        options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));

        return options;
    }

    public static IServiceCollection RegisterApiVersioning(this IServiceCollection services,
        bool onlyUrlPathBasedVersioning = false)
    {
        services.AddApiVersioning(apiVersioningOptions => {
            apiVersioningOptions.AssumeDefaultVersionWhenUnspecified = true;
            apiVersioningOptions.DefaultApiVersion = new ApiVersion(1, 0);
            apiVersioningOptions.ReportApiVersions = true;

            if (onlyUrlPathBasedVersioning)
            {
                apiVersioningOptions.ApiVersionReader = new UrlSegmentApiVersionReader();
            }
        });

        services.AddVersionedApiExplorer(setup => {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });

        return services;
    }

    public static IServiceCollection RegisterSwagger(this IServiceCollection services,
        string swaggerDocTitle)
    {
        services.AddSwaggerGen(swaggerGenOptions => {
            IApiVersionDescriptionProvider provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

            foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
            {
                swaggerGenOptions.SwaggerDoc(description.GroupName, CreateVersionInfo(description, swaggerDocTitle));
            }

            swaggerGenOptions.OperationFilter<ResponseCodesOperationFilter>();
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerWithUI(this IApplicationBuilder app,
        IApiVersionDescriptionProvider provider)
    {
        app.UseSwagger();

        app.UseSwaggerUI(swaggerUiOptions => {
            foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
            {
                swaggerUiOptions.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            }
        });

        return app;
    }

    private static OpenApiInfo CreateVersionInfo(ApiVersionDescription description,
        string swaggerDocTitle)
    {
        OpenApiInfo info = new()
        {
            Title = swaggerDocTitle,
            Version = description.ApiVersion.ToString()
        };

        if (description.IsDeprecated)
        {
            info.Description += " This API version has been deprecated.";
        }

        return info;
    }
}
