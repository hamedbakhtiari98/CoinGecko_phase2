namespace CoinGecko_Phase2.API
{
    public interface IStudentServeice
    {
        Student Login(string userName, string passWord);
        string GenerateJWT(Student student);
        void CreateAdmin();

    }
}
