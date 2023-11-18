namespace CoinGecko_Phase2.API.Reposiroty
{
    public interface IStudentRepository
    {
        int AddStudent(Student student);
        int AddAdmin(Student student);
        Student GetStudent(string userName, string passWord);
        List<Student> GetAllStudents();
         
    }
}
