import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { DatafetchService } from '../datafetch.service';
import { catchError, switchMap, take } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(private dataService: DatafetchService,private router:Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
 
    return this.dataService.getToken$().pipe(

      take(1),
      switchMap((myToken) => {
        if (myToken) {
          request = request.clone({
            setHeaders: { Authorization: `Bearer ${myToken}` }
          });
        }
        return next.handle(request).pipe(
           catchError((err:any)=>{
            if(err instanceof HttpErrorResponse)
            {
              if(err.status === 401)
              {
                this.router.navigate(['/login']);
                this.dataService.handleUnauthorizedError();
              }

            }
            return throwError(()=> new Error("Invalid email or password"))
           }
           )

        );
      })
    );
  }
}
