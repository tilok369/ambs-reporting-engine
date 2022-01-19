import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReportService {  
  constructor(private http: HttpClient) { }
  getAll(page: number, size: number): Observable<any> {
    return this.http.get(environment.apiEndPoint + 'report?page=' + page + '&size=' + size);
  }
  add(report: any) {
    return this.http.post(environment.apiEndPoint + 'report', report, environment.getHttpHeader());
  }
}
