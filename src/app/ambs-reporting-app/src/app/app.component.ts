import { Component, OnInit } from '@angular/core';
import { DashboardService } from './services/dashboard.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ambs-reporting-app';
  public dashboards: any[] = [];
  constructor(private router: Router, private dashboardService: DashboardService){}

  ngOnInit(): void {
    this.showDashboards();
  }

  showDashboards(){
    this.dashboardService.getDashboards(1, 100).subscribe((res: any) => {
      this.dashboards = res;
      console.log(this.dashboards);
    });
  }

  redirectToDashboard(id){
    console.log(id);
    this.router.navigateByUrl('/report', {skipLocationChange: true}).then(()=> {
      this.router.navigateByUrl('/dashboard', {state: {dashboardId: id}});
    });
  }

  logout(){}
}
