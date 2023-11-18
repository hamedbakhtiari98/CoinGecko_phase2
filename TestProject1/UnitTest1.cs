using Xunit;
using CoinGecko_Phase2;
using CoinGecko_Phase2.API;
using Microsoft.Extensions.Configuration;

namespace TestProject1
{
    public class UnitTest1
    {





        //IConfiguration _configuration;
        //public UnitTest1(IConfiguration configuration)
        //{
        //        this._configuration = configuration;
        //}

        [Theory]
        [InlineData("Admin","1234")]
        [InlineData("hamed", "hamed")]
        [InlineData("ali", "ali")]
        [InlineData("ghasem", "ghasem")]
        [InlineData("saeed","saeed")]

        public void Test1(string userName, string passWord)
        {
            var config = CoinGecko_Phase2.API.StudentService.InitConfiguration();

            MyContext context = new MyContext();
            CoinGecko_Phase2.API.StudentService test = new CoinGecko_Phase2.API.StudentService(context, config);

            var q = test.GenerateJWT(userName, passWord);

            Assert.NotNull(q);
        }

        [Theory]
        [InlineData("Admin", "1234", true)]
        [InlineData("hamed", "hamed", true)]
        [InlineData("ali", "ali",false)]
        [InlineData("ghasem", "ghasem", false)]
        [InlineData("saeed", "saeed",false)]
        public void Test2(string userName, string passWord, bool result)
        {
            var config = CoinGecko_Phase2.API.StudentService.InitConfiguration();
            MyContext context = new MyContext();
            CoinGecko_Phase2.API.StudentService test = new CoinGecko_Phase2.API.StudentService(context, config);
            bool res = false;
            var q = test.GenerateJWT(userName, passWord);
            if(test.ReadJWT(q) == userName)
            {
                res = true;
            }

            Assert.Equal(res, result);
        }


    }
}