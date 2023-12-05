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
  token: string = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwiZ2l2ZW5fbmFtZSI6Ik1PSVNFUyIsImZhbWlseV9uYW1lIjoiU0VHVVJBIENFRFJFUyIsImVtYWlsIjoibXNlZ2NlZEBtc2MuY29tIiwidW5pcXVlX25hbWUiOiJtc2VnY2VkIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvaG9tZXBob25lIjoiMTIzNDQzMjExIiwibmJmIjoxNzAxNzczMzM2LCJleHAiOjE3MDE4NTk3MzYsImlhdCI6MTcwMTc3MzMzNn0.ZOMX-Mt1LErHifDxBOf7PevNqiGLVJ87-VVV4p-SGTU';
  constructor() {
  }

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (sessionStorage.getItem('token')) {
      //console.log('TENGO TOKEN: ' + sessionStorage.getItem('token'));
      req = req.clone({
        setHeaders: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: `Bearer ${this.token}`,
        },
      });
    }

    return next.handle(req);
  }
}
