import { Injectable } from '@angular/core';
import { UsuarioDto } from '../Models/usuario.dto';
import { HttpClient } from '@angular/common/http';
import { Observable, firstValueFrom } from 'rxjs';
import { constants } from '../constants';
import { GenericResponse } from '../Models/GenericResponse';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) {
  }

  async register(user: UsuarioDto) {
        return firstValueFrom(this.http.post<UsuarioDto>(constants.BASE_URL + constants.usersUrl + "/add", user))
  }
}
