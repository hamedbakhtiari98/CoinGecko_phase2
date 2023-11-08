using CoinGecko_Phase2.API;
using Microsoft.AspNetCore.Mvc;

namespace CoinGecko_Phase2.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StudnetController : ControllerBase
    {
        MyContext context;
        StudentService studentService;
        public StudnetController()
        {
            context = new MyContext();
            studentService = new StudentService(context);
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
            return student;
        }


        [HttpPost]
        [Route("Login")]
        public bool Login(string userName, string passWord)
        {
            var q = context.students.Any(s=>s.UserName == userName && s.PassWord == passWord);
            if (q)
                return q;
            else
                return false;
        }


    }
}
