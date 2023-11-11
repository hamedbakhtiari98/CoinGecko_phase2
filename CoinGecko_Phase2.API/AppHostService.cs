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
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Admin admin = new Admin()
            {
                UserName = configure["Security:UserName"],
                PassWord = configure["Security:Password"]
            };
            context.Admins.Add(admin);   
            context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
