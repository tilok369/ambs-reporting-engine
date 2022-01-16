import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { DashboardService } from 'src/app/services/dashboard.service';

@Component({
  selector: 'app-report-configuration-add',
  templateUrl: './report-configuration-add.component.html',
  styleUrls: ['./report-configuration-add.component.css']
})
export class ReportConfigurationAddComponent implements OnInit {

  public dashboard: any;
  public message: string = '';

  constructor(private dashboardService: DashboardService) { }

  ngOnInit(): void {
    this.dashboard = {
      id: 0,
      name: '',
      iframeUrl: window.origin + '/dashboard?uid='+ new Date().getTime(),
      status: true,
      createdOn: new Date(),
      createdBy: "admin",
      updatedOn: new Date(),
      updatedBy: "admin"
    }
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
