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
import { LoginService } from './login.service';

@Injectable({
  providedIn: 'root',
})
export class AuthInterceptorService implements HttpInterceptor {
  constructor(private loginService: LoginService) {
  }

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    
      const token = localStorage.getItem('token')!;
      
      req = req.clone({
        setHeaders: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: `Bearer ${token}`,
        },
      });
    
    return next.handle(req);
  }
}
