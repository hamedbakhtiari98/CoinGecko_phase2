using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoinGecko_Phase2.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthorizationController : ControllerBase
    {

        IStudentServeice studentService;
        public AuthorizationController(IStudentServeice studentServeice )
        {
            this.studentService = studentServeice;
        }

        [HttpPost]
        public string Login([FromBody] LoginViewModel login)
        {
            return studentService.GenerateJWT(login.userName, login.passWord);
        }
    }
}
