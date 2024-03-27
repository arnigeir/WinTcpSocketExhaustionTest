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

        public async Task FetchMeaningOfLife()
        {
            var result = await _soapClientService.GetDataAsync(1);
           // _logger.LogInformation($"FetchMeaningOfLife returned '{result}'");
        }


    }
}
