import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GraphService {

  constructor(private http: HttpClient) { }

  getGraph(reportId, parameterVals){
    return this.http.get(environment.apiEndPoint + 'graph/?reportId=' + reportId + '&parameterVals=' + parameterVals);
  }
}
