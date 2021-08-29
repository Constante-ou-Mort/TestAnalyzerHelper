using System.Collections.Generic;
using Newtonsoft.Json;

namespace TestAnalyzerHelper.Models
{
    public class TestStage
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("steps")]
        public List<Step> Steps { get; set; }

        [JsonProperty("attachments")]
        public List<Attachment> Attachments { get; set; }

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