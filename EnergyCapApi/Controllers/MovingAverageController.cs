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
            var result = MovingAverage.CallEnergyCapApi(placeId);
            return System.Text.Json.JsonSerializer.Serialize(result);
        }

        [HttpGet("GetMultiplePlacesMovingAverage")]
        public string GetMultiple(string placeIds, int subset)
        {
            //call the moving average class methods
            return $"This will be the JSON-String of the MULTIPLE average results from string of placeIds: {placeIds} and subset average of {subset}";
        }
    }
}
