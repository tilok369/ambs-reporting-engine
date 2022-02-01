import { Component, OnInit } from '@angular/core';
import { FilterType } from 'src/app/enums/filter-enum';
import { ExportType, ReportType } from 'src/app/enums/report-enum';
import { DashboardWidgetReportVM, FilterVM, ReportVM } from 'src/app/models/dashboard/dashboard-widget-report.model';
import { IDropdownFilter } from 'src/app/models/report/dropdown-filter.model';
import { DashboardService } from 'src/app/services/dashboard.service';
import { FilterService } from 'src/app/services/filter.service';
import { GraphService } from 'src/app/services/graph.service';
import { ReportService } from 'src/app/services/report.service';
import { environment } from 'src/environments/environment';
import * as CanvasJS from '../../../assets/canvasjs.min';
//var CanvasJS = require('../../../assets/canvasjs.min');

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  transactionSummaryReceiveAndPayment: any;
  loanDisburseAndFullPayment: any;
  dashboard: DashboardWidgetReportVM = new DashboardWidgetReportVM();
  dashboardId: number = 0;
  exportTypes: Array<IDropdownFilter> = [];
  constructor(private _graphService: GraphService
    , private _reportService: ReportService
    , private _dashboardService: DashboardService
    , private _filterService: FilterService) { }

  ngOnInit(): void {
    // this.renderGraph("chart-container-1", 6, '%40EndDate%232021-01-31%7C%40StateId%23-1%7C%40ZoneId%234%7C%40DistrictId%234%7C%40RegionId%2327%7C%40BranchId%2333');
    // this.renderGraph("chart-container-2", 5, '%40EndDate%232021-01-31%7C%40StateId%23-1%7C%40ZoneId%234%7C%40DistrictId%234%7C%40RegionId%2327%7C%40BranchId%2333');
    // //this.renderGraph("chart-container-2", 5, '@EndDate#2021-01-31|@StateId#-1|@ZoneId#4|@DistrictId#4|@RegionId#27|@BranchId#33');
    // this.getExportReportData(11, "%40BranchId%232%7C%40Date%232021-09-02");
    // this.getExportReportDataLoanDisburseAndFullPayment(12, "%40BranchId%232%7C%40Date%232021-09-02");
    this.exportTypes.push({ name: 'Excel', value: ExportType.Excel, sortOrder: 1 })
    this.exportTypes.push({ name: 'PDF', value: ExportType.PDF, sortOrder: 2 })

    this.dashboardId = window.history.state.dashboardId;
    console.log(this.dashboardId);
    if(this.dashboardId)
      this.getDashboardWidgetReport(this.dashboardId);
  }

  getDashboardWidgetReport(dashboardId: number) {
    this._dashboardService.getDashboardWidgetReport(dashboardId).subscribe((res: any) => {
      this.dashboard = res;
      console.log(this.dashboard);
    })
  }
  renderGraph(chartContainerId, reportId, parameterVals) {
    this._graphService.getGraph(this.dashboardId, reportId, parameterVals).subscribe((res: any) => {
      console.log(res);
      this.drawGraph(chartContainerId, res);
    });
  }

  drawGraph(chartContainerId, res) {
    let chart = new CanvasJS.Chart(chartContainerId, {
      animationEnabled: true,
      exportEnabled: true,
      title: {
        text: res.title
      },
      subtitles: [{
        text: res.subTitle
      }],
      axisX: {
        title: res.xaxisTitle,
        suffix: res.xaxisSuffix,
        prefix: res.xaxisPrefix
      },
      axisY: {
        title: res.yaxisTitle,
        suffix: res.yaxisSuffix,
        prefix: res.yaxisPrefix
      },
      data: [{
        type: res.type,
        dataPoints: res.dataPoints
      }]
    });

    chart.render();
  }

  getExportReportData(reportId: number, paramVals: string) {
    this._reportService.getExportReportData(this.dashboardId, reportId, paramVals).subscribe((res: any) => {
      console.log(res);
      this.transactionSummaryReceiveAndPayment = res;
      this.transactionSummaryReceiveAndPayment.forEach(td => {
        td.isActive = false;
      });
      this.transactionSummaryReceiveAndPayment[0].isActive = true;
    })
  }
  getExportReportDataLoanDisburseAndFullPayment(reportId: number, paramVals: string) {
    this._reportService.getExportReportData(this.dashboardId, reportId, paramVals).subscribe((res: any) => {
      console.log(res);
      this.loanDisburseAndFullPayment = res;
      this.loanDisburseAndFullPayment.forEach(td => {
        td.isActive = false;
      });
      this.loanDisburseAndFullPayment[0].isActive = true;
    })
  }
  public get filterType(): typeof FilterType {
    return FilterType;
  }
  public get reportType(): typeof ReportType {
    return ReportType;
  }
  getParamVals(report: ReportVM): string {
    let paramVals: string = ''
    report.filters.forEach(function (flt, index) {
      if (flt.type == FilterType.Dropdown && flt.filterValue)
        paramVals = paramVals + '%40' + flt.parameter + '%23' + (index == report.filters.length - 1 ? flt.filterValue?.value : flt.filterValue?.value + '%7C');
      else if (flt.value)
        paramVals = paramVals + '%40' + flt.parameter + '%23' + (index == report.filters.length - 1 ? flt.value : flt.value + '%7C');
    })
    return paramVals;
  }
  getParamWithNameValueForExport(report: ReportVM): string {
    let paramVals: string = ''
    report.filters.forEach(function (flt, index) {
      if (flt.type == FilterType.Dropdown && flt.filterValue)
        paramVals = paramVals + '%40' + flt.label + '%23' + (index == report.filters.length - 1 ? encodeURIComponent(flt.filterValue?.name) : encodeURIComponent(flt.filterValue?.name) + '%7C');
      else if (flt.value)
        paramVals = paramVals + '%40' + flt.label + '%23' + (index == report.filters.length - 1 ? flt.value : flt.value + '%7C');
    })
    return paramVals;
  }
  getReportData(report: ReportVM) {
    if (report.type === ReportType.Tabular) {
      this._reportService.getExportReportData(this.dashboardId, report.id, this.getParamVals(report)).subscribe((res: any) => {
        report.data = res;
        if (report.data) {
          report.data.forEach(td => {
            td.isActive = false;
          });
          report.data[0].isActive = true;
        }
      })
    } else {
      this._graphService.getGraph(this.dashboardId, report.id, this.getParamVals(report)).subscribe((res: any) => {
        report.data = res;
        this.drawGraph(report.id.toString(), report.data);
      })
    }
  }
  exportReport(report: ReportVM) {
    if (report.type == ReportType.Tabular)
      window.open(environment.apiEndPoint + 'report-export/export/' + this.dashboardId + '/' + report.id + '/' + this.getParamVals(report) + '/' + report.exportType + '/' + report.name + '/' + this.getParamWithNameValueForExport(report));
    else
      window.open(environment.apiEndPoint + 'graph/reportExport/' + this.dashboardId + '/Test/' + report.id + '/' + this.getParamVals(report));
  }
  ddfChange(report: ReportVM, filter: FilterVM) {
    if (!filter.dependentParameters) return;
    this._filterService.getDropdownFilter(this.dashboardId,report.id, filter.id, filter.filterValue?.value).subscribe((res: Array<IDropdownFilter>) => {
      report.filters.forEach(rf => {
        if (rf.parameter.toLowerCase() == filter.dependentParameters.toLowerCase()) {
          rf.dropdownFilters = res;
          rf.filterValue = res.length > 0 ? res[0] : { name: '', value: -1 };
          this.ddfChange(report, rf);
          return;
        }
      })
    })
  }
}

