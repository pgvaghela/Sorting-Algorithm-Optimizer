using System.Diagnostics;
using System.Collections.Generic;
using SortingOptimizer.Models;
using System.Linq;

namespace SortingOptimizer.Services {
    public class SortingAnalyzer {
        private const int TimingIntervalMs = 10;
        private const int LargeArrayThreshold = 10000;

        public IEnumerable<SortResult> Analyze(int[] data) {
            var results = new List<SortResult>();
            var algos = new Dictionary<string, Func<int[], int[]>> {
                { "BubbleSort", SortingAlgorithms.BubbleSort },
                { "QuickSort", SortingAlgorithms.QuickSort },
                { "MergeSort", SortingAlgorithms.MergeSort },
                { "InsertionSort", SortingAlgorithms.InsertionSort },
                { "SelectionSort", SortingAlgorithms.SelectionSort },
                { "HeapSort", SortingAlgorithms.HeapSort }
            };

            string profile = AnalyzeDistribution(data);
            string recommended = RecommendAlgorithm(profile, data.Length);

            foreach (var kvp in algos) {
                var timingIntervals = new List<double>();
                int[] sorted = null;
                var sw = Stopwatch.StartNew();
                if (data.Length >= LargeArrayThreshold) {
                    // For large arrays, log timing at intervals
                    var arrCopy = (int[])data.Clone();
                    int n = arrCopy.Length;
                    int step = n / 100; // 100 steps for progress
                    int nextStep = step;
                    int progress = 0;
                    var lastTick = sw.Elapsed.TotalMilliseconds;
                    // Use a wrapper to time the sort in steps (only for BubbleSort, InsertionSort, SelectionSort)
                    if (kvp.Key == "BubbleSort") sorted = TimedBubbleSort(arrCopy, sw, timingIntervals);
                    else if (kvp.Key == "InsertionSort") sorted = TimedInsertionSort(arrCopy, sw, timingIntervals);
                    else if (kvp.Key == "SelectionSort") sorted = TimedSelectionSort(arrCopy, sw, timingIntervals);
                    else {
                        sorted = kvp.Value(arrCopy);
                        timingIntervals.Add(sw.Elapsed.TotalMilliseconds);
                    }
                } else {
                    sorted = kvp.Value(data);
                    timingIntervals.Add(sw.Elapsed.TotalMilliseconds);
                }
                sw.Stop();
                results.Add(new SortResult {
                    Algorithm = kvp.Key,
                    SortedData = sorted,
                    ElapsedMilliseconds = sw.Elapsed.TotalMilliseconds,
                    TimingIntervals = timingIntervals,
                    DistributionProfile = profile,
                    IsRecommended = (kvp.Key == recommended)
                });
            }
            return results;
        }

        // Analyze the distribution of the input array
        private string AnalyzeDistribution(int[] data) {
            if (data.Length < 2) return "trivial";
            bool sorted = true, reverse = true, duplicates = false;
            int dupCount = 0;
            for (int i = 1; i < data.Length; i++) {
                if (data[i] < data[i - 1]) sorted = false;
                if (data[i] > data[i - 1]) reverse = false;
                if (data[i] == data[i - 1]) dupCount++;
            }
            if (sorted) return "sorted";
            if (reverse) return "reverse";
            if (dupCount > data.Length / 10) return "many duplicates";
            // Check for nearly sorted
            int disorder = 0;
            for (int i = 1; i < data.Length; i++)
                if (data[i] < data[i - 1]) disorder++;
            if (disorder < data.Length / 20) return "nearly sorted";
            return "random";
        }

        // Recommend the best algorithm based on profile and size
        private string RecommendAlgorithm(string profile, int n) {
            if (profile == "sorted" || profile == "nearly sorted") return "InsertionSort";
            if (profile == "reverse" && n < 10000) return "MergeSort";
            if (profile == "many duplicates") return "HeapSort";
            if (n > 100000) return "HeapSort";
            return "QuickSort";
        }

        // Timed versions for interval logging
        private int[] TimedBubbleSort(int[] arr, Stopwatch sw, List<double> intervals) {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++) {
                for (int j = 0; j < n - i - 1; j++) {
                    if (arr[j] > arr[j + 1]) (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                }
                if (i % 10 == 0) intervals.Add(sw.Elapsed.TotalMilliseconds);
            }
            intervals.Add(sw.Elapsed.TotalMilliseconds);
            return arr;
        }
        private int[] TimedInsertionSort(int[] arr, Stopwatch sw, List<double> intervals) {
            int n = arr.Length;
            for (int i = 1; i < n; i++) {
                int key = arr[i];
                int j = i - 1;
                while (j >= 0 && arr[j] > key) {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = key;
                if (i % 100 == 0) intervals.Add(sw.Elapsed.TotalMilliseconds);
            }
            intervals.Add(sw.Elapsed.TotalMilliseconds);
            return arr;
        }
        private int[] TimedSelectionSort(int[] arr, Stopwatch sw, List<double> intervals) {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++) {
                int minIdx = i;
                for (int j = i + 1; j < n; j++) {
                    if (arr[j] < arr[minIdx]) minIdx = j;
                }
                (arr[i], arr[minIdx]) = (arr[minIdx], arr[i]);
                if (i % 10 == 0) intervals.Add(sw.Elapsed.TotalMilliseconds);
            }
            intervals.Add(sw.Elapsed.TotalMilliseconds);
            return arr;
        }

        public SortResult? GetBest(int[] data) {
            var all = Analyze(data);
            SortResult? best = null;
            foreach (var r in all) {
                if (best == null || (r.ElapsedMilliseconds < best.ElapsedMilliseconds))
                    best = r;
            }
            return best;
        }

        public SortAnalysisResponse AnalyzeWithImprovement(int[] data) {
            var results = Analyze(data).ToList();
            var baseline = results.FirstOrDefault(r => r.Algorithm == "BubbleSort");
            var recommended = results.FirstOrDefault(r => r.IsRecommended);
            double improvement = 0;
            if (baseline != null && recommended != null && baseline.ElapsedMilliseconds > 0)
                improvement = 100.0 * (baseline.ElapsedMilliseconds - recommended.ElapsedMilliseconds) / baseline.ElapsedMilliseconds;
            return new SortAnalysisResponse {
                Results = results,
                Baseline = baseline,
                Recommended = recommended,
                ImprovementPercent = improvement
            };
        }
    }
}