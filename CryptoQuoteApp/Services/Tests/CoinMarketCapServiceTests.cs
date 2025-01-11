using System.Net;
using Moq;
using Moq.Protected;
using Xunit;

namespace CryptoQuoteApp.Services.Tests;

public class CoinMarketCapServiceTests
{
    private Mock<IConfiguration> _configurationMock;
    
    [Fact]
    public async Task GetCryptoPriceInUsd_ReturnsCorrectPrice()
    {
        _configurationMock = new Mock<IConfiguration>();

        // Mock configuration for APIs
        _configurationMock.Setup(c => c["CoinMarketCap:BaseUrl"]).Returns("https://pro-api.coinmarketcap.com/v2/");
        _configurationMock.Setup(c => c["CoinMarketCap:ApiKey"]).Returns("71ed8263-509d-41cf-80e7-07fadd4ce9bd");
        _configurationMock.Setup(c => c["ExchangeRates:BaseUrl"]).Returns("https://api.exchangeratesapi.io/v2/");
        _configurationMock.Setup(c => c["ExchangeRates:ApiKey"]).Returns("df6e8cb9c8ee63b9179b6df5efae4b02");

        // Arrange
        var cryptoCode = "BTC"; // Example: Bitcoin
        var baseCurrency = "USD";

        // Mock the IHttpClientFactory
        var mockHttpClientFactory = new Mock<IHttpClientFactory>();

        // Mock the HttpMessageHandler
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(@"
                    {
                        ""data"": {
                            ""BTC"": [
                                {
                                    ""quote"": {
                                        ""USD"": {
                                            ""price"": 45000.5
                                        }
                                    }
                                }
                            ]
                        }
                    }")
            });

        // Create the HttpClient using the mock HttpMessageHandler
        var mockHttpClient = new HttpClient(mockHttpMessageHandler.Object);
        mockHttpClient.BaseAddress = new Uri(_configurationMock.Object["CoinMarketCap:BaseUrl"]);
        mockHttpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", _configurationMock.Object["CoinMarketCap:ApiKey"]);
        // Set up the factory to return the mock HttpClient
        mockHttpClientFactory.Setup(f => f.CreateClient("CoinMarketCap")).Returns(mockHttpClient);

        // Create the service with the mocked HttpClientFactory
        var service = new CoinMarketCapService(mockHttpClientFactory.Object);

        // Act
        var price = await service.GetCryptoPriceInUsd(cryptoCode, baseCurrency);

        // Assert
        Assert.Equal(45000.5m, price); // Assert that the price is what we mocked
    }
}