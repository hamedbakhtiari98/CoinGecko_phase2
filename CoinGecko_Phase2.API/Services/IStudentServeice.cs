﻿namespace CoinGecko_Phase2.API
{
    public interface IStudentServeice
    {
        List<Student> GetStudnets(int page);
        Student Login(string userName, string passWord);
        public string GenerateJWT(string userName, string PassWord);
        int AddAdmin();
        int AddStudent(Student student);
        string ReadJWT(string jwtString);
    }
}
