using Xunit;
using CoinGecko_Phase2;
using CoinGecko_Phase2.API;
using Microsoft.Extensions.Configuration;
using CoinGecko_Phase2.API.Reposiroty;
using Microsoft.Identity.Client;
using Moq;
using Quartz.Impl.Triggers;
using CoinGecko_Phase2.API.Controllers;

namespace TestProject1
{
    public class UnitTest1
    {
        private IStudentServeice studentServeice;
        private readonly Mock<IStudentRepository> studentRepositoryMock = new Mock<IStudentRepository>();
        private readonly Mock<IStudentServeice> studentServiceMock = new Mock<IStudentServeice>();

        Student sut;
        public UnitTest1()
        {
            studentServeice = new StudentService(studentRepositoryMock.Object);

        }


        //[Theory]
        //[InlineData("Admin","1234")]
        //[InlineData("hamed", "hamed")]
        //[InlineData("ali", "ali")]
        //[InlineData("ghasem", "ghasem")]
        //[InlineData("saeed","saeed")]

        //public void Test1(string userName, string passWord)
        //{

        //    var q = studentServeice.GenerateJWT(userName, passWord);

        //    Assert.NotNull(q);
        //}

        //[Theory]
        //[InlineData("Admin", "1234", true)]
        //[InlineData("hamed", "hamed", true)]
        //[InlineData("ali", "ali",false)]
        //[InlineData("ghasem", "ghasem", false)]
        //[InlineData("saeed", "saeed",false)]
        //public void Test2(string userName, string passWord, bool result)
        //{

        //    bool res = false;
        //    var q = studentServeice.GenerateJWT(userName, passWord);
        //    if(studentServeice.ReadJWT(q) == userName)
        //    {
        //        res = true;
        //    }

        //    Assert.Equal(res, result);
        //}


        [Fact]
        public void Test3()
        {

            // Arrange
            string userName = "hamed";
            string passWord = "hamed";

            Student sut = new Student();
            sut.UserName = userName;
            sut.PassWord = passWord;

            studentRepositoryMock.Setup(x => x.GetStudent(userName, passWord))
                .Returns(sut);

            

            // Act
            var student = studentServeice.Login(userName, passWord);

            // Assert
            Assert.Equal(userName, student.UserName);

        }

        [Fact]
        public void JWTGenerateTest()
        {

            string userName = "string";
            string passWord = "string";
            string expectedJwt = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI1MyIsIlVzZXJOYW1lIjoic3RyaW5nIiwiUGFzc3dvcmQiOiJBYTArc1pSUTJaSmQ2dnJpbHR4NUIxOW04UXBUN3Myd09iY0RyWnNBbXU4PSIsIk5hbWUiOiJzdHJpbmciLCJuYmYiOjE3MDA0NjA4MjUsImV4cCI6MTcwMDQ2NDQyNSwiaXNzIjoiQ29pbkdlY2tvX1BoYXNlMi5BUEkiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo3MTE2In0.3c-PjwQTRoaV6tBkK8zy-naLuYvtrVqzTgkY3U_R98Y";

            sut = new Student
            {
                UserName = userName,
                PassWord = passWord,
                Email = "",
                Family = "",
                Name = "",
                PhoneNumber = "1234567890",
                StudentId = 123
            };

            studentRepositoryMock.Setup(x => x.GetStudent(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(sut);

            var studentJwtToken = studentServeice.GenerateJWT(userName,passWord);

            //Assert.Equal(expectedJwt, studentJwtToken);
            Assert.NotNull(studentJwtToken);

        }

        [Fact]
        public void JWTGenerateTest2()
        {

            string userName = "string";
            string passWord = "string";
            string expectedJwt = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI1MyIsIlVzZXJOYW1lIjoic3RyaW5nIiwiUGFzc3dvcmQiOiJBYTArc1pSUTJaSmQ2dnJpbHR4NUIxOW04UXBUN3Myd09iY0RyWnNBbXU4PSIsIk5hbWUiOiJzdHJpbmciLCJuYmYiOjE3MDA0NjA4MjUsImV4cCI6MTcwMDQ2NDQyNSwiaXNzIjoiQ29pbkdlY2tvX1BoYXNlMi5BUEkiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo3MTE2In0.3c-PjwQTRoaV6tBkK8zy-naLuYvtrVqzTgkY3U_R98Y";

            sut = new Student
            {
                UserName = userName,
                PassWord = passWord,
                Email = "",
                Family = "",
                Name = "",
                PhoneNumber = "1234567890",
                StudentId = 123
            };

            studentRepositoryMock.Setup(x => x.GenerateJWT(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(expectedJwt);

            var studentJwtToken = studentServeice.GenerateJWT(userName,passWord);


            Assert.Equal(expectedJwt, studentJwtToken);
            //Assert.NotNull(studentJwtToken);

        }


    }
}