namespace CryptoHub.Models
{
    public class Coin
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Symbol { get; set; }

        public decimal Price { get; set; } = 0;

    }
}
