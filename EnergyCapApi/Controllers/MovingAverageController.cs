using Microsoft.AspNetCore.Mvc;

namespace EnergyCapApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovingAverageController : ControllerBase
    {
        [HttpGet("GetSinglePlaceMovingAverage")]
        public string GetSingle(string placeId, int subset)
        {
            //call the moving average class methods
            return $"This will be the JSON-String of the moving average results from place id{placeId} and a subset average of {subset}";
        }

        [HttpGet("GetMultiple PlaceMovingAverage")]
        public string GetMultiple(string placeIds, int subset)
        {
            //call the moving average class methods
            return $"This will be the JSON-String of the MULTIPLE average results from string of placeIds: {placeIds} and subset average of {subset}";
        }
    }
}
