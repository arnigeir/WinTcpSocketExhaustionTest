
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SoapTestService.Services;


public class Program
{
    public static void Main(string[] args)
    {
        IHostBuilder builder = Host.CreateDefaultBuilder(args);
        builder

            .ConfigureServices(services =>
            {
                services
                .AddScoped<ISoapClientService, SoapClientService>()
                .AddLogging()
                .AddHostedService<Worker>()
                .AddHttpClient("MyPrecious", client =>
                {
                    client.BaseAddress = new Uri("https://localhost:5001/Service.svc");
                    
                });
            });

         builder.Build().Run();

    }

    public  class Worker(ILogger<Worker> logger, ISoapClientService service) : BackgroundService
    {

        protected override  Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogDebug("Worker running..");
            while (!stoppingToken.IsCancellationRequested)
            { 
                service.FetchMeaningOfLife().ConfigureAwait(false);
            }
            return Task.CompletedTask;
        }
    }

}

