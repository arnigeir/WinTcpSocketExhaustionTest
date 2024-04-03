using NServiceBus.Logging;


namespace SNBEndpoint.Services
{
    public class SoapClientService : ISoapClientService
    {
        private static readonly ILog _logger = LogManager.GetLogger<TriggerLoadJob>();
        private readonly IService _soapClientService;


        public SoapClientService(IService service) { 
            _soapClientService = service;
        }    

        public async Task<string> FetchMeaningOfLife()
        {
           // _logger.Info($"FetchMeaningOfLife called");
            return await _soapClientService.GetDataAsync(1);           
        }


    }
}
