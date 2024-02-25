using InnovaLab.CountryExplorer.BusinessLogic.DTOs;
using InnovaLab.CountryExplorer.BusinessLogic.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace InnovaLab.CountryExplorer.Application.Controllers;

public class CountriesController : BaseController
{
    private readonly ICountryService _countryService;

    public CountriesController(
        ICountryService countryService,
        ILogger logger) : base(logger)
    {
        _countryService = countryService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CountryDTO>), 200)]
    public async Task<IActionResult> GetAllCountries()
    {
        Logger.Information("Getting All Countries");

        IEnumerable<CountryDTO> countries = await _countryService.GetAllCountriesAsync();

        return Ok(countries);
    }

    [HttpGet("flag-maps/{name}")]
    [ProducesResponseType(typeof(FlagMapsDTO), 200)]
    public async Task<IActionResult> GetFlagMapsByName([FromRoute] string name)
    {
        Logger.Information("Getting FlagMaps by Name = {Name}", name);

        FlagMapsDTO flagMaps = await _countryService.GetFlagMapsInfoAync(name);

        return Ok(flagMaps);
    }
}
