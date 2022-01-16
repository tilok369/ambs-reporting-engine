import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/services/dashboard.service';

@Component({
  selector: 'app-report-configuration-edit',
  templateUrl: './report-configuration-edit.component.html',
  styleUrls: ['./report-configuration-edit.component.css']
})
export class ReportConfigurationEditComponent implements OnInit {

  public dashboard: any;
  public message: string = '';

  constructor(private dashboardService: DashboardService) { }

  ngOnInit(): void {
    var id = window.history.state.dashboardId;
    console.log(window.history.state);
    this.getDashboard(id);
  }

  getDashboard(id){
    this.dashboardService.getDashboard(id).subscribe((res: any) => {
      this.dashboard = res;
    });
  }

  validateDashboard(){
    if(!this.dashboard.name){
      this.message = 'Name is required';
      return false;
    }
    return true;
  }

  saveDashboad(){
    this.message = '';
    console.log(this.dashboard);
    if(this.validateDashboard()){
      this.dashboardService.saveDashboard(this.dashboard).subscribe((res: any) => {
        console.log(res);
        this.message = res.message;
      });
    }
  }

}
