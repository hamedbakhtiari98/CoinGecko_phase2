namespace CoinGecko_Phase2.API
{
    public interface ICryptoService
    {
        List<CryptoInfoWithCoinDTO> GetCryptoInfos(int pageId);
        List<Category> GetCategories(int page);
        OhlcWithCrypto GetOHLC(string id, string date);
    }
}
