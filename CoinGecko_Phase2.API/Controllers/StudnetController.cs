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
        //MyContext context;
        StudentService studentService;
        IConfiguration configuration;
        public StudnetController(IConfiguration configuration, StudentService studentServeice)
        {
            //context = new MyContext();
            //this.configuration = configuration;
            this.studentService = studentServeice;
        }

        [HttpGet]
        [Route("{page}")]
        public List<Student> GetStudents(int page)
        {
            var students = studentService.GetStudnets(page);

            Log.Information("Students Information Log");
            Log.Information("Students are => {@students}", students);


            return students;


        }

        [AllowAnonymous]
        [HttpPost]
        [Route("CreateStudent")]
        public string CreateStudent([FromBody]Student student) // How about from body
        {
            //if(context.students.Any(c=> c.UserName == student.UserName))
            //{
            //    return "User with this username exists";
            //}


            student.PassWord = Service.HashPass(student.PassWord);
            studentService.AddStudent(student);

            Log.Information("Create Student Log");
            Log.Information("Student is => {@student}", student);

            return "User added.";
        }





    }
}
