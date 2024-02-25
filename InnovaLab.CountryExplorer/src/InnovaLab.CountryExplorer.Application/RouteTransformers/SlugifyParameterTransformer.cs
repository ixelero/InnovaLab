using System.Text.RegularExpressions;

namespace InnovaLab.CountryExplorer.Application.RouteTransformers;

public partial class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string TransformOutbound(object value)
    {
        // Slugify value
        return value == null
            ? null
            : AlphabetRegex().Replace(value.ToString() ?? string.Empty, "$1-$2").ToLower();
    }

    [GeneratedRegex("([a-z])([A-Z])")]
    private static partial Regex AlphabetRegex();
}
