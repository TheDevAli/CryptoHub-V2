using CoinGecko.Clients;
using CoinGecko.Entities.Response.Coins;
using Newtonsoft.Json;
using CryptoHub.Models;
using CoinGecko.Entities.Response.Simple;
using CoinGecko.Entities.Response.Search;
using CoinGecko.Interfaces;

namespace CryptoHub.Services;

public class CryptoService
{
    private readonly HttpClient _api;
    private readonly CoinGeckoClient _client;

    public CryptoService(HttpClient api, CoinGeckoClient client)
    {
        _api = api;
        _client = client;
    }

    public async Task<Price> GetCoinPriceByIdAsync(string id, string currency)
    {
        return await _client.SimpleClient.GetSimplePrice(new[] { id }, new[] { currency });
    }

    //Trending coins
    public async Task<TrendingList> GetTrendingCoinsListAsync()
    {
        return await _client.SearchClient.GetSearchTrending();
    }

    //Live Market
    public async Task<List<CoinMarkets>> GetMarketDataAsync()
    {
        //Create an instance of System.Net.HttpClient and add our request header
        HttpClient hClient = new HttpClient();
        hClient.DefaultRequestHeaders.Add("User-Agent", "CryptoHub");

        //Pass our HttpClient instance to the CoinCecko client
        ICoinGeckoClient cgClient = new CoinGeckoClient(hClient);

        return await cgClient.CoinsClient.GetCoinMarkets("usd");
    }

    // Search for coin
    public async Task<IReadOnlyList<CoinList>> GetCoinListAsync() // not using this yet but works 
    {
        return await _client.CoinsClient.GetCoinList();
    }

    public async Task<CoinFullDataById> GetCoinDataByIdAsync(string id)
    {
        return await _client.CoinsClient.GetAllCoinDataWithId(id);
    }
}
