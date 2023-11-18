namespace CoinGecko_Phase2.API.Reposiroty
{
    public interface ICryptoRepository
    {
        List<Category> GetCategories();
        OHLC GetOHLC(string id, string date);
        List<CryptoInfo> GetCryptoInfo();
    }
}
