import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { LoginService } from './Services/login.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  

  constructor(private http: HttpClient,
    private loginService: LoginService) {
    
  }

  title = 'Verifica';


  ngOnInit() {
      this.loginService.auth();
    }
}
