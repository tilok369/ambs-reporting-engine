import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/services/dashboard.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-report-configuration',
  templateUrl: './report-configuration.component.html',
  styleUrls: ['./report-configuration.component.css']
})
export class ReportConfigurationComponent implements OnInit {

  public dashboards: any[] = [];
  constructor(private router: Router, private dashboardService: DashboardService) { }

  ngOnInit(): void {
    this.getDasgboards(1, 20);
  }

  getDasgboards(page, size){
    this.dashboardService.getDashboards(page, size).subscribe((res: any) => {
      console.log(res);
      this.dashboards = res;
    });
  }

  addDashboard(){
    this.router.navigateByUrl('/report-configuration-add');
  }

  editDashboard(id){
    this.router.navigateByUrl('/report-configuration-edit', {state: {dashboardId: id}});
  }

  showWidgets(id){
    this.router.navigateByUrl('/widget', {state: {dashboardId: id}});
  }

}
