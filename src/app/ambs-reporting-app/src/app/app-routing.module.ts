import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ReportConfigurationAddComponent } from './components/report-configuration/report-configuration-add/report-configuration-add.component';
import { ReportConfigurationEditComponent } from './components/report-configuration/report-configuration-edit/report-configuration-edit.component';
import { ReportConfigurationComponent } from './components/report-configuration/report-configuration.component';
import { ReportMetadataAddComponent } from './components/report-metadata/report-metadata-add/report-metadata-add.component';
import { ReportMetadataEditComponent } from './components/report-metadata/report-metadata-edit/report-metadata-edit.component';
import { ReportMetadataComponent } from './components/report-metadata/report-metadata.component';
import { ReportFilterAddComponent } from './components/report-filter/report-filter-add/report-filter-add/report-filter-add.component';
import { ReportFilterComponent } from './components/report-filter/report-filter/report-filter.component';
import { ReportFilterEditComponent } from './components/report-filter/report-filter-edit/report-filter-edit/report-filter-edit.component';


const routes: Routes = [
  {
    path: 'dashboard',
    component: DashboardComponent
  },
  {
    path: 'report-configuration',
    component: ReportConfigurationComponent
  },
  {
    path: 'report-configuration-add',
    component: ReportConfigurationAddComponent
  },
  {
    path: 'report-configuration-edit',
    component: ReportConfigurationEditComponent
  },
  {
    path: 'report-metadata',
    component: ReportMetadataComponent
  },
  {
    path: 'report-metadata-add',
    component: ReportMetadataAddComponent
  },
  {
    path: 'report-metadata-edit',
    component: ReportMetadataEditComponent
  },
  {
    path: 'report-filter-add',
    component: ReportFilterAddComponent
  },
  {
    path: 'report-filter-edit',
    component: ReportFilterEditComponent
  },
  {
    path: 'report-filter',
    component: ReportFilterComponent
  },
  {
    path: '**',
    redirectTo: '/dashboard',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
