using CoinGecko_Phase2.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CoinGecko_Phase2.API.Controllers
{

    [ApiController]
    [Route("api/Student")]

    public class StudnetController : ControllerBase
    {

        IStudentServeice studentService;
        public StudnetController(IStudentServeice studentServeice)
        {

            this.studentService = studentServeice;
        }

        [HttpGet]
        [Route("{page}")]
        public ActionResult GetStudents(int page)
        {
            var students = studentService.GetStudnets(page);
            Log.Information("Students Information Log");
            Log.Information("Students are => {@students}", students);
            return Ok(students);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("CreateStudent")]
        public ActionResult CreateStudent([FromBody]Student student) // How about from body
        {

            student.PassWord = Service.HashPass(student.PassWord);
            Log.Information("Create Student Log");
            Log.Information("Student is => {@student}", student);
            return Ok(studentService.AddStudent(student));
        }
    }
}
