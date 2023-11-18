using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoinGecko_Phase2.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthorizationController : ControllerBase
    {

        //MyContext context;
        //IConfiguration configuration;
        IStudentServeice studentService;
        public AuthorizationController(IConfiguration configuration, IStudentServeice studentServeice )
        {
            //this.configuration = configuration;
            //context = new MyContext();
            this.studentService = studentServeice;
        }



        [HttpGet]
        public IActionResult Get()
        {
            throw new NotImplementedException();
        }


        [HttpPost]
        public string Login([FromBody] LoginViewModel login)
        {
            //var q = studentService.ReadJWT();
            return studentService.GenerateJWT(login.userName, login.passWord);

        }
    }
}
