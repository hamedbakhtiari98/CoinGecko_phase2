using CoinGecko_Phase2.API.Reposiroty;

namespace CoinGecko_Phase2.API
{
    public class CryptoRepository : ICryptoRepository
    {
        private readonly MyContextCrypto context;
        public CryptoRepository(MyContextCrypto context)
        {
            this.context = context;
        }
        public List<Category> GetCategories()
        {
            return context.categories.ToList();
        }

        public List<CryptoInfo> GetCryptoInfo()
        {
            return context.CryptoInfos.ToList();
        }

        public List<OHLC> GetOHLC()
        {
            return context.oHLCs.ToList();
        }
    }
}
