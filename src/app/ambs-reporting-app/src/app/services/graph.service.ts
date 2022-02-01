import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GraphService {

  constructor(private http: HttpClient) { }

  getGraph(dashboardId,reportId, parameterVals){
    return this.http.get(environment.apiEndPoint + 'graph/?dashboardId='+dashboardId+'&reportId=' + reportId + '&parameterVals=' + parameterVals);
  }
}
