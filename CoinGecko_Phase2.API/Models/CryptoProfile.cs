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
                //.ForMember(dest =>
                //    dest.id,
                //    opt => opt.MapFrom(src => src.Crypto.id))
                //.ForMember(dest =>
                //    dest.name,
                //    opt => opt.MapFrom(src => src.Crypto.name))
                //.ForMember(dest =>
                //    dest.symbol,
                //    opt => opt.MapFrom(src => src.Crypto.symbol));
                .ForMember(dest =>
                    dest.crypto,
                    opt => opt.MapFrom(src => src.Crypto));
        }
    }
    //public class OhlcCryptoDTO : Profile
    //{
    //    /// With automapper, we can only map 1 level not 2 level (dest.crypto) is 1 level and (dest.crypto.name) is 2 level
    //    public OhlcCryptoDTO()
    //    {
    //        CreateMap<CryptoInfo, OhlcWithCryptoDTO>()
    //            .ForMember(dest =>
    //                dest.crypto.name,
    //                opt => opt.MapFrom(src => src.Crypto.name))
    //            .ForMember(dest=>
    //                dest.crypto.symbol,
    //                opt => opt.MapFrom(src => src.Crypto.name))
    //            .ForMember(dest => 
    //                dest.crypto.id,
    //                opt => opt.MapFrom(src=> src.CryptoId));
    //    }
    //}
    public class OhlcWithCryptoProfile : Profile
    {
        public OhlcWithCryptoProfile()
        {
            CreateMap<OhlcWithCrypto, OhlcWithCryptoDTO>()
                .ForPath(dest =>
                    dest.crypto.symbol,
                    opt => opt.MapFrom(src => src.symbol))
                .ForPath(dest =>
                    dest.crypto.name,
                    opt => opt.MapFrom(src => src.name))
                .ForPath(dest =>
                    dest.crypto.id,
                    opt => opt.MapFrom(src => src.CryptoId));
        }
    }

}

