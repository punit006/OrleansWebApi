using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace Api.Controllers
{
    [ApiController]
    public class ApiListController
    {
        private readonly IGrainFactory _grainFactory;
        public ApiListController(IGrainFactory grainFactory)
        {
            _grainFactory = grainFactory;
        }


        [HttpGet]
        [Route("api/getapilist")]
        public async Task<List<ApiListModel>> Get()
        {
            var entryGrain = await _grainFactory.GetGrain<IApiList>(new Guid()).GetApiList();          
            return entryGrain;
        }

        
    }
}
