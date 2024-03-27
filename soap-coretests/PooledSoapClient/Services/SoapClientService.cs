using Microsoft.Extensions.Logging;
using SoapServices;
using static Program;

namespace SoapTestService.Services
{
    public class SoapClientService : ISoapClientService
    {
        public static string ServiceName = "SoapClientService";

        private ServiceClient _soapClient = null!;
        private readonly ILogger<Worker> _logger = null!;

        public SoapClientService(IHttpMessageHandlerFactory factory, ILogger<Worker> logger)
        {
            _logger = logger;
            _soapClient = new ServiceClient();
            _soapClient.Endpoint.EndpointBehaviors.Add(new HttpMessageHandlerBehavior(factory, ServiceName));

            _logger.LogWarning("Created a pooled soap client ...");
        }

        public async Task FetchMeaningOfLife()
        {
            //_logger.LogDebug("FetchMeaningOfLife called");
            var channel = _soapClient.ChannelFactory.CreateChannel();
            await channel.GetDataAsync(1).ConfigureAwait(false);
        }


    }
}
