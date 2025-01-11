using CryptoQuoteApp.Models;

namespace CryptoQuoteApp.Services.Interfaces;

public interface ICryptoHelperService
{
    Task<CryptoQuoteResponse> RetrieveCryptoQuote(string cryptoCode, string baseCurrency = "USD");
}