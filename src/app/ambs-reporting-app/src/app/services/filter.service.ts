import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FilterService {

  constructor(private http: HttpClient) {  }

  getFilters(page, size){
    return this.http.get(environment.apiEndPoint + 'filter?page=' + page + '&size=' + size);
  }

  getFilter(id){
    return this.http.get(environment.apiEndPoint + 'filter/' + id);
  }

  saveFilter(filter: any){
    return this.http.post(environment.apiEndPoint + 'filter', filter, environment.getHttpHeader());
  }
}
