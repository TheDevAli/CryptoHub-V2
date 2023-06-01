using CoinGecko.Clients;
using CoinGecko.Entities.Response.Coins;
using CoinGecko.Entities.Response.Search;
using CryptoHub.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;

namespace CryptoHub.Controllers;

public class CryptoController : Controller
{
    private readonly CryptoService _service;
    public CryptoController(CryptoService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index(string? id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return NotFound();
        }

        var res = await _service.GetCoinDataByIdAsync(id);

        if (res == null)
        {
            return NotFound();
        }

        return View(res);
    }

    // Trending coins /Crypto/Trending
    public async Task<IActionResult> Trending()
    {
        var res = await _service.GetTrendingCoinsListAsync();

        return View("TrendingCoins", res);
    }

    // Live Market --- /Crypto/Market
    public async Task<IActionResult> Market()
    {
        var res = await _service.GetMarketDataAsync();

        return View("Market", res);
    }

    // Search Crypto /Crypto/Search
    public async Task<IActionResult> Search(string? searchString)
    {
        var res = await _service.GetCoinListAsync();

        if (string.IsNullOrWhiteSpace(searchString))
        {
            return View("Search", res);
        }

        res = res.Where(r => r.Name.ToLower().Contains(searchString.ToLower())).ToList().AsReadOnly();

        return View("Search", res);
    }

    // Search Crypto /Crypto/Cryptodata
    public async Task<IActionResult> CryptoData(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return NotFound();
        }

        var res = await _service.GetCoinDataByIdAsync(id);

        if (res == null)
        {
            return NotFound();
        }

        return View("CryptoData", res);    
    }

    
    public async Task<IActionResult> Portfolio()
    {
        var res = await _service.PortfolioGeneratorAsync();
        return View("Portfolio", res);
    }

}
