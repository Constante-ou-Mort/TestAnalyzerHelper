using System.Collections.Generic;
using Newtonsoft.Json;

namespace TestAnalyzerHelper.Models
{
    public class BeforeStage
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("time")]
        public Time Time { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("steps")]
        public List<object> Steps { get; set; }

        [JsonProperty("attachments")]
        public List<object> Attachments { get; set; }

        [JsonProperty("parameters")]
        public List<object> Parameters { get; set; }

        [JsonProperty("stepsCount")]
        public decimal StepsCount { get; set; }

        [JsonProperty("hasContent")]
        public bool HasContent { get; set; }

        [JsonProperty("attachmentsCount")]
        public decimal AttachmentsCount { get; set; }

        [JsonProperty("shouldDisplayMessage")]
        public bool ShouldDisplayMessage { get; set; }
    }
}