using System.ComponentModel.DataAnnotations;

namespace CoinGecko_Phase2.API
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }
}
