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

}
