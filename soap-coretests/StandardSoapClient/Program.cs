
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SoapServices;
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
                .AddScoped<IService>(d => new ServiceClient(ServiceClient.EndpointConfiguration.BasicHttpBinding_IService, "https://localhost:5001/Service.svc"))
                .AddScoped<ISoapClientService, SoapClientService>()
                .AddHostedService<Worker>();

            });

        builder.Build().Run();

    }

    public class Worker(ILogger<Worker> logger, ISoapClientService service) : BackgroundService
    {

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Worker running..");
            while (!stoppingToken.IsCancellationRequested)
            {
                service.FetchMeaningOfLife().ConfigureAwait(false);
            }
            return Task.CompletedTask;
        }
    }

}

