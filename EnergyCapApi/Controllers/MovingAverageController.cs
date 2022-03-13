using Microsoft.AspNetCore.Mvc;
using EnergyCapApi.DataCache;
using EnergiCapApi.Attributes;

namespace EnergyCapApi.Controllers
{
    [ApiKey]
    [ApiController]
    [Route("[controller]")]
    public class MovingAverageController : ControllerBase
    {
        [HttpGet("GetSinglePlaceMovingAverage")]
        public string GetSingle(string placeId, int subset = 4)
        {
            return MovingAverage.GetMovingAverage(placeId, subset);
        }

        [HttpGet("GetMultiplePlacesMovingAverage")]
        public string GetMultiple(string placeIds, int subset = 4)
        {      
            return MovingAverage.MultiplePlaceMovingAverage(placeIds, subset);
        }
    }
}
