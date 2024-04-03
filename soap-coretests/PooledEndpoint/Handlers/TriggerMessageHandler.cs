using NServiceBus;
using NServiceBus.Logging;
using SNBEndpoint.Services;
using SNBEndpoint.Messages;


namespace SNBEndpoint.Handlers
{
    public class TriggerMessageHandler : IHandleMessages<TriggerMessage>
    {
        private static readonly ILog _logger = LogManager.GetLogger<TriggerLoadJob>();
        private readonly ISoapClientService _soapClientService;

        public TriggerMessageHandler(ISoapClientService soapClientService) 
        { 
        
            _soapClientService = soapClientService;
        }



        public async Task Handle(TriggerMessage message, IMessageHandlerContext context)
        {
            //_logger.Info("Call SOAP service ...");

            await _soapClientService.FetchMeaningOfLife().ConfigureAwait(false);

           
        }
    }
}
