using InnovaLab.CountryExplorer.BusinessLogic.DTOs;
using InnovaLab.CountryExplorer.Domain.Models;
using Mapster;

namespace InnovaLab.CountryExplorer.BusinessLogic.Mapster;

public static class MapsterConfigExtensions
{
    public static void ConfigContryMapping(this TypeAdapterConfig config)
    {
        config.NewConfig<Country, CountryDTO>()
            .Map(dest => dest.Name, src => src.Name.Common)
            .Map(dest => dest.Capital, src => src.Capitals.FirstOrDefault() ?? string.Empty)
            .Map(dest => dest.Language, src => src.Languages.FirstOrDefault().Value ?? string.Empty)
            .Map(dest => dest.Currency, src => GetCurrency(src.Currencies));
    }

    public static void ConfigFlagMapsMapping(this TypeAdapterConfig config)
    {
        config.NewConfig<FlagMaps, FlagMapsDTO>()
            .Map(dest => dest.Maps, src => src.Maps.GoogleMaps);
    }

    private static string GetCurrency(Dictionary<string, Currency> currencies)
    {
        if (currencies is not null && currencies.Count != 0)
        {
            KeyValuePair<string, Currency> currencyPair = currencies.FirstOrDefault();

            return $"{currencyPair.Key} / {currencyPair.Value.Name}";
        }

        return string.Empty;
    }
}
