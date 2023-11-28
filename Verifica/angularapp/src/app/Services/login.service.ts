import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { LoginInformation } from 'src/app/Models/login-information.dto';
import { Observable, catchError, firstValueFrom, lastValueFrom, map, of, tap, throwError } from 'rxjs';
import { UsuarioDto } from '../Models/usuario.dto';
import { constants } from '../constants';

@Injectable({
  providedIn: 'root',
})
export class LoginService {

  constructor(private http: HttpClient) {
  }

  async login(info: LoginInformation) {

    return firstValueFrom(this.http.post<UsuarioDto>(constants.BASE_URL + constants.loginUrl, info))
  }
}
