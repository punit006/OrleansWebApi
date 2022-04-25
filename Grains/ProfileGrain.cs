using Interfaces;
using Microsoft.Extensions.Logging;
using Orleans;


namespace Grains
{
    public class ProfileGrain : Grain, IProfile
    {
        private readonly ILogger _logger;

        public ProfileGrain(ILogger<ApiListGrain> logger)
        {
            _logger = logger;
        }

        public Task<ProfileModel> GetProfile()
        {
            _logger.LogInformation("Data Received '{ApiLIst}'", "In Grain");
            return Task.FromResult(new ProfileModel() { Id = 0, Name = "Punit" } );
        }
    }
}
