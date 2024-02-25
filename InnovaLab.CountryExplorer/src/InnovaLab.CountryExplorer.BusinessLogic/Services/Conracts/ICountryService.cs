using InnovaLab.CountryExplorer.BusinessLogic.DTOs;

namespace InnovaLab.CountryExplorer.BusinessLogic.Services.Contracts;

public interface ICountryService
{
    Task<IEnumerable<CountryDTO>> GetAllCountriesAsync(CancellationToken cancellationToken = default);

    Task<FlagMapsDTO> GetFlagMapsInfoAync(string name, CancellationToken cancellationToken = default);
}
