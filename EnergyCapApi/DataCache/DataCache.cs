using EnergyCapApi.Models;
using System.Reflection;
using System.Text.Json;

namespace EnergyCapApi.DataCache
{
    public static class DataCache
    {
        static readonly string _jsonFile;
        static DataCache()
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var iconPath = Path.Combine(outPutDirectory, "DataCache\\dataCache.json");
            string json_path = new Uri(iconPath).LocalPath;

            _jsonFile = json_path;

        }

        public async static Task<Place> GetPlaceData(string placeId)
        {
            Console.WriteLine("inside GetPLace Data");
            string json = File.ReadAllText(_jsonFile);
            var cachedData = JsonSerializer.Deserialize<Dictionary<string, Place>>(json);
            Place placeData;

            if (cachedData.ContainsKey(placeId))
            {
                placeData = cachedData[placeId];
            }
            else
            {
                //grab it from API

                placeData = MovingAverage.CallEnergyCapApi(placeId).Result;
                AddPlaceResultsToCachedData(placeData);

            }


            return placeData;

        }

        public static void AddPlaceResultsToCachedData(Place place)
        {
            string json = File.ReadAllText(_jsonFile);
            var dataCache = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
            Console.WriteLine("dataCache: " + dataCache.ToString());


            //if place is already in database- do nothing
            string placeId = place.PlaceId.ToString();

            if (dataCache.ContainsKey(placeId)) return;


            Place placeToAdd = place;
            dataCache.Add(place.PlaceId.ToString(), placeToAdd);

            var options = new JsonSerializerOptions { WriteIndented = true };
            string updatedDataCache = JsonSerializer.Serialize(dataCache, options);
            File.WriteAllText(_jsonFile, updatedDataCache);
        }
    }
}
