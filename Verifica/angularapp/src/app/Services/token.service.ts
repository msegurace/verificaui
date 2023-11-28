import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EvaluateRiskInformation } from '../Models/evaluaterisk-information.dto';
import { firstValueFrom } from 'rxjs';
import { GenericResponse } from '../Models/GenericResponse';
import { constants } from '../constants';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  constructor(private http: HttpClient) {
  }

  async evaluateRisk(info: EvaluateRiskInformation) {
    return firstValueFrom(this.http.post<any>(constants.BASE_URL + constants.tokensUrl + '/evaluaterisk', info));
  }
}
