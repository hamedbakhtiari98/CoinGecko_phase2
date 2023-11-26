using Microsoft.EntityFrameworkCore;

namespace CoinGecko_Phase2.API
{
    public class MyContext:DbContext
    {
        private readonly IConfiguration _configuration;

        public MyContext(DbContextOptions<MyContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MyStudentDbConnectionString"));
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasIndex(p => p.UserName)
                .IsUnique(true);

        }



        #region Tables

        public DbSet<Student> students { get; set; }

        #endregion


    }


    public class MyContextCrypto : DbContext
    {
        private readonly IConfiguration _configuration;

        public MyContextCrypto(DbContextOptions<MyContextCrypto> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MyCryptoDbConnectionString"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Crypto>()
            //    .HasOne(c => c.CryptoInfo)
            //    .WithOne(c => c.Crypto);

        }

        #region Tables

        public DbSet<Crypto> Cryptos { get; set; }
        public DbSet<CryptoInfo> CryptoInfos { get; set; }
        public DbSet<OHLC> oHLCs { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<OhlcWithCrypto> ohlcWithCryptos { get; set; }

        #endregion


    }

}
