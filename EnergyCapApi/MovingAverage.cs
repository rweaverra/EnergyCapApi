using EnergyCapApi.Models;
using System.Text.Json;

namespace EnergyCapApi
{
    public class MovingAverage
    {

        static private readonly HttpClient client = new HttpClient();
        //function to calculate simple moving average

      
        public string GetSingleMovingAverage(string placeId, int subSet)
        {
            string result = "that Place does not exists";
            //check data base if placeId exists
              //Place place DataCache.GetPlace(placeId)
            //if no, call the API to get the PlaceInfo,
            //if(place == null)
              //place = CallEnergyCapApi(placeId)
              //DataCache.AddPlace(Place place)
            //if yes call dataCache to grab the data then perform MovingAverage function
              //SimpleMovingAverage.Update
         
            

            return result;

        }

    
        public static async Task<Place> CallEnergyCapApi(string placeId)
        {
            Place placeData = null;
            client.DefaultRequestHeaders.Add("ECI-APIKEY", "ZWNhcHxJbXBsZW1lbnRhdGlvbg==.ZGV2ZWxfZGVtb3wxMDI0fDIwNDk=.MGQ5MmNjYjEtZDk0MC00NzMwLWFhMjEtZWVhYTViNjEzNGRj");
            var streamTask = client.GetStreamAsync($"https://implement.energycap.com/api/v3/place/{placeId}/digest/calendarized/monthly");

            if (streamTask.Result != null)
            {
             placeData = await JsonSerializer.DeserializeAsync<Place>(await streamTask);
            }


            return placeData;
        }
       

        //function to grab database for multiple moving averages
        public string GetMultipleMovingAverages(string placeIds, int subset)
        {
            return "string";
        }
    }
}
