import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DatafetchService {
  private apiUrl = 'http://localhost:5022/api';

  constructor(public http:HttpClient) { }


getData():Observable<any> {
  return this.http.get("$this.apiUrl/admin");

}

postData(data:any){
  return this.http.post("$this.apiUrl/admin",data);
}











}
