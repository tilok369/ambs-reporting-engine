import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WidgetService {

  constructor(private http: HttpClient) { }

  getWidgets(dashboardId, page, size){
    return this.http.get(environment.apiEndPoint + 'widget?dashboardId=' + dashboardId + '&page=' + page + '&size=' + size);
  }

  getWidget(id){
    return this.http.get(environment.apiEndPoint + 'widget/' + id);
  }

  saveWidget(widget: any){
    return this.http.post(environment.apiEndPoint + 'widget', widget, environment.getHttpHeader());
  }
}
