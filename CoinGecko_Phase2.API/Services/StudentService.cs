using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoinGecko_Phase2.API
{
    public class StudentService : IStudentServeice
    {
        private readonly MyContext context;
        private readonly IConfiguration configure;
        public StudentService(MyContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configure = configuration;
        }

        public Student Login(string userName, string passWord)
        {
            var q = context.students.SingleOrDefault(x => x.UserName == userName && x.PassWord == passWord);
            if (q != null)
            {
               return q;
            }
            else
            {
                return null;
            }
            
        }
        public string GenerateJWT(Student student)
        {


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
            //ClaimsForToken.Add(new Claim("UserName", student.UserName));
            //ClaimsForToken.Add(new Claim("Password", student.PassWord));
            //ClaimsForToken.Add(new Claim("Name", student.Name));


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
