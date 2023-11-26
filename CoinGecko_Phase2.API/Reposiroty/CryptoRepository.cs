using CoinGecko_Phase2.API.Models;
using CoinGecko_Phase2.API.Reposiroty;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace CoinGecko_Phase2.API
{
    public class CryptoRepository : ICryptoRepository
    {
        private readonly MyContextCrypto context;
        public CryptoRepository(MyContextCrypto context)
        {
            this.context = context;
        }
        public List<Category> GetCategories()
        {
            return context.categories.ToList();
        }

        public List<CryptoInfoWithCoinDTO> GetCryptoInfo()
        {
            var q = context.CryptoInfos.Include(c => c.Crypto).ToList();
            return context.CryptoInfos
                .Select(c => new CryptoInfoWithCoinDTO
                {
                    crypto = new CryptoDTO() { id = c.Crypto.id, name = c.Crypto.name, symbol = c.Crypto.symbol },
                    current_price = c.current_price,
                    CryptoInfoid = c.CryptoInfoid,
                    last_updated = c.last_updated,
                    market_cap = c.market_cap,
                    total_volume = c.total_volume
                }).ToList();
        }

        public OHLC GetOHLC(string id, string date)
        {
            var q = context.oHLCs.Where(o => o.CryptoId == id && o.dateTime == date).Include(c => c.Crypto).SingleOrDefault();
            return q;
        }


        public async Task<IEnumerable<object>> Projection()
        {
            return context.CryptoInfos
                .Select(c => new CryptoInfo
                {
                    Crypto = new Crypto() { id = c.Crypto.id, name = "", symbol = "" },
                    CryptoId = c.CryptoId,
                    current_price = c.current_price,
                }).ToList();
        }


       // main
        public OHLC? GetOHLCByStoreProcedure(string CryptoId, string dateTime)
        {
            OHLC oHLC = null;
            // معادل دستور include در Sql command چیست؟
            // چرا به دستور include ایراد میگیره خط زیر؟
            var q = context.oHLCs.FromSqlInterpolated($"GetOHLCBy_ID_Date @CryptoId = {CryptoId}, @dateTime = {dateTime}").ToList();
            if (q != null)
            {
                oHLC = q.FirstOrDefault();
                Console.Out.WriteLine(oHLC);
            }
            return oHLC;
        }

        public OhlcWithCrypto? GetOHLCByStoreProcedure5(string CryptoId, string dateTime)
        {
            OhlcWithCrypto oHLC = null;
            //بازهم ارور کلید مشترک. لعنت به روحت 
            var q = context.ohlcWithCryptos.FromSqlInterpolated($"GetOHLCWithCryptoExceptCryptoId @CryptoId = {CryptoId}, @dateTime = {dateTime}").ToList();
            if (q != null)
            {
                oHLC = q.FirstOrDefault();
            }
            return oHLC;
        }


        //public OHLC? GetOHLCByStoreProcedure4(string CryptoIdd, string dateTime)
        //{
        //    var pCategoryIds = new SqlParameter()
        //    {
        //        ParameterName = "@CryptoId",
        //        Value = CryptoIdd,
        //        DbType = DbType.String
        //    };
        //    var pKeywords = new SqlParameter()
        //    {
        //        ParameterName = "@dateTime",
        //        DbType = DbType.String,
        //        Value = dateTime
        //    };

        //    OHLC oHLC = null;
        //    // معادل دستور include در Sql command چیست؟
        //    // چرا به دستور include ایراد میگیره خط زیر؟
        //    var q = context.oHLCs.FromSqlInterpolated($"GetOHLCBy_ID_Date @CryptoId = {CryptoIdd}, @dateTime = {dateTime}").ToList();
        //    if (q != null)
        //    {
        //        oHLC = q.FirstOrDefault();
        //        Console.Out.WriteLine(oHLC);
        //    }
        //    return oHLC;
        //}


        //branch
        //public OHLC? GetOHLCByStoreProcedure2(string CryptoIdd, string dateTime)
        //{
        //    OHLC oHLC = null;
        //    // معادل دستور include در Sql command چیست؟
        //    // چرا به دستور include ایراد میگیره خط زیر؟
        //    var q = context.oHLCs.FromSqlInterpolated($"GetOHLCWithCryptoExceptCryptoId @CryptoId = {CryptoIdd}, @dateTime = {dateTime}").ToList(); //چرا دستور First رو نمیشه همینجا زد؟  
        //    if (q != null)
        //    {
        //        oHLC = q.FirstOrDefault();
        //        return oHLC;
        //    }
        //    return oHLC;
        //}

        ////branch 2
        //public OhlcWithCryptoDTO? GetOHLCByStoreProcedure3(string CryptoIdd, string dateTime)
        //{
        //    OhlcWithCryptoDTO ohlcWithCrypto = null;
        //    // معادل دستور include در Sql command چیست؟
        //    // چرا به دستور include ایراد میگیره خط زیر؟


        //    // تفاوتی داره که اینجا بگیم از ohlc بگیر یا Crypto
        //    var q = context.Database.SqlQuery<OHLC>($"GetOHLCWithCryptoExceptCryptoId @CryptoId = {CryptoIdd}, @dateTime = {dateTime}").ToList(); //چرا دستور First رو نمیشه همینجا زد؟  

        //    if (q != null)
        //    {
        //        ohlcWithCrypto = q.Select(o => new OhlcWithCryptoDTO
        //        {
        //            // crypto = new CryptoDTO() { id = o.CryptoId, name=o.Crypto.name, symbol=o.Crypto.symbol },
        //            dateTime = o.dateTime,
        //            C = o.C,
        //            H = o.H,
        //            Id = o.Id,
        //            L = o.L,
        //            O = o.O,
        //            volume = o.volume
        //        }).FirstOrDefault();

        //        return ohlcWithCrypto;
        //    }
        //    return ohlcWithCrypto;
        //}

    }
}
