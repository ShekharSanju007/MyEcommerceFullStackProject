import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DatafetchService {
  private apiUrl = 'http://localhost:5022/api/Admin';

  constructor(public http:HttpClient) { }


getData():Observable<any> {
  return this.http.get("$this.apiUrl/admin");

}


postData(data:any):Observable<any>{
  return this.http.post('http://localhost:5022/api/Admin',data);
}











}
