using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<HairpinHostedService>();

// Configure Kestrel to use the certificate from the certs directory.
builder.WebHost.ConfigureKestrel(options =>
{
    options.ConfigureHttpsDefaults(httpsOptions =>
    {
        httpsOptions.ServerCertificate = new X509Certificate2("./certs/tls.pfx", "123456");
    });
});

var app = builder.Build();

app.MapGet("/machinename", () => Environment.MachineName);

app.Run();
