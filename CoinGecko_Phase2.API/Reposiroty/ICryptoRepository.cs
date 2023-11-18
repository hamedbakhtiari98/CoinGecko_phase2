namespace CoinGecko_Phase2.API.Reposiroty
{
    public interface ICryptoRepository
    {
        List<Category> GetCategories();
        List<OHLC> GetOHLC();
        List<CryptoInfo> GetCryptoInfo();
    }
}
