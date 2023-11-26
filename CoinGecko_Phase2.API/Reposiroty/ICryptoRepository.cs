namespace CoinGecko_Phase2.API.Reposiroty
{
    public interface ICryptoRepository
    {
        List<Category> GetCategories();
        OHLC GetOHLC(string id, string date);
        List<CryptoInfoWithCoinDTO> GetCryptoInfo();
        OHLC? GetOHLCByStoreProcedure(string CryptoId, string dateTime);
        OhlcWithCrypto? GetOHLCByStoreProcedure5(string CryptoId, string dateTime);
      //  OHLC? GetOHLCByStoreProcedure4(string CryptoId, string dateTime);
        //OHLC? GetOHLCByStoreProcedure2(string CryptoIdd, string dateTime);
        //OhlcWithCryptoDTO? GetOHLCByStoreProcedure3(string CryptoIdd, string dateTime);
    }
}
