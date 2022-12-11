import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastr: ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      //to catch the error
      catchError(error => {
        if(error){
          if(error.status === 400){

            if(error.error.errors){
              throw error.error;
            } else{
              this.toastr.error(error.error.message, error.error.statusCode)
            }
            
          }

          if(error.status === 401){
            this.toastr.error(error.error.message, error.error.statusCode)
          }

          if(error.status === 404){     
            this.router.navigateByUrl('/not-found')
          }

          if(error.status === 500){
            //we want to pass the error to our internal server error component
            //we are passing the error object which contains the statusCode, message and details
            const navigationExtras: NavigationExtras = {state : {error: error.error}}
            this.router.navigateByUrl('/server-error', navigationExtras)
          }
        }
        //to handle an other error, we will just throw it
        return throwError(error);
      })
    );
  }
}
