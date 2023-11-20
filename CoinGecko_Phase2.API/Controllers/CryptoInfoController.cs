using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CoinGecko_Phase2.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class CryptoInfoController : ControllerBase
    {

        ICryptoService cryptoService;
        IMapper mapper;
        public CryptoInfoController(IMapper mapper, ICryptoService cryptoService)
        {
            this.cryptoService = cryptoService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("GetInformation/{page}")]

        public IActionResult GetInformation(int page)
        {
           var cryptoInformation = cryptoService.GetCryptoInfos(page);
            if (cryptoInformation == null)
            {
                return NotFound();
            }

            Log.Information("Crypto Information Log");
            Log.Information("Crypto Informations are => {@coinInformation}", cryptoInformation);
            return Ok(cryptoInformation);
        }



        [HttpGet]
        [Route("GetOHLC/{id}/{date}")]
        public ActionResult GetOHLC(string id, string date)
        {

            var ohlcInformation = cryptoService.GetOHLC(id, date);

            Log.Information("Crypto Information Log");
            Log.Information("Crypto Informations are => {@ohlcInformation}", ohlcInformation);
            if (ohlcInformation == null)
            {
                return NotFound();
            }

            return Ok(ohlcInformation);
        }

        //[Authorize(Policy = "adminPolicy")]
        [HttpGet]
        [Route("GetCategory/{page}")]
        public IActionResult GetCategories(int page)
        {

            var category = cryptoService.GetCategories(page);

            Log.Information("Crypto Information Log");
            Log.Information("Crypto Informations are => {@category}", category);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);


        }


        //[HttpGet]
        //[Route("{page}")]
        //public IActionResult GetCryptoInfo(int page)
        //{

        //    var q = cryptoService.GetCryptoInfos(page);
        //    var cryptoInfoDTO = mapper.Map<List<CryptoInfoDTO>>(q);
        //    Log.Information("Crypto Information Log");
        //    Log.Information("Crypto Informations are => {@cryptoInfoDTO}", cryptoInfoDTO);
        //    if(cryptoInfoDTO == null) {  return NotFound(); }
        //    return Ok(cryptoInfoDTO);

        //}
    }
}
