import { Component, OnInit } from '@angular/core';
import { ReportType } from 'src/app/enums/report-enum';
import { IReport } from 'src/app/models/report/report.model';
import { ReportService } from 'src/app/services/report.service';
import { FilterService } from 'src/app/services/filter.service';

@Component({
  selector: 'app-report-add',
  templateUrl: './report-add.component.html',
  styleUrls: ['./report-add.component.css']
})
export class ReportAddComponent implements OnInit {

  report: any;
  message: string = '';
  filterList: Array<any> = [];
  selectedFilterList: Array<any> = [];
  deSelectedFilterList: Array<any> = [];

  constructor(private _reportService: ReportService
    , private _filterService: FilterService) { }

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
      this.selectedFilterList.filter(f=>f.id!=0).forEach(f=>{
        this.report.reportFilterList.push(f.id);
      })
    }
    console.log(this.report)
    return
    this._reportService.add(this.report).subscribe((res: any) => {
      this.message = res.message;
    })
  }

}
