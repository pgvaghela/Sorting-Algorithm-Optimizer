import { Component } from '@angular/core';
import { AnalyzeComponent } from './analyze/analyze.component';

@Component({
  selector: 'app-root',
  imports: [AnalyzeComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'sorting-optimizer-frontend';
}
