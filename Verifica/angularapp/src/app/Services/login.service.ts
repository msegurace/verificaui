import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { LoginInformation } from 'src/app/Models/login-information.dto';
import { BehaviorSubject, Observable, catchError, firstValueFrom, lastValueFrom, map, of, tap, throwError } from 'rxjs';
import { UsuarioDto } from '../Models/usuario.dto';
import { constants } from '../constants';
import { Route, Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class LoginService {

  private loggedIn = new BehaviorSubject<boolean>(false); 

  get isLoggedIn() {
    return this.loggedIn.asObservable();
  }

  setLoggedIn(status: boolean) {
    this.loggedIn.next(status);
  }

  constructor(private http: HttpClient, private router: Router) {
  }

  async login(info: LoginInformation) {

    return firstValueFrom(this.http.post<UsuarioDto>(constants.BASE_URL + constants.loginUrl, info));
  }

  async auth() {
    const info = new LoginInformation(constants.username, constants.password);
    firstValueFrom(this.http.post(constants.BASE_URL + constants.authUrl, info, { responseType: 'text' }))
      .then(t => {
        const token = t.replaceAll('"', '',);
        localStorage.setItem("token", token);
      })
      .catch(error => {
        console.log(error.message);
      });
  }



  logout() {
    this.loggedIn.next(false);
    sessionStorage.removeItem('username');
    this.router.navigate(['/']);
  }
}
