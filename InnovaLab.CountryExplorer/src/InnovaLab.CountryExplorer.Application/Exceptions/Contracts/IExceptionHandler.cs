namespace InnovaLab.CountryExplorer.Application.Exceptions;

public interface IExceptionHandler
{
    Task HandleExceptionAsync(
        Exception exception,
        HttpContext context,
        IWebHostEnvironment hostEnvironment = null,
        CancellationToken cancellationToken = default);
}

public interface IExceptionHandler<in TException> : IExceptionHandler
    where TException : Exception
{
    Task HandleExceptionAsync(
        TException exception,
        HttpContext context,
        IWebHostEnvironment hostEnvironment = null,
        CancellationToken cancellationToken = default);
}
