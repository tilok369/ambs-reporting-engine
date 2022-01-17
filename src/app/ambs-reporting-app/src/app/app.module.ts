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
import { MetaDataService } from './services/meta-data.service';
import { ReportMetadataAddComponent } from './components/report-metadata/report-metadata-add/report-metadata-add.component';
import { ReportMetadataEditComponent } from './components/report-metadata/report-metadata-edit/report-metadata-edit.component';

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
    ReportEditComponent,
    ReportMetadataAddComponent,
    ReportMetadataEditComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    DashboardService,
    ReportService,
    MetaDataService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
