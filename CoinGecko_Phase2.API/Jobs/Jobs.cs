using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;


namespace CoinGecko_Phase2.API
{
    public class Jobs : IJob
    {
        MyContext _context;
        IConfiguration configuration;
        IStudentServeice studentServeice;
        public Jobs(MyContext context, IConfiguration configuration, StudentService studentService)
        {
            this._context = context;
            this.configuration = configuration;
            this.studentServeice = studentService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            string userName = configuration["Security:UserName"];
            string passWord = configuration["Security:PassWord"];
            return Task.CompletedTask;
        }
    }

    public class DoJob
    {
        //StdSchedulerFactory factory = new StdSchedulerFactory();
        //IScheduler scheduler = await factory.GetScheduler();
    }
}
