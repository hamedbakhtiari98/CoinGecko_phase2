using CoinGecko_Phase2.API.Models;

namespace CoinGecko_Phase2.API
{
    public interface ICryptoService
    {
        List<CryptoInfoWithCoinDTO> GetCryptoInfos(int pageId);
        List<Category> GetCategories(int page);
        OhlcWithCrypto GetOHLC(string id, string date);
        OHLC? GetOHLCByStoreProcedure3(string CryptoIdd, string dateTime);
        OhlcWithCryptoDTO? GetOHLCByStoreProcedure5(string CryptoId, string dateTime);


    }
}
