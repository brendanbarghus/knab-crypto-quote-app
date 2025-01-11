using System.Diagnostics;
using CryptoQuoteApp.Models;
using CryptoQuoteApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CryptoQuoteApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICryptoHelperService _cryptoHelperService;
    
    public HomeController(ILogger<HomeController> logger, ICryptoHelperService cryptoHelperService)
    {
        _logger = logger;
        _cryptoHelperService = cryptoHelperService;
    }

    public async Task<IActionResult> Index(string cryptoCode)
    {
        if (string.IsNullOrWhiteSpace(cryptoCode))
        {
            ViewBag.Error = "Please enter a valid cryptocurrency code.";
            return View();
        }

        try
        {
            var result = await _cryptoHelperService.RetrieveCryptoQuote(cryptoCode.ToUpper());
            return View(result);
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View();
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }
    
    // public IActionResult Readme()
    // {
    //     return View();
    // }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}