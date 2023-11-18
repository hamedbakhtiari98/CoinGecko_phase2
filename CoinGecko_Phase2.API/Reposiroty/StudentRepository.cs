using CoinGecko_Phase2.API.Reposiroty;

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
    }
}
