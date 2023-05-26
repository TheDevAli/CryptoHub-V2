using CoinGecko.Clients;
using CoinGecko.Entities.Response.Coins;
using CoinGecko.Entities.Response.Search;
using CryptoHub.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

    public async Task<IActionResult> Trending()
    {
        var res = await _service.GetTrendingCoinsListAsync();

        return View("TrendingCoins", res);
    }
}
