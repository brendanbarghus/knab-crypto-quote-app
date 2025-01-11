using System.Text.Json;

namespace CryptoQuoteApp.Services;

/// <summary>
/// You may add caching (e.g., using IMemoryCache) for frequent API calls to reduce latency and API costs.
/// </summary>
public class CoinMarketCapService
{
    private readonly HttpClient _httpClient;

    public  CoinMarketCapService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("CoinMarketCap");
    }

    public async Task<decimal> GetCryptoPriceInUsd(string cryptoCode, string baseCurrency)
    {
        try
        {
            var response = await _httpClient.GetAsync($"cryptocurrency/quotes/latest?symbol={cryptoCode}");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(jsonResponse);
            // Navigate the JSON dynamically
            var root = doc.RootElement;

            // Access `price` property
            var price = root
                .GetProperty("data")
                .GetProperty(cryptoCode)[0]
                .GetProperty("quote")
                .GetProperty(baseCurrency)
                .GetProperty("price")
                .GetDecimal();

            return price;
        }
        catch (HttpRequestException ex)
        {
            // Log HTTP request issues (network errors, timeout, etc.)
            // You could use a logger here, or simply throw a new exception with a message
            throw new Exception($"Error making the API request to CoinMarketCap: {ex.Message}", ex);
        }
        catch (JsonException ex)
        {
            // Log issues related to invalid JSON response
            throw new Exception("Error parsing the JSON response from CoinMarketCap.", ex);
        }
        catch (KeyNotFoundException ex)
        {
            // Handle case when the expected JSON property is missing
            throw new Exception($"Missing expected property in the response for cryptocurrency code '{cryptoCode}' and base currency '{baseCurrency}'.", ex);
        }
        catch (Exception ex)
        {
            // General exception to catch any unexpected issues
            throw new Exception("An unexpected error occurred while fetching cryptocurrency data.", ex);
        }
    }
}