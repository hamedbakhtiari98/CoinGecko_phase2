using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoinGecko_Phase2.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthorizationController : ControllerBase
    {

        MyContext context;
        StudentService studentService;
        IConfiguration configuration;
        public AuthorizationController(IConfiguration configuration)
        {
            this.configuration = configuration;
            context = new MyContext();
            studentService = new StudentService(context, configuration);
        }


        [HttpPost]
        public IActionResult Login([FromBody] LoginViewModel login)
        {
            var q = studentService.Login(login.userName, Service.HashPass(login.passWord));
            if (q != null)
            {
                return Ok(studentService.GenerateJWT(q));
            }

           return NotFound();

        }
    }
}
