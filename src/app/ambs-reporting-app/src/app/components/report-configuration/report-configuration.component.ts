import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/services/dashboard.service';

@Component({
  selector: 'app-report-configuration',
  templateUrl: './report-configuration.component.html',
  styleUrls: ['./report-configuration.component.css']
})
export class ReportConfigurationComponent implements OnInit {

  public dashboards: any[] = [];
  constructor(private dashboardService: DashboardService) { }

  ngOnInit(): void {
    this.getDasgboards(1, 10);
  }

  getDasgboards(page, size){
    this.dashboardService.getDashboards(page, size).subscribe((res: any) => {
      console.log(res);
      this.dashboards = res;
    });
  }

}
