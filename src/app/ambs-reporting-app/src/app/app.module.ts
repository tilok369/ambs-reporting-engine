import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ReportConfigurationComponent } from './components/report-configuration/report-configuration.component';
import { ReportMetadataComponent } from './components/report-metadata/report-metadata.component';
import { DashboardService } from './services/dashboard.service';
import { ReportConfigurationAddComponent } from './components/report-configuration/report-configuration-add/report-configuration-add.component';
import { ReportConfigurationEditComponent } from './components/report-configuration/report-configuration-edit/report-configuration-edit.component';
import { FormsModule } from '@angular/forms';
import { ReportListComponent } from './components/report/report-list/report-list.component';
import { ReportAddComponent } from './components/report/report-add/report-add.component';
import { ReportEditComponent } from './components/report/report-edit/report-edit.component';
import { ReportService } from './services/report.service';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    ReportConfigurationComponent,
    ReportMetadataComponent,
    ReportConfigurationAddComponent,
    ReportConfigurationEditComponent,
    ReportListComponent,
    ReportAddComponent,
    ReportEditComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    DashboardService,
    ReportService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
