# Sorting Optimizer

A full-stack application that analyzes and compares different sorting algorithms. The application consists of an Angular frontend and an ASP.NET Core backend.

## Features

- **Multiple Sorting Algorithms**: Bubble Sort, Quick Sort, and Merge Sort
- **Performance Analysis**: Measures execution time for each algorithm
- **Modern UI**: Clean, responsive interface built with Angular
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
   The backend will be available at `https://localhost:7149`

2. **Start the Frontend**:
   ```bash
   cd sorting-optimizer-frontend
   npm start
   ```
   The frontend will be available at `http://localhost:4200`

3. **Open your browser** and navigate to `http://localhost:4200`

### Usage

1. Enter a comma-separated list of numbers in the input field
2. Click "Analyze" to run all sorting algorithms
3. View the results showing:
   - Algorithm name
   - Sorted data
   - Execution time in milliseconds

### API Endpoints

- `POST /api/sort/analyze` - Returns results from all algorithms
- `POST /api/sort/best` - Returns only the fastest algorithm

### Example Request

```json
{
  "data": [5, 3, 8, 1, 2]
}
```

### Example Response

```json
[
  {
    "algorithm": "BubbleSort",
    "sortedData": [1, 2, 3, 5, 8],
    "elapsedMilliseconds": 0
  },
  {
    "algorithm": "QuickSort",
    "sortedData": [1, 2, 3, 5, 8],
    "elapsedMilliseconds": 0
  },
  {
    "algorithm": "MergeSort",
    "sortedData": [1, 2, 3, 5, 8],
    "elapsedMilliseconds": 0
  }
]
```

## Technologies Used

- **Frontend**: Angular 19, TypeScript, CSS3
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