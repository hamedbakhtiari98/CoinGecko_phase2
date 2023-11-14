using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;

namespace CoinGecko_Phase2.API.Health
{
    public class HealthDbConnection : IHealthCheck
    {
        private readonly MyContext _context;

        public HealthDbConnection()
        {
                _context = new MyContext();
        }



        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
               await _context.students.AnyAsync(s => s.Name == "admin");
               return HealthCheckResult.Healthy();
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy(exception: ex);                
            }
        }
    }
}
