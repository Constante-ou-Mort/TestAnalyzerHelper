using Newtonsoft.Json;
using System.Collections.Generic;

namespace TestAnalyzerHelper.Models
{
    public class TestCaseModel
    {
        [JsonProperty("uid")]
        public string Uid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("historyId")]
        public string HistoryId { get; set; }

        [JsonProperty("time")]
        public Time Time { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("flaky")]
        public bool Flaky { get; set; }

        [JsonProperty("newFailed")]
        public bool NewFailed { get; set; }

        [JsonProperty("beforeStages")]
        public List<BeforeStage> BeforeStages { get; set; }

        [JsonProperty("testStage")]
        public TestStage TestStage { get; set; }

        [JsonProperty("afterStages")]
        public List<AfterStage> AfterStages { get; set; }

        [JsonProperty("labels")]
        public List<Label> Labels { get; set; }

        [JsonProperty("parameters")]
        public List<object> Parameters { get; set; }

        [JsonProperty("links")]
        public List<object> Links { get; set; }

        [JsonProperty("hidden")]
        public bool Hidden { get; set; }

        [JsonProperty("retry")]
        public bool Retry { get; set; }

        [JsonProperty("extra")]
        public Extra Extra { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("parameterValues")]
        public List<object> ParameterValues { get; set; }
    }
}