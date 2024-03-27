
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

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                service.FetchMeaningOfLife().ConfigureAwait(false);
                //logger.LogInformation($"Soap service returned {result} to Worker");
                //await Task.Delay(1_000, stoppingToken).ConfigureAwait(false);
            }
        }
    }

}

