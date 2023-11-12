import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { LoginInformation } from 'src/app/Models/login-information.dto';
import { catchError, lastValueFrom, map, throwError } from 'rxjs';
import { UsuarioDto } from '../Models/usuario.dto';

@Injectable({
  providedIn: 'root',
})
export class LoginService {

  constructor(private http: HttpClient) {
  }

  login(info: LoginInformation) {

    console.log("Voy a: " + environment.BASE_URL + environment.IdentityUrl);
    return this.http.post<UsuarioDto>(environment.BASE_URL + environment.IdentityUrl, info)
      .subscribe(data => {
        console.log(data);
      });
    

  }
}
