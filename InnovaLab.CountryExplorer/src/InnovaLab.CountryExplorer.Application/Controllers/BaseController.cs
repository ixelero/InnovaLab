using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ILogger = Serilog.ILogger;

namespace InnovaLab.CountryExplorer.Application.Controllers;

[AllowAnonymous]
[ApiController]
[Route("[controller]")]
public class BaseController : ControllerBase
{
    protected ILogger Logger { get; }

    public BaseController(ILogger logger)
    {
        Logger = logger;
    }

    [NonAction]
    protected static CreatedResult Created([ActionResultObjectValue] object value)
    {
        return new CreatedResult(string.Empty, value);
    }
}
