import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { firstValueFrom, throwError } from 'rxjs';
import { LoginInformation } from './Models/login-information.dto';
import { constants } from './constants';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  

  constructor(private http: HttpClient) {
    
  }

  title = 'Verifica';


  ngOnInit() {
    console.log("INIT");
    const info = new LoginInformation(constants.username, constants.password);
    firstValueFrom(this.http.post(constants.BASE_URL + constants.authUrl, info, { responseType: 'text' }))
      .then(t => {
        console.log("token en sesión: " + t);
        sessionStorage.setItem("token", t);
        console.log("token en sesión: " + sessionStorage.getItem("token"));
      })
      .catch(error => {
        console.log(error);
      });
    
  }
}
