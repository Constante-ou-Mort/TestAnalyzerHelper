using Newtonsoft.Json;

namespace TestAnalyzerHelper.Models
{
    public class Attachment
    {
        [JsonProperty("uid")]
        public string Uid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("size")]
        public decimal Size { get; set; }
    }
}