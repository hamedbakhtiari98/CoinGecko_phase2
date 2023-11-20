using CoinGecko_Phase2.API.Reposiroty;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoinGecko_Phase2.API
{
    public class StudentRepository : IStudentRepository
    {
        private readonly MyContext context;

        public StudentRepository(MyContext context)
        {
                this.context = context;
        }


        public int AddAdmin(Student student)
        {
            context.students.Add(student);
            return context.SaveChanges();
        }

        public int AddStudent(Student student)
        {
            context.students.Add(student);
            return context.SaveChanges();
        }

        public List<Student> GetAllStudents()
        {
            return context.students.ToList();
        }

        public Student GetStudent(string userName, string passWord)
        {
            return context.students.SingleOrDefault(s=>s.UserName ==  userName && s.PassWord == passWord);  
        }


        public string GenerateJWT(string userName, string PassWord)
        {

            Student student = GetStudent(userName, PassWord);

            //Student student = new Student
            //{
            //    StudentId = 12,
            //    UserName = userName,
            //    PassWord = PassWord,
            //    Email = "student@stu.com",
            //    Name = "test",
            //    Family = "test",
            //    PhoneNumber = "09127338586"
            //};

            var configure = StudentService.InitConfiguration();


            if (student == null)
            {
                return null;
            }

            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(configure["JwtSettings:Key"]));

            //byte[] test = Encoding.ASCII.GetBytes(configure["JwtSettings:Issuer"]);

            //string test1 = test[1].ToString();
            //string test2 = test[2].ToString();

            var signInCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256
                );
            var ClaimsForToken = new List<Claim>();
            ClaimsForToken.Add(new Claim("UserId", student.StudentId.ToString()));
            ClaimsForToken.Add(new Claim("UserName", student.UserName));
            ClaimsForToken.Add(new Claim("Password", student.PassWord));
            ClaimsForToken.Add(new Claim("Name", student.Name));

            //var q = configure["JwtSettings:Issuer"];

            var jwtSecurityToken = new JwtSecurityToken(
                configure["JwtSettings:Issuer"],
                configure["JwtSettings:Audience"],
                ClaimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signInCredentials
            );

            var token = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            return token;
        }

    }
}
