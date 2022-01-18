import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-report-add',
  templateUrl: './report-add.component.html',
  styleUrls: ['./report-add.component.css']
})
export class ReportAddComponent implements OnInit {

  public report:any;
  public message: string = '';

  constructor() { }

  ngOnInit(): void {
    this.report = {
      id: 0,
      widgetId: 0,
      widgetName: '',
      name: '',
      status: true,
      type: 1,
      createdOn: new Date(),
      createdBy: "admin",
      updatedOn: new Date(),
      updatedBy: "admin",
      reportFilterList: [],
      tabularFeature: {
        id: 0,
        reportId: 0,
        script: '',
        title: '',
        subTitle: '',
        showFilterInfo: true,
        template: '',
        asOnDate: false,
        exportable: true,
        hasTotalColumn: false,
        hasTotalRow: false,
        createdOn: new Date(),
        createdBy: "admin",
        updatedOn: new Date(),
        updatedBy: "admin"
      },
      graphicalFeature: {
        id: 0,
        reportId: 0,
        script: '',
        title: '',
        subTitle: '',
        showFilterInfo: true,
        showLegend: true,
        xaxisTitle: '',
        yaxisTitle: '',
        xaxisSuffix: '',
        xaxisPrefix: '',
        yaxisSuffix: '',
        yaxisPrefix: '',
        createdOn: new Date(),
        createdBy: "admin",
        updatedOn: new Date(),
        updatedBy: "admin"
      }
    };
  }

  saveReport(){

  }

}
