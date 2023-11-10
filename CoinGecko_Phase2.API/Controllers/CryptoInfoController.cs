using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoinGecko_Phase2.API.Controllers
{
    //[Authorize]
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
        public List<CryptoInfoDTO> GetCryptoInfo()
        {
            var q = cryptoService.GetCryptoInfos();

            var cryptoInfoDTO = mapper.Map<List<CryptoInfoDTO>>(q);

            return cryptoInfoDTO;
        }
    }
}
