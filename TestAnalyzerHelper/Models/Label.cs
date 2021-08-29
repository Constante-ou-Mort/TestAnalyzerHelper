using Newtonsoft.Json;

namespace TestAnalyzerHelper.Models
{
    public class Label
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}