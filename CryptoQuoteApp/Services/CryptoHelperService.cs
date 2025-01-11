using CryptoQuoteApp.Models;
using CryptoQuoteApp.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CryptoQuoteApp.Services;

/// <summary>
/// You may add caching (e.g., using IMemoryCache) for frequent API calls to reduce latency and API costs.
/// </summary>
public class CryptoHelperService : ICryptoHelperService
{

    private readonly CoinMarketCapService _coinMarketCapService;
    private readonly ExchangeRatesService _exchangeRatesService;
    private readonly ILogger<CryptoHelperService> _logger;

    public CryptoHelperService(ILogger<CryptoHelperService> logger, CoinMarketCapService coinMarketCapService, ExchangeRatesService exchangeRatesService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _coinMarketCapService = coinMarketCapService;
        _exchangeRatesService = exchangeRatesService;
    }

    public async Task<CryptoQuoteResponse> RetrieveCryptoQuote(string cryptoCode, string baseCurrency = "USD")
    {
        try
        {
            // By default, the cryptocurrency quote base currency is USD
            // The error response when retrieving the crypto quotes is ""error_message": "\"Your plan is limited to 1 convert options"" when passing the following params ; ?symbol=BTC&convert=BTC,USD
            // The free plan does not allow conversion types - so we have to live with USD being the base currency for now (Will make the GetCryptoPriceInUsd method dynamic at a later stage)
            // For now it's implicit.

            _logger.LogInformation("Fetching price for cryptocurrency code: {CryptoCode}", cryptoCode);
            var usdPrice = await _coinMarketCapService.GetCryptoPriceInUsd(cryptoCode, baseCurrency);

            // The exchange rate requires a conversion as well due to fact that the free license does not allow for the base currency code to be changed.
            // The default is EUR (License can be obtained at a later stage to make the magic happen)
            // For now we will perform a magic trick and calculate the exchange rates for USD
            var exchangeRates = await _exchangeRatesService.GetExchangeRates(baseCurrency);

            // Check if exchange rates contain the necessary keys
            if (!exchangeRates.ContainsKey("EUR") || !exchangeRates.ContainsKey("USD") ||
                !exchangeRates.ContainsKey("BRL") || !exchangeRates.ContainsKey("GBP") ||
                !exchangeRates.ContainsKey("AUD"))
            {
                throw new Exception("Missing required exchange rate data from ExchangeRates API.");
            }

            //Convert the current default is EUR base to USD equivalent using the following formula (The formula should work for any base conversion using the free license)
            /*
             EUR = exchangeRates["EUR"] / exchangeRates["USD"]
             BRL = exchangeRates["BRL"] / exchangeRates["USD"]
             GBP = exchangeRates["GBP"] / exchangeRates["USD"]
             AUD = exchangeRates["AUD"] / exchangeRates["USD"]
             */
            var eurRate = (exchangeRates["EUR"] / exchangeRates["USD"]);
            var brlRate = (exchangeRates["BRL"] / exchangeRates["USD"]);
            var gbpRate = (exchangeRates["GBP"] / exchangeRates["USD"]);
            var audRate = (exchangeRates["AUD"] / exchangeRates["USD"]);

            var quotes = new Dictionary<string, decimal>
            {
                { "USD", usdPrice },
                { "EUR", usdPrice * eurRate },
                { "BRL", usdPrice * brlRate },
                { "GBP", usdPrice * gbpRate },
                { "AUD", usdPrice * audRate }
            };

            return new CryptoQuoteResponse
            {
                CryptoQuote = new CryptoQuote()
                {
                    Cryptocurrency = cryptoCode,
                    Quotes = quotes
                },
               Success = true,
               Message = string.Empty
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving cryptocurrency quote for {CryptoCode}", cryptoCode);
            return new CryptoQuoteResponse
            {
                
                Success = false,
                Message = ex.Message
            };
        }
    }
}