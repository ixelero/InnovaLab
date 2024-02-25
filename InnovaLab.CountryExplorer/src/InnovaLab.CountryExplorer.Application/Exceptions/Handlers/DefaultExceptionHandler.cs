using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace InnovaLab.CountryExplorer.Application.Exceptions;

public class DefaultExceptionHandler : IExceptionHandler<Exception>
{
    public Task HandleExceptionAsync(
        Exception exception,
        HttpContext context,
        IWebHostEnvironment hostEnvironment = null,
        CancellationToken cancellationToken = default)
    {
        string jsonResult = JsonSerializer.Serialize(
            new ProblemDetails
            {
                Title = $"{ErrorsMessages.DefaultMessage}. {exception.Message}. {exception.StackTrace}",
                Status = (int)HttpStatusCode.InternalServerError,
            });

        context.Response.ContentType = ContentTypes.ApplicationProblemJson;

        return context.Response.WriteAsync(jsonResult, cancellationToken);
    }
}
