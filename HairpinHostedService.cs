using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class HairpinHostedService : IHostedService, IDisposable
{
    private readonly ILogger<HairpinHostedService> _logger;
    private Timer _timer;
    private readonly HttpClient _httpClient;

    public HairpinHostedService(ILogger<HairpinHostedService> logger)
    {
        _logger = logger;
        _httpClient = new HttpClient();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Hairpin Hosted Service is starting.");
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        return Task.CompletedTask;
    }

    private async void DoWork(object state)
    {
        try
        {
            var envApiIpAddress = Environment.GetEnvironmentVariable("API_IP_ADDRESS");
            if (string.IsNullOrEmpty(envApiIpAddress))
            {
                _logger.LogWarning("API_IP_ADDRESS environment variable is not set.");
                return;
            }
            var response = await _httpClient.GetStringAsync($"http://{envApiIpAddress}/machinename");
            _logger.LogInformation($"Response from REST service: {response}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while calling the REST service.");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Hairpin Hosted Service is stopping.");
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
        _httpClient?.Dispose();
    }
}
