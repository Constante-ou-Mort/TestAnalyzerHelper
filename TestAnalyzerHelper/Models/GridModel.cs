namespace TestAnalyzerHelper.Models
{
    public class GridModel
    {
        public string TestName { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
    }

    public static class CaseStatuses
    {
        public const string DidNotReproduce = "Did not reproduce";
        public const string Fixed = "Fixed";
        public const string TestBug = "Test bug";
        public const string ProductBug = "Product bug";
    }
}