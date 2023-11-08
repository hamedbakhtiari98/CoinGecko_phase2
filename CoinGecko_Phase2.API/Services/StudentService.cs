namespace CoinGecko_Phase2.API
{
    public class StudentService:IStudentServeice
    {
        private readonly MyContext context;

        public StudentService(MyContext context)
        {
            this.context = context;
        }


        public bool Login(string userName, string passWord)
        {
            return context.students.Any(x => x.UserName == userName && x.PassWord == passWord);
        }
    }
}
