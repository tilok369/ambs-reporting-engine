import { Component, OnInit } from '@angular/core';
import { GraphService } from 'src/app/services/graph.service';
import * as CanvasJS from '../../../assets/canvasjs.min';
//var CanvasJS = require('../../../assets/canvasjs.min');

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor(private graphService: GraphService) { }

  ngOnInit(): void {
    this.renderGraph(5, '%40EndDate%232021-01-31%7C%40StateId%23-1%7C%40ZoneId%234%7C%40DistrictId%234%7C%40RegionId%2327%7C%40BranchId%2333');
  }

  renderGraph(reportId, parameterVals){
    this.graphService.getGraph(reportId, parameterVals).subscribe((res: any) => {
      console.log(res);
      var dataPoints: any = [];
      for(let d of res.dataPoints)
      {
        dataPoints.push({y: d.y, label: d.label});
      }

      let chart = new CanvasJS.Chart("chart-container", {
      animationEnabled: true,
      exportEnabled: true,
      title: {
        text: res.title
      },
      data: [{
        type: res.type,
        dataPoints: dataPoints
      }]
    });
      
    chart.render();
    });
  }

}
