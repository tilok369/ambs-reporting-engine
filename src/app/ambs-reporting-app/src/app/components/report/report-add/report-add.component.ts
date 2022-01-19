import { Component, Input, OnInit } from '@angular/core';
import { ReportType } from 'src/app/enums/report-enum';
import { ReportService } from 'src/app/services/report.service';
import { FilterService } from 'src/app/services/filter.service';
import { Report } from 'src/app/models/report/report.model';
import { InputOutputService } from 'src/app/services/input-output.service';

@Component({
  selector: 'app-report-add',
  templateUrl: './report-add.component.html',
  styleUrls: ['./report-add.component.css']
})
export class ReportAddComponent implements OnInit {
  report: Report=new Report();
  message: string = '';
  filterList: Array<any> = [];
  selectedFilterList: Array<any> = [];
  deSelectedFilterList: Array<any> = [];
  widgetList:any;

  constructor(private _reportService: ReportService
    , private _filterService: FilterService
    ,private _inputOutputService:InputOutputService) { 
      // this._inputOutputService.selectedWidget.subscribe((res:any)=>{
      //   this.widget=res;
      //   console.log(res);
      // })
     }

  ngOnInit(): void {
    this.report.widgetId=window.history.state.widgetId;
    this.report.widgetName=window.history.state.widgetName;
    this.getFilters();
  }
  getFilters() {
    this._filterService.getFilters(1, 50).subscribe((res: any) => {
      console.log(res);
      this.filterList = res;
      this.filterList.unshift({id:0,name:''});
      this.deSelectedFilterList=[...this.filterList];
      this.selectedFilterList.unshift({id:0,name:''});
    })
  }
  selectFilter(selectedValue: any) {
    if(selectedValue.value==0)return;
    this.deSelectedFilterList = this.deSelectedFilterList.filter(f => f.id != selectedValue.value);
    this.selectedFilterList = this.filterList.filter((objFromA) => {
      return !this.deSelectedFilterList.find(function(objFromB) {
        return objFromA.id === objFromB.id
      })
    })
    if(!this.selectedFilterList.find(f=>f.id==0))this.selectedFilterList.unshift({id:0,name:''});
  }
  deSelectFilter(deSelectedValue: any) {
    if(deSelectedValue.value==0)return;
    this.selectedFilterList = this.selectedFilterList.filter(f => f.id != deSelectedValue.value);
    this.deSelectedFilterList = this.filterList.filter((objFromA) => {
      return !this.selectedFilterList.find(function(objFromB) {
        return objFromA.id === objFromB.id
      })
    })
    if(!this.deSelectedFilterList.find(f=>f.id==0))this.deSelectedFilterList.unshift({id:0,name:''});
  }
  public get reportType(): typeof ReportType {
    return ReportType;
  }
  saveReport() {
    if(this.selectedFilterList.length>0){
      this.selectedFilterList.filter(f=>f.id!=0).forEach((f,i) =>{
        this.report.reportFilterList?.push({id:0,reportId:0,filterId:f,sortOrder:i+1})
      })
    }
    console.log(this.report)
    return
    this._reportService.add(this.report).subscribe((res: any) => {
      this.message = res.message;
    })
  }

}
