import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HeaderMenus } from 'src/app/Models/header-menu.dto';
import { HeaderMenusService } from 'src/app/Services/header-menus.service';
import { LoginService } from '../../Services/login.service';
import { Observable } from 'rxjs';
//import { LocalStorageService } from 'src/app/Services/local-storage.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
  isLoggedIn$: Observable<boolean> | undefined;  

  constructor(
    private router: Router,
    private headerMenusService: HeaderMenusService,
    private loginService: LoginService
  ) {
    
  }

  ngOnInit(): void {

    this.isLoggedIn$ = this.loginService.isLoggedIn;
    this.isLoggedIn$.subscribe(c => console.log("LoggedIn: " + c));
  }

  home(): void {
    this.router.navigateByUrl('home');
  }

  login(): void {
    this.router.navigateByUrl('login');
  }

  register(): void {
    this.router.navigateByUrl('register');
  }

  manageApps(): void {
    this.router.navigateByUrl('apps');
  }

  manageUsers(): void {
    this.router.navigateByUrl('users');
  }

  logout(): void {
    this.loginService.logout();                      
    this.router.navigateByUrl('home');
  }
}
