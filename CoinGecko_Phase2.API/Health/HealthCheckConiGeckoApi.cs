using Microsoft.Extensions.Diagnostics.HealthChecks;
using RestSharp;

namespace CoinGecko_Phase2.API.Health
{
    public class HealthCheckConiGeckoApi : IHealthCheck
    {
        RestClient client = new RestClient("https://api.coingecko.com");
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var request = new RestRequest("api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=1&page=1&sparkline=false", Method.Get);
                var q = client.Get(request);
                return HealthCheckResult.Healthy();
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy();
            }


            throw new NotImplementedException();
        }
    }
}
