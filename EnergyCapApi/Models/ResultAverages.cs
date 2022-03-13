using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyCapApi.Models
{
    public class ResultAverages
    {
        public List<double>? TotalCostSimpleAverage { get; set; }
        public List<double>? GlobalUseSimpleAverage { get; set; }
        public List<double>? GlobalUnitCostSimpleAverage { get; set; }
    }
}
