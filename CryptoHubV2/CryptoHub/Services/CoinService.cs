using CoinGecko.Clients;
using CoinGecko.Entities.Response.Coins;
using Newtonsoft.Json;
using CryptoHub.Models;
using CoinGecko.Entities.Response.Simple;
using CoinGecko.Entities.Response.Search;

namespace CryptoHub.Services;

public class CoinService
{
    private readonly HttpClient _api;
    private readonly CoinGeckoClient _client;

    public CoinService(HttpClient api, CoinGeckoClient client)
    {
        _api = api;
        _client = client;
    }

    public async Task<Price> GetCoinPriceByIdAsync(string id, string currency)
    {
        return await _client.SimpleClient.GetSimplePrice(new[] { id }, new[] { currency });
    }

    public async Task<CoinFullDataById> GetCoinDataByIdAsync(string id)
    {        
        return await _client.CoinsClient.GetAllCoinDataWithId(id);
    }

    //Trending coins
    public async Task<TrendingList> GetTrendingCoinsListAsync()
    {
        return await _client.SearchClient.GetSearchTrending();
    }
}
