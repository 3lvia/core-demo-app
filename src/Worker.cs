using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CoreDemoApp;

public class Worker(ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await DoWork(stoppingToken);
    }

    private Task DoWork(CancellationToken stoppingToken)
    {
        logger.LogInformation("CoreDemoApp.Worker is working.");

        return Task.CompletedTask;
    }
}