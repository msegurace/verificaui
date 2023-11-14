import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { AplicacionDto } from '../Models/aplicacion.dto';
import { environment } from '../../environments/environment';
import { constants } from '../constants';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {

  constructor(private http: HttpClient) {
  }

  async getall() {
    return firstValueFrom(this.http.get<AplicacionDto[]>(constants.appsUrl))
  }
}
