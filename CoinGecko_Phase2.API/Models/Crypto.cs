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


    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string? id { get; set; }
        public string? name { get; set; }
        public float? market_cap { get; set; }
        public double? volume_24h { get; set; }
        public string? updated_at { get; set; }
    }


    public class OHLC
    {
        [Key]
        public int Id { get; set; }
        public string dateTime { get; set; }
        public double? volume { get; set; }
        public double? O { get; set; }
        public double? H { get; set; }
        public double? L { get; set; }
        public double? C { get; set; }

        [ForeignKey("id")]
        public string CryptoId { get; set; }
        public Crypto Crypto { get; set; }
    }
}
