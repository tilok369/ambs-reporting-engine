import { HttpHeaders } from "@angular/common/http";

export const environment = {
  production: true,
  apiEndPoint: 'https://localhost:7078/api/v1.0/',
  header: new HttpHeaders().set("Access-Control-Allow-Origin", "*")
};
