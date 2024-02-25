using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace InnovaLab.CountryExplorer.Application.Exceptions;

public abstract class BaseExceptionHandler<TException> : IExceptionHandler<TException>
    where TException : Exception
{
    public abstract Task HandleExceptionAsync(
        TException exception,
        HttpContext context,
        IWebHostEnvironment hostEnvironment = null,
        CancellationToken cancellationToken = default);

    public virtual Task HandleExceptionAsync(
        Exception exception,
        HttpContext context,
        IWebHostEnvironment hostEnvironment = null,
        CancellationToken cancellationToken = default) =>
        HandleExceptionAsync(exception as TException, context, hostEnvironment, cancellationToken);

    protected Task SetResponseAsync(
        HttpContext context,
        string message,
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
        string detail = default,
        CancellationToken cancellationToken = default)
    {
        string jsonResult = JsonSerializer.Serialize(
            new ProblemDetails
            {
                Title = message,
                Status = (int)statusCode,
                Detail = detail
            });

        context.Response.ContentType = ContentTypes.ApplicationProblemJson;
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(jsonResult, cancellationToken);
    }
}
