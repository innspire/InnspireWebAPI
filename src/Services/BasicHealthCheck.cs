using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net;
using System.Text;

namespace InnspireWebAPI.Services
{
    public class BasicHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {

            return Task.FromResult(HealthCheckResult.Healthy("Everything is just fine"));
        }
    }

    public class HealthReportServer
    {
        private readonly ILogger<HealthReportServer>? logger;
        private readonly HealthCheckService checkService;

        public void Start()
        {
            string healthAddress = "http://*:8081/";
            var healthListener = new HttpListener();
            try
            {
                healthListener.Prefixes.Add(healthAddress);
                healthListener.Start();
            }
            catch (Exception ex)
            {

                logger?.LogError(ex, "Unable to start Health Server");
                return;
            }
            

            Task.Run(async() =>
            {
                while (true)
                {
                    try
                    {
                        var context = healthListener.GetContext();

                        var checkResult = await checkService.CheckHealthAsync();
                        byte[] result;
                        if (checkResult.Status == HealthStatus.Healthy)
                        {
                            context.Response.StatusCode = StatusCodes.Status200OK;
                            result = Encoding.UTF8.GetBytes("healthy");
                        }
                        else
                        {
                            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                            result = Encoding.UTF8.GetBytes("aua");
                        }
                        
                        
                        context.Response.OutputStream.Write(result, 0, result.Length);
                        context.Response.OutputStream.Dispose();
                    }
                    catch (Exception ex)
                    {
                        logger?.LogError(ex, "Unable to make health check");

                    }

                }
            });
        }

        public HealthReportServer(ILogger<HealthReportServer>? logger, HealthCheckService checkService)
        {
            this.logger = logger;
            this.checkService = checkService;
        }
    }

    public static class HealthExtensions
    {

        public static HealthReportServer UseHealthServer(this WebApplication webApp)
        {
            var logger = webApp.Services.GetService<ILogger<HealthReportServer>>();
            var healthCheckService = webApp.Services.GetService<HealthCheckService>();
            var server = new HealthReportServer(logger, healthCheckService);
            server.Start();
            return server;
        }
    }
}
