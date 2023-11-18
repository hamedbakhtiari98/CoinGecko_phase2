using CoinGecko_Phase2.API.Reposiroty;
using Microsoft.EntityFrameworkCore;

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
            return context.CryptoInfos.Include(c=>c.Crypto).ToList();
        }

        public OHLC GetOHLC(string id, string date)
        {
            return context.oHLCs.Where(o => o.CryptoId == id && o.dateTime == date).Include(c=>c.Crypto).SingleOrDefault();
        }
    }
}
