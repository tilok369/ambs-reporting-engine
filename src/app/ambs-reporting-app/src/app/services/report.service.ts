import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Report } from '../models/report/report.model';

@Injectable({
  providedIn: 'root'
})
export class ReportService {
    
  constructor(private http: HttpClient) { }
  get(id:number):Observable<any>{
    return this.http.get(environment.apiEndPoint + 'report/'+id);
  }
  getAll(page: number, size: number): Observable<any> {
    return this.http.get(environment.apiEndPoint + 'report?page=' + page + '&size=' + size);
  }
  add(report: any) {
    return this.http.post(environment.apiEndPoint + 'report', report, environment.getHttpHeader());
  }
  edit(report: Report) {
    return this.http.put(environment.apiEndPoint + 'report', report, environment.getHttpHeader());
  }
  getExportReportData(reportId:number,paramVals:string){
    return this.http.get(environment.apiEndPoint + 'report-export/data/'+reportId+'/'+paramVals);
  }
}
