using System.Collections.Generic;
using Newtonsoft.Json;

namespace TestAnalyzerHelper.Models
{
    public class Extra
    {
        [JsonProperty("severity")]
        public string Severity { get; set; }

        [JsonProperty("retries")]
        public List<object> Retries { get; set; }

        [JsonProperty("categories")]
        public List<object> Categories { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }
    }
}