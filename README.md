# Sorting Optimizer

A full-stack application for analyzing and comparing sorting algorithms on large datasets. The application consists of an Angular frontend and an ASP.NET Core backend.

## Features

- **Multiple Sorting Algorithms**: Bubble Sort, Quick Sort, Merge Sort, Insertion Sort, Heap Sort, Selection Sort, and more
- **Benchmarking up to 1 Million Records**: Handles very large arrays efficiently
- **Performance Analysis**: Measures execution time for each algorithm, with timing logged at 10ms intervals for large datasets
- **Input Distribution Analysis**: Detects if the input is sorted, reverse, random, nearly sorted, or contains duplicates
- **Dynamic Algorithm Recommendation**: Suggests the optimal algorithm based on input characteristics
- **Baseline vs. Dynamic Comparison**: Compares a baseline (e.g., BubbleSort) to the dynamically selected best algorithm, showing improvement percentage
- **Modern UI**: Clean, responsive interface built with Angular
- **Drag-and-Drop File Upload**: Upload CSV or JSON files for large datasets
- **Multiple Visualizations**: Bar, line, scatter, and table views for performance comparison (powered by Chart.js)
- **Real-time Results**: Compare algorithm performance instantly

## Architecture

- **Frontend**: Angular 19 with standalone components
- **Backend**: ASP.NET Core 9 with REST API
- **Communication**: HTTP/HTTPS between frontend and backend

## Getting Started

### Prerequisites

- .NET 9 SDK
- Node.js 18+ and npm
- Angular CLI

### Running the Application

1. **Start the Backend**:
   ```bash
   cd sorting-optimizer-backend
   dotnet run
   ```
   The backend will be available at `https://localhost:7149` (or as shown in your terminal)

2. **Start the Frontend**:
   ```bash
   cd sorting-optimizer-frontend
   npm start
   ```
   The frontend will be available at `http://localhost:4200`

3. **Open your browser** and navigate to `http://localhost:4200`

## Usage

1. **Manual Input**: Enter a comma-separated list of numbers in the input field
2. **File Upload**: Drag and drop a CSV or JSON file containing your array
3. Click "Analyze" to run all sorting algorithms
4. View the results showing:
   - Algorithm name
   - Sorted data
   - Execution time in milliseconds (with timing intervals for large arrays)
   - Input distribution type
   - Recommended algorithm
   - Baseline vs. dynamic improvement percentage
   - Visualizations: bar, line, scatter, table

## API Endpoints

- `POST /api/sort/analyze` - Returns results from all algorithms, input analysis, recommendations, and timing intervals
- `POST /api/sort/best` - Returns only the fastest algorithm (deprecated in favor of `/analyze`)

### Example Request

```json
{
  "array": [5, 3, 8, 1, 2]
}
```

### Example Response

```json
{
  "results": [
    {
      "algorithm": "BubbleSort",
      "sortedData": [1, 2, 3, 5, 8],
      "timings": [0, 1, 2, 3],
      "totalMilliseconds": 3
    },
    {
      "algorithm": "QuickSort",
      "sortedData": [1, 2, 3, 5, 8],
      "timings": [0, 0, 1],
      "totalMilliseconds": 1
    }
    // ...more algorithms
  ],
  "inputDistribution": "random",
  "recommendedAlgorithm": "QuickSort",
  "baselineAlgorithm": "BubbleSort",
  "improvementPercentage": 66.7
}
```

## Technologies Used

- **Frontend**: Angular 19, TypeScript, CSS3, Chart.js (ng2-charts)
- **Backend**: ASP.NET Core 9, C#
- **Development**: Angular CLI, .NET CLI

## Project Structure

```
sorting-optimizer/
├── sorting-optimizer-backend/     # ASP.NET Core API
│   ├── Controllers/               # API controllers
│   ├── Models/                    # Data models
│   ├── Services/                  # Business logic
│   └── Program.cs                 # Application entry point
└── sorting-optimizer-frontend/    # Angular application
    ├── src/
    │   ├── app/
    │   │   ├── analyze/           # Main component
    │   │   └── services/          # API service
    │   └── main.ts               # Application entry point
    └── package.json
```

## Advanced Features

- Handles arrays up to 1 million elements
- Logs timing at 10ms intervals for large arrays
- Analyzes input distribution (sorted, reverse, random, nearly sorted, duplicates)
- Recommends the optimal algorithm for your data
- Compares baseline and dynamic algorithms, showing improvement
- Drag-and-drop upload for CSV/JSON
- Multiple visualization types (bar, line, scatter, table)

## Testing

- Try small and large arrays (e.g., 5,2,9,1,5,6 or 10000,9999,...,1)
- Upload CSV/JSON files for large datasets
- Switch between visualization types
- Check recommended algorithm and improvement stats
- Edge cases: empty input, non-numeric input, duplicate-heavy arrays

---

For more, see the frontend and backend README files or the [GitHub repo](https://github.com/pgvaghela/Sorting-Algorithm-Optimzier). 