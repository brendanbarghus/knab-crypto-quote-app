using System.Net;
using Moq;
using Moq.Protected;
using Xunit;

namespace CryptoQuoteApp.Services.Tests
{
    public class ExchangeRatesServiceTests
    {
        private Mock<IConfiguration> _configurationMock;
        
        [Fact]
        public async Task GetExchangeRates_ReturnsCorrectRates()
        {
            // Arrange
            var baseCurrency = "USD";
            var exchangeRateApiKey = "fake-api-key";

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
                        ""rates"": {
                            ""EUR"": 0.85,
                            ""GBP"": 0.75,
                            ""AUD"": 1.35
                        }
                    }")
                });

            // Create the HttpClient using the mock HttpMessageHandler
            var mockHttpClient = new HttpClient(mockHttpMessageHandler.Object);
            mockHttpClient.BaseAddress = new Uri("https://api.exchangeratesapi.io/v2/");
            
            var mockConfigSection = new Mock<IConfigurationSection>();
            mockConfigSection.Setup(x => x.Value).Returns("ExchangeRates:ApiKey");

            // Setup IConfiguration to return the mock section when requested
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x.GetSection("ExchangeRates:ApiKey")).Returns(mockConfigSection.Object);
            
            // Set up the factory to return the mock HttpClient
            mockHttpClientFactory.Setup(f => f.CreateClient("ExchangeRates")).Returns(mockHttpClient);

            // Create the service with the mocked HttpClientFactory and IConfiguration
            var service = new ExchangeRatesService(mockHttpClientFactory.Object, mockConfiguration.Object);

            // Act
            var rates = await service.GetExchangeRates(baseCurrency);

            // Assert
            Assert.NotNull(rates);
            Assert.Contains("EUR", rates);
            Assert.Equal(0.85m, rates["EUR"]);
            Assert.Contains("GBP", rates);
            Assert.Equal(0.75m, rates["GBP"]);
            Assert.Contains("AUD", rates);
            Assert.Equal(1.35m, rates["AUD"]);
        }
    }
}
