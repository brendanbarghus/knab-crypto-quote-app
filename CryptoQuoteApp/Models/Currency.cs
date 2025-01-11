using Newtonsoft.Json;

namespace CryptoQuoteApp.Models;

public class Currency
{
    [JsonProperty("price")]
    public decimal Price { get; set; }
}