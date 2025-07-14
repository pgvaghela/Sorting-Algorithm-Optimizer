import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { AnalyzeComponent } from './analyze/analyze.component';
import { SortingService } from './services/sorting.service';


@NgModule({
  declarations: [
    AppComponent,
    AnalyzeComponent      // ← our new component
  ],
  imports: [
    BrowserModule,
    HttpClientModule,     // ← for HTTP calls
    FormsModule           // ← for [(ngModel)]
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
