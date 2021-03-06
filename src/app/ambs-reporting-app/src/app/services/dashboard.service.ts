import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  constructor(private http: HttpClient) { }

  getDashboards(page, size){
    return this.http.get(environment.apiEndPoint + 'dashboard?page=' + page + '&size=' + size);
  }

  getDashboard(id){
    return this.http.get(environment.apiEndPoint + 'dashboard/' + id);
  }
  getDashboardWidgetReport(dashboardId:number){
    return this.http.get(environment.apiEndPoint + 'dashboard/dashboard-widget-report/' + dashboardId);
  }

  saveDashboard(dashboad: any){
    return this.http.post(environment.apiEndPoint + 'dashboard', dashboad, environment.getHttpHeader());
  }

}
