using Grafana.OpenTelemetry;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace auth_service.Extensions;

public static class BuilderExtensions
{
    public static IHostApplicationBuilder ConfigureApp(this IHostApplicationBuilder builder)
    {
        builder.AddHealthCheck();
        builder.ConfigureOpenTelemetry();
        builder.Services.AddServiceDiscovery();
        

        return builder;
    }

    public static IHostApplicationBuilder AddHealthCheck(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks().AddCheck("self",()=>HealthCheckResult.Healthy(),["live"]);

        return builder;
    }

    public static IHostApplicationBuilder ConfigureOpenTelemetry(this IHostApplicationBuilder builder)
    {
        builder.Services.AddOpenTelemetry()
            .WithTracing(
            configure =>
            {
                configure.AddHttpClientInstrumentation().AddAspNetCoreInstrumentation().AddOtlpExporter(options =>
                {
                    options.Endpoint = new Uri("http://jaeger:4317");
                });;
                configure.UseGrafana().AddOtlpExporter();
            })
            .WithMetrics(configure =>
            {
                configure.UseGrafana().AddPrometheusExporter();
                
            });
        builder.Logging.AddOpenTelemetry(options =>
        {
            options.UseGrafana().AddConsoleExporter();
        });
        return builder;
    }
}