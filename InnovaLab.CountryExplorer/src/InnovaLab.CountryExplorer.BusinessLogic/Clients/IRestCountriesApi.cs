using InnovaLab.CountryExplorer.Domain.Models;
using Refit;

namespace InnovaLab.CountryExplorer.BusinessLogic.Clients;

public interface IRestCountriesApi
{
    [Get("/all")]
    Task<IEnumerable<Country>> GetAllCountriesAsync(
        [Query(CollectionFormat.Csv)] IEnumerable<string> fields,
        CancellationToken cancellationToken = default);

    [Get("/name/{name}")]
    Task<IApiResponse> GetFlagMapsInfoAync(
        string name,
        [Query(CollectionFormat.Csv)] IEnumerable<string> fields,
        CancellationToken cancellationToken = default);
}
