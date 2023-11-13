namespace CoinGecko_Phase2.API
{
    public class AppHostService : IHostedService
    {
        IConfiguration configure;
        MyContext _context;
        MyContext context = new MyContext();
        public AppHostService(IConfiguration configuration)
        {
            this._context = new MyContext();
            this.configure = configuration;

        }
        //IStudentServeice _studentServeice = new StudentService(_context, configuration);
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if(!context.students.Any(c=> c.UserName == "Admin"))
            {
                Student admin = new Student()
                {
                    Name = configure["Security:Name"],
                    UserName = configure["Security:UserName"],
                    Email = configure["Security:Email"],
                    PhoneNumber = configure["Security:PhoneNumber"],
                    Family = configure["Security:Family"],
                    PassWord = Service.HashPass(configure["Security:Password"])
                };
                await context.students.AddAsync(admin);
                await context.SaveChangesAsync();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
