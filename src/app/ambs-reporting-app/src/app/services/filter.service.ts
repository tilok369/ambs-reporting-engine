import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Filter } from '../models/report/filter.model';

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
  getGraphTypes():Observable<any> {
    return this.http.get(environment.apiEndPoint + 'filter/graphType');
  }
  getDropdownFilter(dashboardId:number,reportId:number,filterId:number,filterValue:any):Observable<any>{
    return this.http.get(environment.apiEndPoint + 'filter/dropdownvalues/'+dashboardId+'/'+reportId+"/"+filterId+"/"+filterValue);
  }
}
