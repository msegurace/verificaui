import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, firstValueFrom } from 'rxjs';
import { AplicacionDto } from '../Models/aplicacion.dto';
import { environment } from '../../environments/environment';
import { constants } from '../constants';
import { GenericResponse } from '../Models/GenericResponse';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {

  constructor(private http: HttpClient) {
  }

  async getall() {
    return firstValueFrom(this.http.get<GenericResponse>(constants.BASE_URL + constants.appsUrl + "/getall?page=1&take=10"));
  }

  async get(id: string) {
    return firstValueFrom(this.http.get<GenericResponse>(constants.BASE_URL + constants.appsUrl + `/get?id=${id.trim()}page=1&take=10`));
  }

  async register(app: AplicacionDto) {
    return firstValueFrom(this.http.post<AplicacionDto>(constants.BASE_URL + constants.appsUrl + "/add", app))
  }
}
