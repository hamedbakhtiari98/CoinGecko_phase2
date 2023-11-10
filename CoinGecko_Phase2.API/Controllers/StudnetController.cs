using CoinGecko_Phase2.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoinGecko_Phase2.API.Controllers
{
    [Authorize]
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

        [HttpGet]
        public List<Student> GetStudents()
        {
            return context.students.ToList(); 
        }

        [HttpPost]
        [Route("CreateStudent")]
        public Student CreateStudent([FromBody]Student student) // How about from body
        {
            student.PassWord = Service.HashPass(student.PassWord);
            context.students.Add(student);
            context.SaveChanges();
            List<Student> students = new List<Student>();
            return student;
        }
    }
}
