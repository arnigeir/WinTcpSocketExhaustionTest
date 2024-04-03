using NServiceBus.Logging;
using SNBEndpoint.Behaviours;


namespace SNBEndpoint.Services
{
    public class SoapClientService : ISoapClientService
    {
        private static readonly ILog _logger = LogManager.GetLogger<TriggerLoadJob>();
        private ServiceClient _soapClient = null!;

        public static string ServiceName = "SoapClientService";

        public SoapClientService(IHttpMessageHandlerFactory factory)
        {
            _soapClient = new ServiceClient();
            _soapClient.Endpoint.EndpointBehaviors.Add(new HttpMessageHandlerBehavior(factory, ServiceName));
            _logger.Info("Created a pooled soap client ...");
        }

        public async Task<string> FetchMeaningOfLife()
        {
            var channel = _soapClient.ChannelFactory.CreateChannel();
            return await channel.GetDataAsync(1).ConfigureAwait(false);
        }


    }
}
