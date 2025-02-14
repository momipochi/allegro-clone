using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using auth_service.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;

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
app.MapGet("/jwt", () =>
{   
    string privateKeyPath = "Keys/private_key.pem";
    var privateKey = File.ReadAllText(privateKeyPath).ToCharArray();
    app.Logger.LogInformation($"The privatekey text: {privateKey}");

    using RSA rsa = RSA.Create();
    rsa.ImportFromPem(privateKey);
    var credentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256)
    {
        CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity([
            new Claim(JwtRegisteredClaimNames.Sub, "user123"),
            new Claim(JwtRegisteredClaimNames.Name, "Alice"),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        ]),
        Expires = DateTime.UtcNow.AddHours(1),
        SigningCredentials = credentials,
        Issuer = "auth-service",
        Audience = "auth-service",
    };
    
    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
});
app.MapGet("/hello", () => "PlayingWithSchemes").AllowAnonymous();
app.Run();

