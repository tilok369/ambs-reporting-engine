import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ReportType } from 'src/app/enums/report-enum';
import { IReportList } from 'src/app/models/report/report-list.model';
import { ReportService } from 'src/app/services/report.service';

@Component({
  selector: 'app-report-list',
  templateUrl: './report-list.component.html',
  styleUrls: ['./report-list.component.css']
})
export class ReportListComponent implements OnInit {
widgetId:number=0;
widgetName:string='';
  reportList: Array<IReportList> = [];

  constructor(private _router: Router,
    private _reportService: ReportService) { }
  ngOnInit(): void {
    this.widgetId = window.history.state.widgetId;
    this.widgetName = window.history.state.widgetName;
    console.log(this.widgetId);
    this.getReports();
  }
  getReports() {
    if(!this.widgetId)return;
    this._reportService.getByWidget(this.widgetId,0, 100).subscribe((res: Array<IReportList>) => {
      this.reportList = res;
    })
  }
  public get reportType(): typeof ReportType {
    return ReportType;
  }
  addReport(){
    this._router.navigateByUrl('/report-add',{state:{widgetId:this.widgetId,widgetName:this.widgetName}});
  }

  editReport(id:number){
    this._router.navigateByUrl('/report-edit', {state: {reportId: id,widgetId:this.widgetId,widgetName:this.widgetName}});
  }
  deleteReport(id:number){

  }
}
