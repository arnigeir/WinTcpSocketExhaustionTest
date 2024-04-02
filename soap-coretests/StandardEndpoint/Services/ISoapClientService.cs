namespace SNBEndpoint.Services
{
    public interface ISoapClientService
    {
        public Task<string> FetchMeaningOfLife();
    }
}
