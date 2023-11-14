import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  UntypedFormControl,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { HeaderMenusService } from 'src/app/Services/header-menus.service';
import { Observable, catchError, firstValueFrom, throwError } from 'rxjs';
import { UsuarioDto } from '../../Models/usuario.dto';
import { LoginService } from '../../Services/login.service';
import { SharedService } from '../../Services/shared.service';
import { LoginInformation } from '../../Models/login-information.dto';
import { HeaderMenus } from '../../Models/header-menu.dto';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  loginUser?: UsuarioDto;
  loginInformation: LoginInformation;
  username?: FormControl;
  password?: FormControl;
  loginForm?: FormGroup;
  responseOK: boolean = false;
  errorResponse: any;

  constructor(
    private formBuilder: FormBuilder,
    private authService: LoginService,
    private sharedService: SharedService,
    private headerMenusService: HeaderMenusService,
    private router: Router
  ) {
    this.loginInformation = new LoginInformation('', '');

    this.initForm();
  }

  initForm = () => {
    this.username = new UntypedFormControl('', [
      Validators.required
    ]);

    this.password = new UntypedFormControl('', [
      Validators.required,
      Validators.minLength(6),
      Validators.maxLength(16),
    ]);

    this.loginForm = this.formBuilder.group({
      email: this.username,
      password: this.password,
    });
  };

  ngOnInit(): void { }

  async login() {
    this.loginInformation.username = this.username!.value;
    this.loginInformation.password = this.password!.value;
    try {
      await this.handleLogin();
      this.responseOK = this.loginUser != null;
    } catch (error: any) {
      this.responseOK = false;
      console.log('login error: ' + error.error);
      this.handleLoginError(error);
    }
    this.handleLoginToast();
  }

  handleLogin = async () => {
    try {

      await this.authService.login(this.loginInformation)
        .then(user => {
          this.loginUser = user;
          sessionStorage.setItem('username', this.loginUser!.username);
        })
        .catch(error => throwError(() => error));
    } catch (error: any) {
      throwError(() => error);
    }
  };

  handleLoginToast = async () => {
    await this.sharedService.managementToast(
      'loginFeedback',
      this.responseOK,
      this.errorResponse
    );

    if (this.responseOK) {
      this.updateOptionsMenu();
    }
  };

  handleLoginError = (error: any) => {
    this.responseOK = false;
    this.errorResponse = error.error;
    this.handleError(this.errorResponse);
    const headerInfo: HeaderMenus = {
      showAuthSection: false,
      showNoAuthSection: true,
    };
    this.headerMenusService.headerManagement.next(headerInfo);
  };

  updateOptionsMenu = () => {
    const headerInfo: HeaderMenus = {
      showAuthSection: true,
      showNoAuthSection: false,
    };
    this.headerMenusService.headerManagement.next(headerInfo);
    this.router.navigateByUrl('home');
  };

  private handleError = (errorResponse: any): void => {
    this.sharedService.errorLog(errorResponse);
  };
}
