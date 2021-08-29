using Newtonsoft.Json;

namespace TestAnalyzerHelper.Models
{
    public class Time
    {
        [JsonProperty("start")]
        public long Start { get; set; }

        [JsonProperty("stop")]
        public long Stop { get; set; }

        [JsonProperty("duration")]
        public decimal Duration { get; set; }
    }
}