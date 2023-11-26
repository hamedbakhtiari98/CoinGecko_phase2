using CoinGecko_Phase2.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CoinGecko_Phase2.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/Student")]

    public class StudnetController : ControllerBase
    {

        IStudentServeice studentService;
        private readonly IConfiguration configuration;
        public StudnetController(IStudentServeice studentServeice, IConfiguration configuration)
        {

            this.studentService = studentServeice;
            this.configuration = configuration;
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
        public ActionResult CreateStudent([FromBody] Student student) // How about from body
        {

            student.PassWord = Service.HashPass(student.PassWord);
            Log.Information("Create Student Log");
            Log.Information("Student is => {@student}", student);
            return Ok(studentService.AddStudent(student));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("UpdateStudent/{id}/{name}")]
        public ActionResult UpdateStudent(int id, string name)
        {
            return Ok(studentService.UpdateStudnet(id, name));
        }

        [AllowAnonymous]
        [Route("test")]
        public ActionResult Test()
        {
            var q = configuration["ConnectionStrings:MyStudentDbConnectionString"];
            var q1 = configuration["properties:versions:type"];
            return Ok(q);
        }

    }
}
