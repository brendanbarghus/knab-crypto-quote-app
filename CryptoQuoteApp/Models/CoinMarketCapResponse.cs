using Newtonsoft.Json;

namespace CryptoQuoteApp.Models;

public class CoinMarketCapResponse
{
    [JsonProperty("data")]
    public Dictionary<string, CurrencyData> Data { get; set; }
}