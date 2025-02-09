using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace auth_service.Extensions;

public static class BuilderExtensions
{
    public static IHostApplicationBuilder ConfigureApp(this IHostApplicationBuilder builder)
    {

        
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
        builder.Logging.AddOpenTelemetry
        
        return builder;
    }
}