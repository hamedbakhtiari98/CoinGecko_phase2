using Microsoft.EntityFrameworkCore;

namespace CoinGecko_Phase2.API
{
    public class MyContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-G7IA4K7; Initial Catalog=StudentDb; Integrated Security=True; TrustServerCertificate=True");
        }


        #region Tables

        public DbSet<Student> students { get; set; }


        #endregion


    }
}
