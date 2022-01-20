import { Component, OnInit } from '@angular/core';
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
  constructor(private graphService: GraphService
    , private _reportService: ReportService) { }

  ngOnInit(): void {
    //this.renderGraph("chart-container-1", 6, '%40EndDate%232021-01-31%7C%40StateId%23-1%7C%40ZoneId%234%7C%40DistrictId%234%7C%40RegionId%2327%7C%40BranchId%2333');
    //this.renderGraph("chart-container-2", 5, '%40EndDate%232021-01-31%7C%40StateId%23-1%7C%40ZoneId%234%7C%40DistrictId%234%7C%40RegionId%2327%7C%40BranchId%2333');
    //this.renderGraph("chart-container-2", 5, '@EndDate#2021-01-31|@StateId#-1|@ZoneId#4|@DistrictId#4|@RegionId#27|@BranchId#33');
    this.getExportReportData(11, "%40BranchId%232%7C%40Date%232021-09-02");
    this.getExportReportDataLoanDisburseAndFullPayment(12, "%40BranchId%232%7C%40Date%232021-09-02");
  }


  renderGraph(chartContainerId, reportId, parameterVals) {
    this.graphService.getGraph(reportId, parameterVals).subscribe((res: any) => {
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
  }
  