import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor() {}

  //We want this interceptor to fetch our token from local storage and set it in our header in our requests
  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const token = localStorage.getItem('token')

    if(token){
      request = request.clone({
        setHeaders: {
          Authorization : `Bearer ${token}`
        }
      });
    }

    return next.handle(request)
  }
}
