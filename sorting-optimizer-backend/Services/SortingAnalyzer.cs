using System.Diagnostics;
using System.Collections.Generic;
using SortingOptimizer.Models;

namespace SortingOptimizer.Services {
    public class SortingAnalyzer {
        public IEnumerable<SortResult> Analyze(int[] data) {
            var results = new List<SortResult>();
            var algos = new Dictionary<string, Func<int[], int[]>> {
                { "BubbleSort", SortingAlgorithms.BubbleSort },
                { "QuickSort", SortingAlgorithms.QuickSort },
                { "MergeSort", SortingAlgorithms.MergeSort }
            };

            foreach (var kvp in algos) {
                var sw = Stopwatch.StartNew();
                var sorted = kvp.Value(data);
                sw.Stop();
                results.Add(new SortResult {
                    Algorithm = kvp.Key,
                    SortedData = sorted,
                    ElapsedMilliseconds = sw.Elapsed.TotalMilliseconds
                });
            }
            return results;
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
    }
}