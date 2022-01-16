import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MetaDataService {

  constructor(private http: HttpClient) { }

  getMetadatas(page, size){
    return this.http.get(environment.apiEndPoint + 'metadata?page=' + page + '&size=' + size);
  }

  getMetadata(id){
    return this.http.get(environment.apiEndPoint + 'metadata/' + id);
  }

  saveMetadata(metadata: any){
    return this.http.post(environment.apiEndPoint + 'metadata', metadata, environment.getHttpHeader());
  }
}
