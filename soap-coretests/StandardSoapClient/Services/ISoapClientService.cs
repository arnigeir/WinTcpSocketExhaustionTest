﻿namespace SoapTestService.Services
{
    public interface ISoapClientService
    {
        public Task<string> FetchMeaningOfLife();
    }
}
