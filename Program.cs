using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<HairpinHostedService>();

// Configure Kestrel to use the certificate from the certs directory.
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(443, listenOptions =>
    {
        listenOptions.UseHttps("./certs/tls.pfx", "123456"); // Path to your certificate and key
    });
});

var app = builder.Build();

app.MapGet("/machinename", () => Environment.MachineName);

app.Run();
