import { Component, OnInit } from '@angular/core';
import { ReportType } from 'src/app/enums/report-enum';
import { DashboardWidgetReportVM, ReportVM } from 'src/app/models/dashboard/dashboard-widget-report.model';
import { DashboardService } from 'src/app/services/dashboard.service';
import { GraphService } from 'src/app/services/graph.service';
import { ReportService } from 'src/app/services/report.service';
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
  constructor(private _graphService: GraphService
    , private _reportService: ReportService
    , private _dashboardService: DashboardService) { }

  ngOnInit(): void {
    // this.renderGraph("chart-container-1", 6, '%40EndDate%232021-01-31%7C%40StateId%23-1%7C%40ZoneId%234%7C%40DistrictId%234%7C%40RegionId%2327%7C%40BranchId%2333');
    // this.renderGraph("chart-container-2", 5, '%40EndDate%232021-01-31%7C%40StateId%23-1%7C%40ZoneId%234%7C%40DistrictId%234%7C%40RegionId%2327%7C%40BranchId%2333');
    // //this.renderGraph("chart-container-2", 5, '@EndDate#2021-01-31|@StateId#-1|@ZoneId#4|@DistrictId#4|@RegionId#27|@BranchId#33');
    // this.getExportReportData(11, "%40BranchId%232%7C%40Date%232021-09-02");
    // this.getExportReportDataLoanDisburseAndFullPayment(12, "%40BranchId%232%7C%40Date%232021-09-02");
    this.getDashboardWidgetReport(15);
  }

  getDashboardWidgetReport(dashboardId: number) {
    this._dashboardService.getDashboardWidgetReport(dashboardId).subscribe((res: any) => {
      this.dashboard = res;
      console.log(this.dashboard);
    })
  }
  renderGraph(chartContainerId, reportId, parameterVals) {
    this._graphService.getGraph(reportId, parameterVals).subscribe((res: any) => {
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
    this._reportService.getExportReportData(reportId, paramVals).subscribe((res: any) => {
      console.log(res);
      this.transactionSummaryReceiveAndPayment = res;
      this.transactionSummaryReceiveAndPayment.forEach(td => {
        td.isActive = false;
      });
      this.transactionSummaryReceiveAndPayment[0].isActive = true;
    })
  }
  getExportReportDataLoanDisburseAndFullPayment(reportId: number, paramVals: string) {
    this._reportService.getExportReportData(reportId, paramVals).subscribe((res: any) => {
      console.log(res);
      this.loanDisburseAndFullPayment = res;
      this.loanDisburseAndFullPayment.forEach(td => {
        td.isActive = false;
      });
      this.loanDisburseAndFullPayment[0].isActive = true;
    })
  }
  getParamVals(report: ReportVM): string {
    let paramVals: string = ''
    report.filters.forEach(flt => {
      if (flt.value)
        paramVals = paramVals + '%40' + flt.parameter + '%23' + flt.value + '%7C';
    })
    return "%40BranchId%232%7C%40Date%232021-09-02";
  }
  getReportData(report: ReportVM) {
    if (report.type === ReportType.Tabular) {
      this._reportService.getExportReportData(report.id, this.getParamVals(report)).subscribe((res: any) => {
        report.data = res;
        report.data.forEach(td => {
          td.isActive = false;
        });
      })
    } else {
      this._graphService.getGraph(report.id, this.getParamVals(report)).subscribe((res: any) => {
        report.data = res;
      })
    }
  }
}

