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
        MyContext context;
        StudentService studentService;
        IConfiguration configuration;
        public StudnetController(IConfiguration configuration)
        {
            context = new MyContext();
            this.configuration = configuration;
            studentService = new StudentService(context,configuration);
        }

        [Authorize]
        [HttpGet]
        [Route("{page}")]
        public List<Student> GetStudents(int page)
        {
            var pageResult = 3f;
            var pageCuont = Math.Ceiling(context.students.Count()/ pageResult);

            var students = context.students
                .Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult)
                .ToList();

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
            context.students.Add(student);
            context.SaveChanges();
            List<Student> students = new List<Student>();

            Log.Information("Create Student Log");
            Log.Information("Student is => {@student}", students);

            return "User added.";
        }
    }
}
