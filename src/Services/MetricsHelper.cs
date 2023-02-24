using OpenTelemetry;
using OpenTelemetry.Metrics;
using System.Diagnostics.Metrics;

namespace InnspireWebAPI.Services
{
    public class MetricsHelper
    {


        public static void ActivateExporting(params string[] meters)
        {
            //string[] allMeters = new string[] { WebsiteMeter.Name };
            //if (meters != null && meters.Length > 0)
            //{
            //    allMeters = allMeters.Union(meters).ToArray();
            //}
            var meterProvider = Sdk.CreateMeterProviderBuilder()
                //.AddMeter(allMeters)
                .AddAspNetCoreInstrumentation()
                .AddEventCountersInstrumentation()
                .AddRuntimeInstrumentation()
                .AddPrometheusHttpListener(options => { 
                        options.UriPrefixes = new string[] { "http://*:9123/" };
                })
                .Build();
        }
    }
}
