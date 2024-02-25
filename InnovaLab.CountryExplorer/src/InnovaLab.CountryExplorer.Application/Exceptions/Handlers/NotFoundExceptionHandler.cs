using System.Net;
using InnovaLab.CountryExplorer.BusinessLogic.CustomExceptions;

namespace InnovaLab.CountryExplorer.Application.Exceptions;

public class NotFoundExceptionHandler : BaseExceptionHandler<NotFoundException>
{
    public override Task HandleExceptionAsync(
        NotFoundException exception,
        HttpContext context,
        IWebHostEnvironment hostEnvironment = null,
        CancellationToken cancellationToken = default) =>
        SetResponseAsync(context, exception.Message, HttpStatusCode.NotFound, null, cancellationToken);
}
