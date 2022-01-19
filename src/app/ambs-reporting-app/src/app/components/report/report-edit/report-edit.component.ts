import { Component, OnInit } from '@angular/core';
import { ReportType } from 'src/app/enums/report-enum';
import { IDropdownFilter } from 'src/app/models/report/dropdown-filter.model';
import { Report } from 'src/app/models/report/report.model';
import { FilterService } from 'src/app/services/filter.service';
import { ReportService } from 'src/app/services/report.service';

@Component({
  selector: 'app-report-edit',
  templateUrl: './report-edit.component.html',
  styleUrls: ['./report-edit.component.css']
})
export class ReportEditComponent implements OnInit {
  reportId: number = 0;
  public report: Report = new Report();
  public message: string = '';
  filterList: Array<any> = [];
  selectedFilterList: Array<any> = [];
  deSelectedFilterList: Array<any> = [];
  graphTypeList: Array<IDropdownFilter> = [];

  constructor(private _reportService: ReportService
    , private _filterService: FilterService) { }

  ngOnInit(): void {
    this.reportId = window.history.state.reportId;
    //----------- start: this will be changed with getReport() call -----------------------
    this.getFilters();
    //-------------- end --------------------------
  }
  getReport() {
    this._reportService.get(this.reportId).subscribe((res: Report) => {
      this.report = res;

      this.selectedFilterList=this.filterList.filter((objFromA) => {
        return !this.report.reportFilterList.find(function (objFromB) {
          return objFromA.id !== objFromB.filterId
        })
      })
      if (!this.selectedFilterList.find(f => f.id == 0)) this.selectedFilterList.unshift({ id: 0, name: '' });

      this.deSelectedFilterList = this.filterList.filter((objFromA) => {
        return !this.selectedFilterList.find(function (objFromB) {
          return objFromA.id === objFromB.id
        })
      })
      if (!this.deSelectedFilterList.find(f => f.id == 0)) this.deSelectedFilterList.unshift({ id: 0, name: '' });
    })
  }
  getFilters() {
    this._filterService.getFilters(1, 50).subscribe((res: any) => {
      console.log(res);
      this.filterList = res;
      this.filterList.unshift({ id: 0, name: '' });
      this.deSelectedFilterList = [...this.filterList];
      this.selectedFilterList.unshift({ id: 0, name: '' });
      this.getGraphTypes();
    })
  }
  getGraphTypes() {
    this._filterService.getGraphTypes().subscribe((res: Array<IDropdownFilter>) => {
      this.graphTypeList = res;
      this.getReport();
    })
  }
  selectFilter(selectedValue: any) {
    if (selectedValue.value == 0) return;
    this.deSelectedFilterList = this.deSelectedFilterList.filter(f => f.id != selectedValue.value);
    this.selectedFilterList = this.filterList.filter((objFromA) => {
      return !this.deSelectedFilterList.find(function (objFromB) {
        return objFromA.id === objFromB.id
      })
    })
    if (!this.selectedFilterList.find(f => f.id == 0)) this.selectedFilterList.unshift({ id: 0, name: '' });
  }
  deSelectFilter(deSelectedValue: any) {
    if (deSelectedValue.value == 0) return;
    this.selectedFilterList = this.selectedFilterList.filter(f => f.id != deSelectedValue.value);
    this.deSelectedFilterList = this.filterList.filter((objFromA) => {
      return !this.selectedFilterList.find(function (objFromB) {
        return objFromA.id === objFromB.id
      })
    })
    if (!this.deSelectedFilterList.find(f => f.id == 0)) this.deSelectedFilterList.unshift({ id: 0, name: '' });
  }
  public get reportType(): typeof ReportType {
    return ReportType;
  }
  saveReport() {
    this.report.reportFilterList=[];
    if(this.selectedFilterList.length>0){
      this.selectedFilterList.filter(f=>f.id!=0).forEach((f,i) =>{
        this.report.reportFilterList?.push({id:0,reportId:this.report.id,filterId:f.id,sortOrder:i+1})
      })
    }
    this._reportService.edit(this.report).subscribe((res: any) => {
      this.message = res.message;
      if (res.success) {
        this.report = new Report();
        this.report.widgetId = window.history.state.widgetId;
        this.report.widgetName = window.history.state.widgetName;
      }
    })

  }

}
