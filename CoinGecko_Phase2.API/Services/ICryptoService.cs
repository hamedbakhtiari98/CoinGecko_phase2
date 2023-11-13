namespace CoinGecko_Phase2.API
{
    public interface ICryptoService
    {
        List<CryptoInfo> GetCryptoInfos(int pageId);
        List<Category> GetCategories();


    }
}
