import { Component, OnInit } from '@angular/core';
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
  reportList: Array<IReportList> = [];

  constructor(private _router: Router,
    private _reportService: ReportService) { }
  ngOnInit(): void {
    this.getReports();
  }
  getReports() {
    this._reportService.getAll(1, 10).subscribe((res: Array<IReportList>) => {
      this.reportList = res;
    })
  }
  public get reportType(): typeof ReportType {
    return ReportType;
  }
  addReport(){
    this._router.navigateByUrl('/report-add');
  }

  editReport(id:number){
    this._router.navigateByUrl('/report-edit', {state: {reportId: id}});
  }
  deleteReport(id:number){

  }
}
