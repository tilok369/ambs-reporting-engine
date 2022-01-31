import { Component, OnInit } from '@angular/core';
import { ReportType } from 'src/app/enums/report-enum';
import { IDropdownFilter } from 'src/app/models/report/dropdown-filter.model';
import { GraphicalFeature } from 'src/app/models/report/graphical-feature.model';
import { Report } from 'src/app/models/report/report.model';
import { TabularFeature } from 'src/app/models/report/tabular-feature.model';
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
      if(this.report.type==ReportType.Tabular)this.report.graphicalFeature=new GraphicalFeature();
      else this.report.tabularFeature=new TabularFeature();
      this.report.widgetName = window.history.state.widgetName;
      this.report.reportFilterList.forEach(rf=>{
        this.selectedFilterList=this.selectedFilterList.concat(this.filterList.filter(fl=>fl.id==rf.filterId));
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
      //this.filterList.unshift({ id: 0, name: '' });
      this.deSelectedFilterList = [...this.filterList];
      //this.selectedFilterList.unshift({ id: 0, name: '' });
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
    if (!selectedValue) return;
    this.deSelectedFilterList = this.deSelectedFilterList.filter(f => f.id != selectedValue.id);
    // this.selectedFilterList = this.filterList.filter((objFromA) => {
    //   return !this.deSelectedFilterList.find(function (objFromB) {
    //     return objFromA.id === objFromB.id
    //   })
    // })
    // if (!this.selectedFilterList.find(f => f.id == 0)) this.selectedFilterList.unshift({ id: 0, name: '' });
    this.selectedFilterList.push(selectedValue);
  }
  deSelectFilter(deSelectedValue: any) {
    if (!deSelectedValue) return;
    this.selectedFilterList = this.selectedFilterList.filter(f => f.id != deSelectedValue.id);
    this.deSelectedFilterList.push(deSelectedValue);
    // if (deSelectedValue.value == 0) return;
    // this.selectedFilterList = this.selectedFilterList.filter(f => f.id != deSelectedValue.value);
    // this.deSelectedFilterList = this.filterList.filter((objFromA) => {
    //   return !this.selectedFilterList.find(function (objFromB) {
    //     return objFromA.id === objFromB.id
    //   })
    // })
    // if (!this.deSelectedFilterList.find(f => f.id == 0)) this.deSelectedFilterList.unshift({ id: 0, name: '' });
  }
  downPosition(filter: any) {
    let currentIndex = this.selectedFilterList.indexOf(filter);
    if (currentIndex === this.selectedFilterList.length - 1) return;
    this.selectedFilterList=this.arrayMove(this.selectedFilterList,currentIndex,currentIndex+1);
  }
  upPosition(filter: any) {
    let currentIndex = this.selectedFilterList.indexOf(filter);
    if (currentIndex <1) return;
    this.selectedFilterList=this.arrayMove(this.selectedFilterList,currentIndex,currentIndex-1);
  }
  arrayMove(arr:any, oldIndex:number, newIndex:number) {
    if (newIndex >= arr.length) {
      var k = newIndex - arr.length + 1;
      while (k--) {
        arr.push(undefined);
      }
    }
    arr.splice(newIndex, 0, arr.splice(oldIndex, 1)[0]);
    return arr; 
  };
  public get reportType(): typeof ReportType {
    return ReportType;
  }
  validate():string{
    let errorMessage:string='';
    if(!this.report.name)errorMessage+='Name is required<br>';
      if(!this.report.type) errorMessage+='Type is required<br>';
      if(this.report.type==ReportType.Tabular){
       if(!this.report.tabularFeature.script) errorMessage+='Script is required<br>';
      }else{
        if(!this.report.graphicalFeature.script) errorMessage+='Script is required<br>';
        if(!this.report.graphicalFeature.graphType) errorMessage+='Graph Type is required<br>';
      }
    return errorMessage;
  }
  saveReport() {
    this.message=this.validate();
    if(this.message.length>0)return;
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
