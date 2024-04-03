
using NServiceBus;
using NServiceBus.Logging;

using ORServiceBus.Endpoint.Configuration.NetCore;

namespace SNBEndpoint
{
    public class Configuration : BusConfiguration
    {
        private readonly ILog log = LogManager.GetLogger<Configuration>();

        public Configuration(NServiceBusOptions options, ILog log) : base(options.ToNServiceBusConfig(), log)
        {
            this.log = log;
        }

        public override void InitializeRouting(RoutingSettings<RabbitMQTransport> routing)
        {
        }

        public override void InitializeExtraConfiguration(EndpointConfiguration endpointConfiguration)
        {
        }


    }
}
