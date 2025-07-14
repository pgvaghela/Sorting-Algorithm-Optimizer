import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface SortRequest {
  data: number[];
}

export interface SortResult {
  algorithm: string;
  sortedData: number[];
  elapsedMilliseconds: number;
  timingIntervals?: number[];
  distributionProfile?: string;
  isRecommended?: boolean;
  comparisons?: number;
}

export interface SortAnalysisResponse {
  results: SortResult[];
  baseline?: SortResult;
  recommended?: SortResult;
  improvementPercent: number;
}

@Injectable({
  providedIn: 'root'
})
export class SortingService {
  // point this at your ASP.NET Core API
  private baseUrl = 'https://localhost:7149/api/sort';

  constructor(private http: HttpClient) { }

  analyze(data: number[]): Observable<SortAnalysisResponse> {
    return this.http.post<SortAnalysisResponse>(
      `${this.baseUrl}/analyze`,
      { data }
    );
  }

  // if you have other endpoints:
  best(data: number[]): Observable<SortResult> {
    return this.http.post<SortResult>(
      `${this.baseUrl}/best`,
      { data }
    );
  }
}
