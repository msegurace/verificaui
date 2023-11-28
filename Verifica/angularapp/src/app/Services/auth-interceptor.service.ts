import {
    HttpClient,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, firstValueFrom} from 'rxjs';
import { constants } from '../constants'
import { LoginInformation } from '../Models/login-information.dto';

@Injectable({
  providedIn: 'root',
})
export class AuthInterceptorService implements HttpInterceptor {
  
  constructor() {
    
  }

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (sessionStorage.getItem('token')) {
      console.log('TENGO TOKEN: ' + sessionStorage.getItem('token'));
      req = req.clone({
        setHeaders: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: `Bearer ${sessionStorage.getItem('token')}`,
        },
      });
    }

    return next.handle(req);
  }
}
