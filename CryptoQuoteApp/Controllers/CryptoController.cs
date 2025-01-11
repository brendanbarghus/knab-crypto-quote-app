using CryptoQuoteApp.Models;
using CryptoQuoteApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CryptoQuoteApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CryptoController : ControllerBase
{
    private readonly ICryptoHelperService _cryptoHelperService;
    private readonly ILogger<CryptoController> _logger;
    
    public CryptoController(ILogger<CryptoController> logger, ICryptoHelperService cryptoHelperService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _cryptoHelperService = cryptoHelperService;
    }

    /// <summary>
    /// Get Crypto Quotes By CryptoCode
    /// </summary>
    /// <param name="cryptoCode"></param>
    /// <returns></returns>
    
    [HttpGet("quote/{cryptoCode}")]
    [SwaggerOperation(Summary = "Retrieve the latest crypto quote", Description = "Fetch the latest quote for a given cryptocurrency code.")]
    [SwaggerResponse(200, "Returns the crypto quote details", typeof(CryptoQuote))]
    [SwaggerResponse(400, "Invalid cryptocurrency code")]
    [SwaggerResponse(500, "Internal server error")]

    public async Task<IActionResult> GetCryptoQuotes(string cryptoCode)
    {
        try
        {
            if (string.IsNullOrEmpty(cryptoCode))
            {
                _logger.LogWarning("Empty cryptocurrency code received.");
                return BadRequest("Cryptocurrency code cannot be empty.");
            }
            
            var quotes = await  _cryptoHelperService.RetrieveCryptoQuote(cryptoCode);
            return Ok(quotes);
        }
        catch (Exception ex)
        {
            // Log the exception details for further debugging
            _logger.LogError(ex, "An error occurred while fetching the cryptocurrency price.");
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }
}
