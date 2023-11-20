namespace CoinGecko_Phase2.API
{
    public class CryptoInfoDTO
    {
        public double? current_price { get; set; }
        public double? market_cap { get; set; }
        public double? total_volume { get; set; }
    }

    public class CryptoInfoWithCoinDTO
    {
        public int CryptoInfoid { get; set; }
        public double? current_price { get; set; }
        public double? market_cap { get; set; }
        public double? total_volume { get; set; }
        public string? last_updated { get; set; }

        //public string id { get; set; }
        //public string symbol { get; set; }
        //public string name { get; set; }

        public CryptoDTO crypto { get; set; }

    }

    public class CryptoDTO
    {
        public string id { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
    }


    public class OhlcWithCrypto
    {
        public int Id { get; set; }
        public string dateTime { get; set; }
        public double? volume { get; set; }
        public double? O { get; set; }
        public double? H { get; set; }
        public double? L { get; set; }
        public double? C { get; set; }
        public string id { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
    }


}
