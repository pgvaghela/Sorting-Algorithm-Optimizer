using Microsoft.AspNetCore.Mvc;
using SortingOptimizer.Models;
using SortingOptimizer.Services;
using System.Collections.Generic;

namespace SortingOptimizer.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class SortController : ControllerBase {
        private readonly SortingAnalyzer _analyzer;

        public SortController() {
            _analyzer = new SortingAnalyzer();
        }

        [HttpPost("analyze")]
        public SortAnalysisResponse Analyze([FromBody] SortRequest req) {
            return _analyzer.AnalyzeWithImprovement(req.Data ?? new int[0]);
        }

        [HttpPost("best")]    // returns only the best algorithm
        public ActionResult<SortResult> Best([FromBody] SortRequest req) {
            var result = _analyzer.GetBest(req.Data ?? new int[0]);
            if (result == null) {
                return NotFound("No sorting algorithms available");
            }
            return result;
        }
    }
}