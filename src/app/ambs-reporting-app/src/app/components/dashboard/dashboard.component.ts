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
  tableDatas:any;
  constructor(private graphService: GraphService
    ,private _reportService:ReportService) { }

  ngOnInit(): void {
     this.renderGraph("chart-container-1", 6, '%40EndDate%232021-01-31%7C%40StateId%23-1%7C%40ZoneId%234%7C%40DistrictId%234%7C%40RegionId%2327%7C%40BranchId%2333');
     this.renderGraph("chart-container-2", 5, '%40EndDate%232021-01-31%7C%40StateId%23-1%7C%40ZoneId%234%7C%40DistrictId%234%7C%40RegionId%2327%7C%40BranchId%2333');
    this.getExportReportData();
  }

  renderGraph(chartContainerId, reportId, parameterVals){
    this.graphService.getGraph(reportId, parameterVals).subscribe((res: any) => {
      console.log(res);
      this.drawGraph(chartContainerId, res);   
    });
  }

  drawGraph(chartContainerId, res){
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
  tabClicked(data:any){
    data.isActive=true;
    this.tableDatas.filter(dt=>dt.sheetName!==data.sheetName).forEach(td => {
      td.isActive=false;
    });
  }
  getExportReportData(){
this._reportService.getExportReportData().subscribe((res:any)=>{
  console.log(res);
  this.tableDatas=res;
  this.tableDatas.forEach(td => {
    td.isActive=false;
  });
  this.tableDatas[0].isActive=true;
})
  }

}
