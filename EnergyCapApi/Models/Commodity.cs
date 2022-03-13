using System.Text.Json.Serialization;

namespace EnergyCapApi.Models
{
    public class Commodity
    {
        [JsonPropertyName("commodityCode")]
        public string? CommodityCode { get; set; }

        [JsonPropertyName("commonUseUnit")]
        public Dictionary<string, object>? CommonUseUnit { get; set; }

        [JsonPropertyName("costUnit")]
        public Dictionary<string, object>? CostUnit { get; set; }


        [JsonPropertyName("results")]
        public MonthlyResults[]? Results { get; set; }






    }
}
