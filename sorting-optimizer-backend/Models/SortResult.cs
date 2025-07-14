namespace SortingOptimizer.Models {
    public class SortResult {
        public string? Algorithm { get; set; }
        public int[]? SortedData { get; set; }
        public double ElapsedMilliseconds { get; set; }
        public List<double>? TimingIntervals { get; set; } // ms at 10ms intervals
        public string? DistributionProfile { get; set; } // e.g., random, sorted, reverse, duplicates
        public bool IsRecommended { get; set; } // true if this is the recommended algorithm
    }

    public class SortAnalysisResponse {
        public List<SortResult> Results { get; set; } = new();
        public SortResult? Baseline { get; set; }
        public SortResult? Recommended { get; set; }
        public double ImprovementPercent { get; set; }
    }
}