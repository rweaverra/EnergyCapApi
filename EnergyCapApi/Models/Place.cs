using System.Text.Json;
using System.Text.Json.Serialization;

namespace EnergyCapApi.Models
{
    public class Place
    {
        [JsonPropertyName("placeId")]
        public int PlaceId { get; set; }

        [JsonPropertyName("commodities")]
        public List<Commodity>? Commodities { get; set; }

    }
}
