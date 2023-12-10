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
  async getall() {
    return firstValueFrom(this.http.get<GenericResponse>(constants.BASE_URL + constants.usersUrl + "/getall?page=1&take=10"));
  }

  async get(id: string) {
    return firstValueFrom(this.http.get<GenericResponse>(constants.BASE_URL + constants.usersUrl + `/get?id=${id.trim()}page=1&take=10`));
  }

  async register(user: UsuarioDto) {
        return firstValueFrom(this.http.post<UsuarioDto>(constants.BASE_URL + constants.usersUrl + "/add", user))
  }
}
