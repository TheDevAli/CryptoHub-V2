using CoinGecko.Entities.Response.Coins;
using CoinGecko.Entities.Response.Simple;

namespace CryptoHub.Models
{
    public class Portfolio
    {
        public Price CryptoPrice { get; set; }
        public IReadOnlyList<CoinList> CryptoList { get; set; }
    }
}

