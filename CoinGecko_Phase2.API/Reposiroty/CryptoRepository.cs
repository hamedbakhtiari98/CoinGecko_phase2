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

        public List<CryptoInfoWithCoinDTO> GetCryptoInfo()
        {
         //   return context.CryptoInfos.Include(c => c.Crypto).ToList();
            return context.CryptoInfos
                .Select(c => new CryptoInfoWithCoinDTO
                {
                    crypto = new CryptoDTO() { id = c.Crypto.id, name = c.Crypto.name, symbol = c.Crypto.symbol },
                    current_price = c.current_price,
                    CryptoInfoid = c.CryptoInfoid,
                    last_updated = c.last_updated,
                    market_cap = c.market_cap,
                    total_volume = c.total_volume
                }).ToList();
        }

        public OHLC GetOHLC(string id, string date)
        {
            return context.oHLCs.Where(o => o.CryptoId == id && o.dateTime == date).Include(c => c.Crypto).SingleOrDefault();
        }


        public async Task<IEnumerable<object>> Projection()
        {
            return context.CryptoInfos
                .Select(c => new CryptoInfo
                {
                    Crypto = new Crypto() { id = c.Crypto.id, name = "", symbol = "" },
                    CryptoId = c.CryptoId,
                    current_price = c.current_price,
                }).ToList();
        }
    }
}
