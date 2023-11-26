using Microsoft.AspNetCore.Mvc;

namespace CoinGecko_Phase2.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TestController : ControllerBase
    {
        [Route("index")]
        public IActionResult Index([FromServices] IStudentServeice studentServeice)
        {
            return Ok(studentServeice.GetStudnets(1));
        }
    }
}
