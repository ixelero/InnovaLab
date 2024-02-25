using InnovaLab.CountryExplorer.BusinessLogic.Clients;
using InnovaLab.CountryExplorer.BusinessLogic.CustomExceptions;
using InnovaLab.CountryExplorer.BusinessLogic.DTOs;
using InnovaLab.CountryExplorer.BusinessLogic.Extensions;
using InnovaLab.CountryExplorer.BusinessLogic.Services.Contracts;
using InnovaLab.CountryExplorer.Domain.Models;
using Mapster;
using Refit;

namespace InnovaLab.CountryExplorer.BusinessLogic.Services;

public class CountryService : ICountryService
{
    private readonly IRestCountriesApi _restCountriesApi;

    public CountryService(IRestCountriesApi restCountriesApi)
    {
        _restCountriesApi = restCountriesApi;
    }

    public async Task<IEnumerable<CountryDTO>> GetAllCountriesAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<string> fields = ["name", "capital", "currencies", "languages", "region"];

        IEnumerable<Country> countries = await _restCountriesApi.GetAllCountriesAsync(fields, cancellationToken);

        return countries.Adapt<IEnumerable<CountryDTO>>();
    }

    public async Task<FlagMapsDTO> GetFlagMapsInfoAync(string name, CancellationToken cancellationToken = default)
    {
        IEnumerable<string> fields = ["flag", "maps"];

        IApiResponse response = await _restCountriesApi.GetFlagMapsInfoAync(name, fields, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            IEnumerable<FlagMaps> flagMaps = await response.GetResponseValueAsync<IEnumerable<FlagMaps>>(cancellationToken);

            return flagMaps.FirstOrDefault().Adapt<FlagMapsDTO>();
        }

        throw new NotFoundException(typeof(Country), $"Name = '{name}'");
    }
}
