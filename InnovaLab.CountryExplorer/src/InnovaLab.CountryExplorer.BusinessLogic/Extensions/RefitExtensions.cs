using System.Text.Json;
using Refit;

namespace InnovaLab.CountryExplorer.BusinessLogic.Extensions;

public static class RefitExtensions
{
    public static async Task<T> GetResponseValueAsync<T>(
        this IApiResponse response,
        CancellationToken cancellationToken = default)
    {
        JsonSerializerOptions jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };
        JsonSerializerOptions options = jsonSerializerOptions;

        if (response.IsSuccessStatusCode)
        {
            if (response is ApiResponse<HttpContent> apiResponse && apiResponse.Content != null)
            {
                string contentString = await apiResponse.Content.ReadAsStringAsync(cancellationToken);
                return JsonSerializer.Deserialize<T>(contentString, options);
            }
        }

        return default;
    }
}
