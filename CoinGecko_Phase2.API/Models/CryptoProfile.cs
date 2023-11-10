using AutoMapper;

namespace CoinGecko_Phase2.API.Models
{
    public class CryptoProfile:Profile
    {
        public CryptoProfile()
        {
            CreateMap<CryptoInfo, CryptoInfoDTO>();
        }
    }
}

