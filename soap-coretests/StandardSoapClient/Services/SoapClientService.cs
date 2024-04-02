using Microsoft.Extensions.Logging;
using SoapServices;
using static Program;

namespace SoapTestService.Services
{
    public class SoapClientService : ISoapClientService
    {
        private readonly ILogger<Worker> _logger = null!;
        private readonly IService _soapClientService;


        public SoapClientService(IService service,ILogger<Worker> logger) { 
            _logger = logger;
            _soapClientService = service;
        }    

        public async Task<string> FetchMeaningOfLife()
        {
            _logger.LogInformation($"FetchMeaningOfLife called");
            return await _soapClientService.GetDataAsync(1).ConfigureAwait(false);
           
        }


    }
}
