using Microsoft.EntityFrameworkCore;

namespace CoinGecko_Phase2.API
{
    public class MyContext:DbContext
    {

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=DESKTOP-G7IA4K7; Initial Catalog=StudentDb; Integrated Security=True; TrustServerCertificate=True");
        //}


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
        public MyContextCrypto(DbContextOptions<MyContextCrypto> options): base(options)
        {
                
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=DESKTOP-G7IA4K7; Initial Catalog=CryptoDB; Integrated Security=True; TrustServerCertificate=True");
        //}


        #region Tables

        public DbSet<Crypto> Cryptos { get; set; }
        public DbSet<CryptoInfo> CryptoInfos { get; set; }
        public DbSet<OHLC> oHLCs { get; set; }
        public DbSet<Category> categories { get; set; }

        #endregion


    }

}
