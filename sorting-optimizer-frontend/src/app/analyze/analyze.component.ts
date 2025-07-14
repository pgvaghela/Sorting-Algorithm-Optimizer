import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ChartConfiguration } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { SortingService, SortResult, SortAnalysisResponse } from '../services/sorting.service';

@Component({
  selector: 'app-analyze',
  templateUrl: './analyze.component.html',
  styleUrls: ['./analyze.component.css'],
  imports: [FormsModule, CommonModule, BaseChartDirective],
  standalone: true
})
export class AnalyzeComponent {
  rawInput = '';
  results?: SortResult[];
  baseline?: SortResult;
  recommended?: SortResult;
  improvementPercent?: number;
  loading = false;
  error: string | null = null;

  // Visualization
  vizTypes = [
    { label: 'Bar Chart', value: 'bar' },
    { label: 'Line Chart', value: 'line' },
    { label: 'Scatter Plot', value: 'scatter' },
    { label: 'Table', value: 'table' }
  ];
  selectedViz = 'bar';

  // Chart.js data/config
  barChartData: ChartConfiguration<'bar'>['data'] = { labels: [], datasets: [] };
  lineChartData: ChartConfiguration<'line'>['data'] = { labels: [], datasets: [] };
  scatterChartData: ChartConfiguration<'scatter'>['data'] = { datasets: [] };

  constructor(private sortingService: SortingService) { }

  onFileDropped(event: DragEvent): void {
    event.preventDefault();
    this.error = null;
    const file = event.dataTransfer?.files?.[0];
    if (!file) return;
    const reader = new FileReader();
    reader.onload = () => {
      try {
        const text = reader.result as string;
        if (file.name.endsWith('.json')) {
          const arr = JSON.parse(text);
          if (Array.isArray(arr)) {
            this.rawInput = arr.join(',');
          } else {
            this.error = 'JSON must be an array of numbers.';
          }
        } else {
          // Assume CSV
          this.rawInput = text.replace(/\s+/g, '').replace(/\n/g, ',').replace(/,+/g, ',');
        }
      } catch {
        this.error = 'Failed to parse file.';
      }
    };
    reader.readAsText(file);
  }

  onDragOver(event: DragEvent): void {
    event.preventDefault();
  }

  onSubmit(): void {
    this.results = undefined;
    this.baseline = undefined;
    this.recommended = undefined;
    this.improvementPercent = undefined;
    this.error = null;

    let data: number[];
    try {
      data = this.rawInput
        .split(',')
        .map(s => parseFloat(s.trim()))
        .filter(n => !isNaN(n));
      if (data.length === 0) throw new Error();
    } catch {
      this.error = 'Please enter a comma-separated list of numbers.';
      return;
    }

    this.loading = true;
    this.sortingService.analyze(data).subscribe({
      next: (res: SortAnalysisResponse) => {
        this.results = res.results;
        this.baseline = res.baseline;
        this.recommended = res.recommended;
        this.improvementPercent = res.improvementPercent;
        this.prepareCharts();
        this.loading = false;
      },
      error: err => {
        console.error(err);
        this.error = 'Server error – check console for details.';
        this.loading = false;
      }
    });
  }

  onVizTypeChange(type: string) {
    this.selectedViz = type;
    this.prepareCharts();
  }

  prepareCharts() {
    if (!this.results) return;
    // Bar chart: algorithm vs. total time
    this.barChartData = {
      labels: this.results.map(r => r.algorithm),
      datasets: [{
        label: 'Elapsed Time (ms)',
        data: this.results.map(r => r.elapsedMilliseconds),
        backgroundColor: this.results.map(r => r.isRecommended ? '#007bff' : '#aaa')
      }]
    };
    // Line chart: timing intervals for each algorithm
    const maxIntervals = Math.max(...this.results.map(r => r.timingIntervals?.length || 0));
    this.lineChartData = {
      labels: Array.from({ length: maxIntervals }, (_, i) => `${i}`),
      datasets: this.results.map(r => ({
        label: r.algorithm,
        data: r.timingIntervals || [],
        borderColor: r.isRecommended ? '#007bff' : undefined,
        fill: false
      }))
    };
    // Scatter plot: algorithm vs. time (x: index, y: ms)
    this.scatterChartData = {
      datasets: this.results.map((r, i) => ({
        label: r.algorithm,
        data: [{ x: i, y: r.elapsedMilliseconds }],
        backgroundColor: r.isRecommended ? '#007bff' : '#aaa'
      }))
    };
  }
}
