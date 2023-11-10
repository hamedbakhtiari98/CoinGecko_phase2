namespace CoinGecko_Phase2.API
{
    public class CryptoService:ICryptoService
    {
        MyContextCrypto context;

        public CryptoService(MyContextCrypto _context)
        {
            this.context = _context;
        }

        public List<CryptoInfo> GetCryptoInfos()
        {
            return context.CryptoInfos.OrderByDescending(c=>c.market_cap).Take(10).ToList();
        }
    }
}
