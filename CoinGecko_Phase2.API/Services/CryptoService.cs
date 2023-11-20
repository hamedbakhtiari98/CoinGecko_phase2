using AutoMapper;
using CoinGecko_Phase2.API.Reposiroty;
using RestSharp;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CoinGecko_Phase2.API
{
    public class CryptoService:ICryptoService
    {
        private readonly IMapper mapper;
        private readonly ICryptoRepository cryptoRepository;

        public CryptoService(ICryptoRepository cryptoRepository, IMapper mapper)
        {
            this.cryptoRepository = cryptoRepository;
            this.mapper = mapper;
        }

        public List<CryptoInfoWithCoinDTO> GetCryptoInfos(int pageId)
        {
            var pageResult = 5f;
            var q = cryptoRepository.GetCryptoInfo().OrderByDescending(c=>c.market_cap)
                .Skip((pageId-1) * (int)pageResult)
                .Take((int)pageResult)
                .ToList();
            var cryptoInfoWithCrypto = mapper.Map<List<CryptoInfoWithCoinDTO>> (q);
            return q;
        }


        public List<Category> GetCategories(int page)
        {


            var pageResult = 5f;
            var category = cryptoRepository.GetCategories()
                .Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult)
                .ToList();
            return category;
        }

        public OhlcWithCrypto GetOHLC(string id, string date)
        {
            var q = cryptoRepository.GetOHLC(id, date);
            var cryptoInfoWithCrypto = mapper.Map<OhlcWithCrypto>(q);
            return cryptoInfoWithCrypto;

        }
    }
}
