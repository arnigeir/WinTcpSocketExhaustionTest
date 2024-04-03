
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NServiceBus;
using NServiceBus.Logging;
using Quartz;
using Serilog;
using SNBEndpoint.Services;

namespace SNBEndpoint
{
    class Program
    {
        public static Task Main(string[] args)
        {
            return CreateHostBuilder(args).Build().RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            return Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .UseSerilog((context, services, logConfiguration) => logConfiguration.ReadFrom.Configuration(context.Configuration))
                .UseNServiceBus(context =>
                {
                    var nServiceBusOptions = new NServiceBusOptions();
                    context.Configuration.GetSection("NServiceBusOptions").Bind(nServiceBusOptions);

                    //nServiceBusOptions.BusDBConnectionString = Environment.GetEnvironmentVariable("amis_sql_connection_string", EnvironmentVariableTarget.Machine);
                    //nServiceBusOptions.TransportConnectionString = Environment.GetEnvironmentVariable("ami_solution_rabbitmq_connection_string", EnvironmentVariableTarget.Machine);

                    //nServiceBusOptions.BusDBConnectionString = "Data Source=mssql-nbus-t-1;Initial Catalog=AMI-Solution-BUS;Integrated Security=True";
                    //nServiceBusOptions.TransportConnectionString = "host=bus-rmq-test.or.is;port=5672;username=svc-rmq.amis;password=FaraiBus2049";


                    nServiceBusOptions.BusDBConnectionString = "Data Source=ami-bus-d-5.or.is;Initial Catalog=AMIDEV;Integrated Security=True;Encrypt=false";
                    nServiceBusOptions.TransportConnectionString = "host=ami-bus-d-5;username=amidev;password=amidev";

                    var endpointConfiguration = new Configuration(nServiceBusOptions, LogManager.GetLogger<Configuration>());

                    return endpointConfiguration.ConfigureEndpoint();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    InitializeDependencies(hostContext, services);

                });

        }

        private static void InitializeDependencies(HostBuilderContext context, IServiceCollection services)
        {
            services
                .Configure<NServiceBusOptions>(context.Configuration.GetSection("NServiceBusOptions"))
                .AddQuartz(q =>
                    {
                        q.SchedulerId = "PooledEndpointQuartzTrigger";
                        q.UseMicrosoftDependencyInjectionJobFactory();
                        q.ScheduleJob<TriggerLoadJob>(trigger =>
                            trigger
                                .WithIdentity("Sync CT meter trigger")
                                .WithCronSchedule(context.Configuration["Quartz:Cron:Trigger"]));
                    })
                .AddQuartzHostedService(options =>
                {
                    options.WaitForJobsToComplete = true;
                })                
                //.AddScoped<IService>(d => new ServiceClient(ServiceClient.EndpointConfiguration.BasicHttpBinding_IService, "http://ami-bus-d-5:5000/Service.svc"))
                .AddSingleton<ISoapClientService, SoapClientService>()
                .AddHttpClient("MyPrecious", client =>
                {
                    client.BaseAddress = new Uri("http://localhost:5000/Service.svc");

                });

        }
    }
}
