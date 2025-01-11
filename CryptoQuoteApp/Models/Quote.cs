using Newtonsoft.Json;

namespace CryptoQuoteApp.Models;

public class Quote
{
    [JsonProperty("USD")]
    public Currency USD { get; set; }
}