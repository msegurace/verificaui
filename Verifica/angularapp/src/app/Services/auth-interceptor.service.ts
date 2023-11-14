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
//import { LocalStorageService } from './local-storage.service';

@Injectable({
  providedIn: 'root',
})
export class AuthInterceptorService implements HttpInterceptor {
  access_token: string | null;

  constructor(private http: HttpClient) {
    this.access_token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwiZ2l2ZW5fbmFtZSI6Ik1PSVNFUyIsImZhbWlseV9uYW1lIjoiU0VHVVJBIENFRFJFUyIsImVtYWlsIjoibXNlZ2NlZEBtc2MuY29tIiwidW5pcXVlX25hbWUiOiJtc2VnY2VkIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvaG9tZXBob25lIjoiMTIzNDQzMjExIiwibmJmIjoxNjk5OTExODk5LCJleHAiOjE2OTk5OTgyOTksImlhdCI6MTY5OTkxMTg5OX0.iXTHmWybWBt-wj3qIUcsiVqhnfV4C3vXoY5Wx_6zLQU";
  }

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    var l = new LoginInformation(constants.username, constants.password)
    //firstValueFrom(this.http.post<any>(constants.authUrl, l)).then(a => this.access_token = a);
    if (this.access_token) {
      req = req.clone({
        setHeaders: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: `Bearer ${this.access_token}`,
        },
      });
    }

    return next.handle(req);
  }
}
