using System.Text.Json.Serialization;

namespace InnovaLab.CountryExplorer.Domain.Models;

public class Country
{
    public Name Name { get; set; }

    public Dictionary<string, Currency> Currencies { get; set; }

    [JsonPropertyName("capital")]
    public IEnumerable<string> Capitals { get; set; }

    public string Region { get; set; }

    public Dictionary<string, string> Languages { get; set; }
}
