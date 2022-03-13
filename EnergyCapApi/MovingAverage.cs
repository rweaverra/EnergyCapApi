using EnergyCapApi.Models;
using System.Text.Json;

namespace EnergyCapApi
{
    public class MovingAverage
    {

        static private readonly HttpClient client = new HttpClient();
        //function to calculate simple moving average

        private readonly int _k;
        private readonly int[] _values;

        private int _index = 0;
        private int _sum = 0;

        public MovingAverage(int k)
        {
            if (k <= 0) throw new ArgumentOutOfRangeException(nameof(k), "Must be greater than 0");

            _k = k;
            _values = new int[k];

        }

        public double Update(int nextInput)
        {
            // calculate the new sum
            _sum = _sum - _values[_index] + nextInput;

            // overwrite the old value with the new one
            _values[_index] = nextInput;

            // increment the index (wrapping back to 0)
            _index = (_index + 1) % _k;

            // calculate the average
            return ((double)_sum) / _k;
        }

        public static string MultiplePlaceMovingAverage(string placeids, int window)
        {
            string[] placeIds = placeids.Split(',');
            var commoditiesWithMovingAverage = new List<Place>();

            foreach (string placeId in placeIds)
            {

                string jsonString = GetMovingAverage(placeId, window);

                Place? currentPlace = JsonSerializer.Deserialize<Place>(jsonString);

                commoditiesWithMovingAverage.Add(currentPlace);

            }
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string jsonData = JsonSerializer.Serialize(commoditiesWithMovingAverage, options);

            return jsonData;
        }


        public static string GetMovingAverage(string placeId, int window)
        {
            if (window > 12) throw new ArgumentOutOfRangeException(nameof(window), "Window Must be less than 12");

            Task<Place> placeData = DataCache.DataCache.GetPlaceData(placeId);


            Place returnData = new() { PlaceId = int.Parse(placeId) };
            returnData.Commodities = new List<Commodity>();
            var placedataCommodities = placeData.Result.Commodities;

            foreach (Commodity commodity in placedataCommodities)
            {
                var returnCommodity = new Commodity();
                returnCommodity.CommodityCode = commodity.CommodityCode;
                returnCommodity.CommonUseUnit = commodity.CommonUseUnit;
                returnCommodity.CostUnit = commodity.CostUnit;


                List<MonthlyResults>? results = commodity.Results;

                var totalCost = new List<float>();
                var globalUse = new List<float>();
                var globalUseUnitCost = new List<float>();


                var resultAverages = new ResultAverages();
                resultAverages.TotalCostSimpleAverage = new List<double>();
                resultAverages.GlobalUseSimpleAverage = new List<double>();
                resultAverages.GlobalUnitCostSimpleAverage = new List<double>();


                foreach (MonthlyResults monthlyCommodity in results)
                {
                    totalCost.Add((float)monthlyCommodity.TotalCost);
                    globalUse.Add((float)monthlyCommodity.GlobalUse);
                    globalUseUnitCost.Add((float)monthlyCommodity.GlobalUseUnitCost);
                }

                //run the simple moving average on each totalCost, globalUse, globalseUnitCost
                var calculator1 = new MovingAverage(window);
                var calculator2 = new MovingAverage(window);
                var calculator3 = new MovingAverage(window);

                foreach (var cost in totalCost)
                {
                    var sma = calculator1.Update((int)cost);
                    resultAverages.TotalCostSimpleAverage.Add(sma);
                }

                foreach (var amount in globalUse)
                {
                    var sma = calculator2.Update((int)amount);
                    resultAverages.GlobalUseSimpleAverage.Add(sma);

                }

                foreach (var unitCost in globalUseUnitCost)
                {
                    var sma = calculator3.Update((int)unitCost);
                    resultAverages.GlobalUnitCostSimpleAverage.Add(sma);
                }
                //add the lists to returnCommodity.results
                returnCommodity.ResultAverages = resultAverages;
                returnData.Commodities.Add(returnCommodity);

            }



            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonData = JsonSerializer.Serialize(returnData, options);
       
            return jsonData;

        }


        public static async Task<Place> CallEnergyCapApi(string placeId)
        {
            Place? placeData = null;
            client.DefaultRequestHeaders.Add("ECI-APIKEY", "ZWNhcHxJbXBsZW1lbnRhdGlvbg==.ZGV2ZWxfZGVtb3wxMDI0fDIwNDk=.MGQ5MmNjYjEtZDk0MC00NzMwLWFhMjEtZWVhYTViNjEzNGRj");
            var streamTask = client.GetStreamAsync($"https://implement.energycap.com/api/v3/place/{placeId}/digest/calendarized/monthly");

            if (streamTask.Result != null)
            {
             placeData = await JsonSerializer.DeserializeAsync<Place>(await streamTask);
            }


            return placeData;
        }
       

    }
}
