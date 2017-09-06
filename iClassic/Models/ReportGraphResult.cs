using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iClassic.Models
{
    public class ReportGraphResult
    {
        public string Time { get; set; }
        public double Thu { get; set; }
        public double Chi { get; set; }

        [JsonProperty("Lợi nhuận")]
        public double LoiNhuan { get; set; }
    }
}