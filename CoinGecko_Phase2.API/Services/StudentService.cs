using CoinGecko_Phase2.API;
using CoinGecko_Phase2.API.Reposiroty;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoinGecko_Phase2.API
{
    public class StudentService : IStudentServeice
    {

        private IStudentRepository studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }


        public List<Student> GetStudnets(int page)
        {
            var q = studentRepository.GetAllStudents();
            var pageResult = 3f;
            var pageCuont = Math.Ceiling(q.Count() / pageResult);

            var students = q
                .Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult)
                .ToList();
            return students;
        }


        public Student Login(string userName, string passWord)
        {
            return studentRepository.GetStudent(userName, passWord);
        }

        public int AddStudent(Student student)
        {
            return studentRepository.AddStudent(student);
        }

        public int AddAdmin()
        {
            var configure = StudentService.InitConfiguration();
            Student admin = new Student()
            {
                Name = configure["Security:Name"],
                UserName = configure["Security:UserName"],
                Email = configure["Security:Email"],
                PhoneNumber = configure["Security:PhoneNumber"],
                Family = configure["Security:Family"],
                PassWord = Service.HashPass(configure["Security:Password"])
            };
            return studentRepository.AddAdmin(admin);
        }

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }

        public string GenerateJWT(string userName, string passWord)
        {
            var student = Login(userName, Service.HashPass(passWord));
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

           // return studentRepository.GenerateJWT(userName, passWord);

        }

        public string ReadJWT(string jwtString)
        {
            //var jwtsample = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI1MSIsIlVzZXJOYW1lIjoiQWRtaW4iLCJQYXNzd29yZCI6Ik5qRFJ3bEdHVnR1UXc2ZVc5NFc0YmFBU2pTREdFbWdXWW9tMmRQRUpUNHc9IiwiTmFtZSI6IkFkbWluIiwibmJmIjoxNjk5OTUyMDk3LCJleHAiOjE2OTk5NTU2OTcsImlzcyI6IkNvaW5HZWNrb19QaGFzZTIuQVBJIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzExNiJ9.UGpoA3Adk42v5RujQUTBYRFHZ-6lu3gbOOSszCcsKak";
            var handler = new JwtSecurityTokenHandler();
            if(jwtString ==  null)
            {
                return null;
            }
            var token = handler.ReadJwtToken(jwtString).Payload.Claims.ToList();
            return token[1].Value;
        }

        public int UpdateStudnet(int id, string name)
        {
            return studentRepository.UpdateStudnet(id, name);
        }
    }
}
