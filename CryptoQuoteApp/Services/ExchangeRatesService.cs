using CryptoQuoteApp.Models;
using Newtonsoft.Json;

namespace CryptoQuoteApp.Services;

/// <summary>
/// You may add caching (e.g., using IMemoryCache) for frequent API calls to reduce latency and API costs.
/// </summary>
public class ExchangeRatesService
{
    private readonly HttpClient _httpClient;
    private readonly string _exchangeRateAccessKey;

    public ExchangeRatesService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient("ExchangeRates");
        _exchangeRateAccessKey = configuration.GetValue<string>("ExchangeRates:ApiKey") ?? throw new InvalidOperationException();
    }

    public async Task<Dictionary<string, decimal>> GetExchangeRates(string baseCurrency = "USD")
        {
            try
            {
                // Make the request to the exchange rates API
                var response = await _httpClient.GetAsync($"/latest?access_key={_exchangeRateAccessKey}&base={baseCurrency}");
                
                // Ensure that the request was successful
                response.EnsureSuccessStatusCode();

                // Read the response content
                var jsonResponse = await response.Content.ReadAsStringAsync();

                // Deserialize the response into an ExchangeRatesResponse object
                var data = JsonConvert.DeserializeObject<ExchangeRatesResponse>(jsonResponse);

                // Return the rates dictionary from the response
                return data?.Rates ?? new Dictionary<string, decimal>();
            }
            catch (HttpRequestException ex)
            {
                // Handle network errors (e.g., unreachable API, invalid responses, etc.)
                throw new Exception($"Error making the API request to the ExchangeRates service: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                // Handle issues with parsing the JSON response
                throw new Exception("Error parsing the JSON response from ExchangeRates API.", ex);
            }
            catch (KeyNotFoundException ex)
            {
                // Handle missing keys in the response data
                throw new Exception("The expected data key was not found in the response.", ex);
            }
            catch (InvalidOperationException ex)
            {
                // Handle invalid operation (e.g., missing API key in configuration)
                throw new Exception($"Invalid operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Catch all other unexpected exceptions
                throw new Exception("An unexpected error occurred while fetching exchange rates.", ex);
            }
        }
}