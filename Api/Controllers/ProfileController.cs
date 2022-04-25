using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace Api.Controllers
{
    public class ProfileController
    {
        private readonly IGrainFactory _grainFactory;
        public ProfileController(IGrainFactory grainFactory)
        {
            _grainFactory = grainFactory;
        }


        [HttpGet]
        [Route("api/getprofile")]
        public async Task<ProfileModel> Get()
        {
            var entryGrain = await _grainFactory.GetGrain<IProfile>(new Guid()).GetProfile();
            return entryGrain;
        }
    }
}
