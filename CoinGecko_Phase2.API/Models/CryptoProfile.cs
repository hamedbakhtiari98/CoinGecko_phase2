using AutoMapper;

namespace CoinGecko_Phase2.API.Models
{
    public class CryptoProfile : Profile
    {
        public CryptoProfile()
        {
            CreateMap<CryptoInfo, CryptoInfoDTO>();
        }
    }

    public class CryptoInformationProfile : Profile
    {
        public CryptoInformationProfile()
        {
            CreateMap<CryptoInfo, CryptoInfoWithCoinDTO>()
                .ForMember(dest =>
                    dest.id,
                    opt => opt.MapFrom(src => src.Crypto.id))
                .ForMember(dest =>
                    dest.name,
                    opt => opt.MapFrom(src => src.Crypto.name))
                .ForMember(dest =>
                    dest.symbol,
                    opt => opt.MapFrom(src => src.Crypto.symbol));
        }
    }

    public class OhlcWithCryptoProfile : Profile
    {
        public OhlcWithCryptoProfile()
        {
            CreateMap<OHLC, OhlcWithCrypto>()
                .ForMember(dest =>
                    dest.id,
                    opt => opt.MapFrom(src => src.Crypto.id))
                .ForMember(dest =>
                    dest.name,
                    opt => opt.MapFrom(src => src.Crypto.name))
                .ForMember(dest =>
                    dest.symbol,
                    opt => opt.MapFrom(src => src.Crypto.symbol));
        }
    }


}

