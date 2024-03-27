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
        private int _state = 1;

        public SoapClientService(IHttpMessageHandlerFactory factory, ILogger<Worker> logger)
        {
            _logger = logger;
            _soapClient = new ServiceClient();
            _soapClient.Endpoint.EndpointBehaviors.Add(new HttpMessageHandlerBehavior(factory, ServiceName));
        }

        public async Task FetchMeaningOfLife()
        {
            int request = (int)DateTime.UtcNow.Ticks;
            //_logger.LogInformation($"SoapClientService.FetchMeaningOfLife state: {_state}");
            var channel = _soapClient.ChannelFactory.CreateChannel();
            var result = await channel.GetDataAsync(request).ConfigureAwait(false);
            //_logger.LogInformation($"SoapClientService.FetchMeaningOfLife returned '{result}'");
            _state++;

        }


    }
}
