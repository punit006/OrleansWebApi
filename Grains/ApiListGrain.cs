using Interfaces;
using Microsoft.Extensions.Logging;
using Orleans;

namespace Grains
{
    public class ApiListGrain : Grain, IApiList
    {        
        private readonly ILogger _logger;

        public ApiListGrain(ILogger<ApiListGrain> logger)
        {
            _logger = logger;
        }


        public Task<List<ApiListModel>> GetApiList()
        {
            _logger.LogInformation("Data Received '{ApiLIst}'", "In Grain");
            return Task.FromResult(new List<ApiListModel>() { new ApiListModel { ApiCode = 0, Version = 0.1f } });
        }
    }
}
