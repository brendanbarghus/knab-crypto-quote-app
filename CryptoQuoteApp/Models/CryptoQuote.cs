namespace CryptoQuoteApp.Models;

public class CryptoQuote
{
    public string Cryptocurrency { get; set; }
    public Dictionary<string, decimal> Quotes { get; set; }
}