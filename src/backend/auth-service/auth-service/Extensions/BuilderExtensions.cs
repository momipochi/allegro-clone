using System.Diagnostics;
using System.Diagnostics.Metrics;
using Grafana.OpenTelemetry;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;


namespace auth_service.Extensions;

public static class BuilderExtensions
{
    public static IHostApplicationBuilder ConfigureApp(this IHostApplicationBuilder builder)
    {
        builder.AddHealthCheck();
        // builder.ConfigureOpenTelemetry();
        builder.Services.AddServiceDiscovery();
        builder.AddAuth();
        builder.Services.AddAuthorization();

        return builder;
    }

    public static IHostApplicationBuilder AddAuth(this IHostApplicationBuilder builder)
    {
        builder.Services.AddAuthentication().AddJwtBearer("PlayingWithSchemes", options =>
        {
            options.Authority = "http://localhost:8080"; // Your auth-service
            options.Audience = "dummy_audience";
            options.RequireHttpsMetadata = false; // Set to true in production
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = "http://localhost:8080",
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true
            };

            // Automatically fetch JWKS public keys
            options.ConfigurationManager = new Microsoft.IdentityModel.Protocols.ConfigurationManager<Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectConfiguration>(
                "http://localhost:8080/.well-known/jwks.json",
                new Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectConfigurationRetriever()
            );
        });
        return builder;
    }
    public static IHostApplicationBuilder AddHealthCheck(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks().AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);

        return builder;
    }

    public static IHostApplicationBuilder ConfigureOpenTelemetry(this IHostApplicationBuilder builder)
    {
        var tracingOtlpEndpoint = "http://jaeger:4317";
        // Custom metrics for the application
        builder.Services.AddOpenTelemetry()
            .WithTracing(
                tracing =>
                {

                    tracing.AddHttpClientInstrumentation().AddAspNetCoreInstrumentation().AddOtlpExporter(options =>
                    {
                        options.Endpoint = new Uri(tracingOtlpEndpoint);
                    });
                    // tracing.UseGrafana().AddOtlpExporter();
                })
            .WithMetrics(metrics =>
                {
                    metrics.AddPrometheusExporter();
                    metrics.AddMeter("Microsoft.AspNetCore.Hosting", "Microsoft.AspNetCore.Server.Kestrel");
                }

            );
        builder.Logging.AddOpenTelemetry(options =>
        {
            options.UseGrafana().AddConsoleExporter();
        });
        return builder;
    }
}