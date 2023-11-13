using RestSharp;
using System.Text.Json;

namespace CoinGecko_Phase2.API
{
    public class CryptoService:ICryptoService
    {
        MyContextCrypto context;
        private readonly RestClient myRestClient = new RestClient("https://api.coingecko.com");
        public CryptoService(MyContextCrypto _context)
        {
            this.context = _context;
        }

        public List<CryptoInfo> GetCryptoInfos(int pageId)
        {
            var pageResult = 5f;
            return context.CryptoInfos.OrderByDescending(c=>c.market_cap)
                .Skip((pageId-1) * (int)pageResult)
                .Take((int)pageResult)
                .ToList();
        }


        public List<Category> GetCategories()
        {

            var request = new RestRequest("api/v3/coins/categories", Method.Get);
            var q = myRestClient.Get(request);
            var category = JsonSerializer.Deserialize<List<Category>>(q.Content);
            return category.ToList();

        }




    }
}
