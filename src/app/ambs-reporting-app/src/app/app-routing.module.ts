import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ReportConfigurationAddComponent } from './components/report-configuration/report-configuration-add/report-configuration-add.component';
import { ReportConfigurationEditComponent } from './components/report-configuration/report-configuration-edit/report-configuration-edit.component';
import { ReportConfigurationComponent } from './components/report-configuration/report-configuration.component';
import { ReportMetadataAddComponent } from './components/report-metadata/report-metadata-add/report-metadata-add.component';
import { ReportMetadataEditComponent } from './components/report-metadata/report-metadata-edit/report-metadata-edit.component';
import { ReportMetadataComponent } from './components/report-metadata/report-metadata.component';
import { ReportAddComponent } from './components/report/report-add/report-add.component';
import { ReportEditComponent } from './components/report/report-edit/report-edit.component';
import { ReportListComponent } from './components/report/report-list/report-list.component';
import { WidgetAddComponent } from './components/widget/widget-add/widget-add.component';
import { WidgetEditComponent } from './components/widget/widget-edit/widget-edit.component';
import { WidgetComponent } from './components/widget/widget.component';

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
    path: 'widget',
    component: WidgetComponent
  },
  {
    path: 'widget-add',
    component: WidgetAddComponent
  },
  {
    path: 'widget-edit',
    component: WidgetEditComponent
  },
  {
    path: 'report',
    component: ReportListComponent
  },
  {
    path: 'report-add',
    component: ReportAddComponent
  },
  {
    path: 'report-edit',
    component: ReportEditComponent
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
