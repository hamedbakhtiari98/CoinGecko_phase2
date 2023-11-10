using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoinGecko_Phase2.API
{
    public class Crypto
    {

        [Key]
        public string id { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public CryptoInfo? CryptoInfo { get; set; }

    }


    public class CryptoInfo
    {
        [Key]
        public int CryptoInfoid { get; set; }
        public double? current_price { get; set; }
        public double? market_cap { get; set; }
        public double? total_volume { get; set; }
        public string? last_updated { get; set; }


        [ForeignKey("id")]
        public string CryptoId { get; set; }
        public Crypto Crypto { get; set; }

    }
}
