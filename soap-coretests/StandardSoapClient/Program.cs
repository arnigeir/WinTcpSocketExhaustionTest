
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
            //int n = 100000;
            //logger.Log(LogLevel.Information, "Start {n} threads",n);
            //var tasks = new List<Task>();
            //for(int i = 0; i < 100000; i++)
            //{
            //    if (stoppingToken.IsCancellationRequested) break;
            //    tasks.Add(service.FetchMeaningOfLife());

            //}
            //await Task.WhenAll(tasks);
            //logger.LogInformation("Done running {n} threads",n);
            while(!stoppingToken.IsCancellationRequested)
            {
                //logger.LogInformation("Do request ..");
                service.FetchMeaningOfLife().ConfigureAwait(false);
            }
            return Task.CompletedTask;
        }
    }

}

