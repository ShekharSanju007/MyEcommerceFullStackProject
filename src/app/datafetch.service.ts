import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DatafetchService {
  private logOutSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  logOut$: Observable<boolean> = this.logOutSubject.asObservable();

  private apiUrl = 'http://localhost:4694/api';

  constructor(public http: HttpClient, private router: Router) {}

  getData(): Observable<any> {
    return this.http.get(this.apiUrl + '/admin');
  }

  RegisterData(data: any): Observable<any> {
    return this.http.post(this.apiUrl + '/admin/Register', data);
  }

  LoginData(data: any): Observable<any> {
    return this.http.post(this.apiUrl + '/admin/Login', data);
  }

  storeToken(tokenValue: string) {
    localStorage.setItem('token', tokenValue);
    this.logOutSubject.next(false);
  }

  getToken$(): Observable<string | null> {
    return of(localStorage.getItem('token'));
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

  signOut() {
    localStorage.clear();
    this.logOutSubject.next(true);
    this.router.navigate(['/login']);
  }

  handleUnauthorizedError(){
    this.logOutSubject.next(true);

  }
}
