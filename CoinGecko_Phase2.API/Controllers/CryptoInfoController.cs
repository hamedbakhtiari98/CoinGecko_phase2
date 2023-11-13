using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CoinGecko_Phase2.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class CryptoInfoController : ControllerBase
    {
        MyContextCrypto context;
        ICryptoService cryptoService;
        IMapper mapper;
        public CryptoInfoController(IMapper mapper)
        {
            context = new MyContextCrypto();
            cryptoService = new CryptoService(context);
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("GetInformation/{page}")]

        public List<CryptoInfo> GetInformation(int page)
        {
            var pageResult = 5f;
            var coinInformation = context.CryptoInfos.OrderByDescending(c => c.market_cap)
                .Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult)
                .ToList();

            Log.Information("Crypto Information Log");
            Log.Information("Crypto Informations are => {@coinInformation}", coinInformation);
            return coinInformation;

        }



        [HttpGet]
        [Route("GetOHLC/{id}/{date}")]
        public OHLC GetOHLC(string id, string date)
        {
            var ohlcInformation = context.oHLCs.Where(o => o.CryptoId == id && o.dateTime == date).SingleOrDefault();

            Log.Information("Crypto Information Log");
            Log.Information("Crypto Informations are => {@ohlcInformation}", ohlcInformation);

            return ohlcInformation;

        }



        [Authorize(Policy = "adminPolicy")]
        [HttpGet]
        [Route("GetCategory/{page}")]
        public List<Category> GetCategories(int page)
        {
            var pageResult = 5f;
            var category = context.categories
                .Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult)
                .ToList();

            Log.Information("Crypto Information Log");
            Log.Information("Crypto Informations are => {@category}", category);

            return category;
        }


        [HttpGet]
        [Route("{page}")]
        public List<CryptoInfoDTO> GetCryptoInfo(int page)
        {

            var q = cryptoService.GetCryptoInfos(page);
            var cryptoInfoDTO = mapper.Map<List<CryptoInfoDTO>>(q);
            Log.Information("Crypto Information Log");
            Log.Information("Crypto Informations are => {@cryptoInfoDTO}", cryptoInfoDTO);
            return cryptoInfoDTO;

        }







    }
}
