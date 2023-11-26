using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data.Entity.Infrastructure;

namespace CoinGecko_Phase2.API.Reposiroty
{
    public interface IStudentRepository
    {
        int AddStudent(Student student);
        int AddAdmin(Student student);
        Student GetStudent(string userName, string passWord);
        List<Student> GetAllStudents();
        string GenerateJWT(string userName, string PassWord);
        Student GetStudentByUsername(string userName);
        //EntityEntry<Student> GetStudentEntryStatus(Student student);
        int UpdateStudnet(int id, string name);


    }
}
