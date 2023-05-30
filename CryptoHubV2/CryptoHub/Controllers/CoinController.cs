using CoinGecko.Clients;
using CoinGecko.Entities.Response.Coins;
using CoinGecko.Entities.Response.Search;
using CryptoHub.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CryptoHub.Controllers;

public class CoinController : Controller
{
    private readonly CoinService _service;
    public CoinController(CoinService service)
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

    // Trending coins /Coin/Trending
    public async Task<IActionResult> Trending()
    {
        var res = await _service.GetTrendingCoinsListAsync();

        return View("TrendingCoins", res);
    }

    // Live Market --- /Coin/Market
    public async Task<IActionResult> Market()
    {
        var res = await _service.GetMarketDataAsync();

        return View("Market", res);
    }

    // Search coin /Coin/Search
    public async Task<IActionResult> Search(string? searchString)
    {
        var res = await _service.GetCoinListAsync();

        if (string.IsNullOrWhiteSpace(searchString))
        {
            return View("Search", res);
        }

        res = res.Where(r => r.Name == searchString).ToList().AsReadOnly();
        return View("Search", res);
    }

    // Search Coin /Coin/Coindata
    public async Task<IActionResult> CoinData(string id)
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

        return View("CoinData", res);
        
    }

}
