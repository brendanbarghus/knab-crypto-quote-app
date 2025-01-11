using Newtonsoft.Json;

namespace CryptoQuoteApp.Models;

public class CurrencyData
{
    [JsonProperty("quote")]
    public Quote Quote { get; set; }
}