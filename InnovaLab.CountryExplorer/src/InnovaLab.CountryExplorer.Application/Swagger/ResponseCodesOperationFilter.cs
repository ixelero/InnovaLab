using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace InnovaLab.CountryExplorer.Application.Swagger;

public class ResponseCodesOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.ApiDescription.HttpMethod == HttpMethods.Get)
        {
            operation.Responses.Add("204", new OpenApiResponse { Description = "No Content" });
        }

        if (context.ApiDescription.HttpMethod == HttpMethods.Post)
        {
            operation.Responses.Add("400", new OpenApiResponse { Description = "Bad Request" });
        }

        operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
        operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });
        operation.Responses.Add("404", new OpenApiResponse { Description = "Not Found" });
    }
}
