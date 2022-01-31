import { Component, Input, OnInit } from '@angular/core';
import { ReportType } from 'src/app/enums/report-enum';
import { ReportService } from 'src/app/services/report.service';
import { FilterService } from 'src/app/services/filter.service';
import { Report } from 'src/app/models/report/report.model';
import { InputOutputService } from 'src/app/services/input-output.service';
import { IDropdownFilter } from 'src/app/models/report/dropdown-filter.model';

@Component({
  selector: 'app-report-add',
  templateUrl: './report-add.component.html',
  styleUrls: ['./report-add.component.css']
})
export class ReportAddComponent implements OnInit {
  report: Report = new Report();
  message: string = '';
  filterList: Array<any> = [];
  selectedFilterList: Array<any> = [];
  deSelectedFilterList: Array<any> = [];
  graphTypeList: Array<IDropdownFilter> = [];

  constructor(private _reportService: ReportService
    , private _filterService: FilterService
    , private _inputOutputService: InputOutputService) {
    // this._inputOutputService.selectedWidget.subscribe((res:any)=>{
    //   this.widget=res;
    //   console.log(res);
    // })
  }

  ngOnInit(): void {
    this.report.widgetId = window.history.state.widgetId;
    this.report.widgetName = window.history.state.widgetName;
    this.getFilters();
    this.getGraphTypes();
  }
  getFilters() {
    this._filterService.getFilters(1, 50).subscribe((res: any) => {
      console.log(res);
      this.filterList = res;
      //this.filterList.unshift({ id: 0, name: '' });
      this.deSelectedFilterList = [...this.filterList];
      //this.selectedFilterList.unshift({ id: 0, name: '' });
    })
  }
  getGraphTypes() {
    this._filterService.getGraphTypes().subscribe((res: Array<IDropdownFilter>) => {
      this.graphTypeList = res;
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
  validate(): string {
    let errorMessage: string = '';
    if (!this.report.name) errorMessage += 'Name is required<br>';
    if (!this.report.type) errorMessage += 'Type is required<br>';
    if (this.report.type == ReportType.Tabular) {
      if (!this.report.tabularFeature.script) errorMessage += 'Script is required<br>';
    } else {
      if (!this.report.graphicalFeature.script) errorMessage += 'Script is required<br>';
      if (!this.report.graphicalFeature.graphType) errorMessage += 'Graph Type is required<br>';
    }
    return errorMessage;
  }
  saveReport() {
    this.message = this.validate();
    if (this.message.length > 0) return;
    this.report.reportFilterList = [];
    if (this.selectedFilterList.length > 0) {
      this.selectedFilterList.filter(f => f.id != 0).forEach((f, i) => {
        this.report.reportFilterList?.push({ id: 0, reportId: 0, filterId: f.id, sortOrder: i + 1 })
      })
    }
    // this.report.graphicalFeature=this.report.type===ReportType.Graphical?this.report.graphicalFeature:null;
    // this.report.tabularFeature=this.report.type===ReportType.Tabular?this.report.tabularFeature:null;
    console.log(this.report)
    //return
    this._reportService.add(this.report).subscribe((res: any) => {
      this.message = res.message;
      if (res.success) {
        this.report = new Report();
        this.report.widgetId = window.history.state.widgetId;
        this.report.widgetName = window.history.state.widgetName;
      }
    })
  }
  // todo = ['Get to work', 'Pick up groceries', 'Go home', 'Fall asleep'];

  // done = ['Get up', 'Brush teeth', 'Take a shower', 'Check e-mail', 'Walk dog'];

  // drop(event: CdkDragDrop<string[]>) {
  //   if (event.previousContainer === event.container) {
  //     moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
  //   } else {
  //     transferArrayItem(
  //       event.previousContainer.data,
  //       event.container.data,
  //       event.previousIndex,
  //       event.currentIndex,
  //     );
  //   }
  // }

}

