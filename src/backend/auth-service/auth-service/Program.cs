using auth_service.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureApp();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseOpenTelemetryPrometheusScrapingEndpoint();

app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "wwwroot/.well-known")),
    RequestPath = "/.well-known"  // Ensures requests to /.well-known/ are served correctly
});
app.MapGet("/", () => "Hello World!").AllowAnonymous();
app.MapGet("/hello",[Authorize(AuthenticationSchemes = "PlayingWithSchemes")] () => "PlayingWithSchemes");
app.Run();

