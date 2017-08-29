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
        public float Thu { get; set; }
        public float Chi { get; set; }

        [JsonProperty("Lợi nhuận")]
        public float LoiNhuan { get; set; }
    }
}