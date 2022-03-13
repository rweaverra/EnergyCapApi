using System.Text.Json.Serialization;

namespace EnergyCapApi.Models
{
    public class MonthlyResults
    {
        [JsonPropertyName("totalCost")]
        public double TotalCost { get; set; }

        [JsonPropertyName("globalUse")]
        public double GlobalUse { get; set; }

        [JsonPropertyName("globalUseUnitCost")]
        public double GlobalUseUnitCost { get; set; }

    }


}
