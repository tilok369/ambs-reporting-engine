import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/services/dashboard.service';
import { FilterService } from 'src/app/services/filter.service';

@Component({
  selector: 'app-report-filter-edit',
  templateUrl: './report-filter-edit.component.html',
  styleUrls: ['./report-filter-edit.component.css']
})
export class ReportFilterEditComponent implements OnInit {

  public filter: any;
  public dashboards: any[] = [];
  public message: string = '';

  constructor(private filterService: FilterService, private dashboardService: DashboardService) { }

  ngOnInit(): void {
    this.filter = {
      id: 0,
      name: '',
      label: '',
      parameter:''
    };
    var id = window.history.state.filterId;
    this.getDasgboards(1, 100);
    this.getFilter(id);
  }

  getDasgboards(page, size){
    this.dashboardService.getDashboards(page, size).subscribe((res: any) => {
      console.log(res);
      this.dashboards = res;
    });
  }

  getFilter(id){
    this,this.filterService.getFilter(id).subscribe((res: any) => {
      this.filter = res;
    });
  }

  validateFilter(){
    if(!this.filter.name){
      this.message = 'Name is required';
      return false;
    }
    return true;
  }

  saveFilter(){
    this.message = '';
    console.log(this.filter);
    if(this.validateFilter()){
      this.filterService.saveFilter(this.filter).subscribe((res: any) => {
        console.log(res);
        this.message = res.message;
      });
    }
  }

}
