namespace auth_service.Extensions;

public static class AppExtensions
{
    public static WebApplication ConfigureApp(this WebApplication app)
    {
        // app.MapPrometheusScrapingEndpoint();
        // app.UseOpenTelemetryPrometheusScrapingEndpoint();
        return app;
    }
}