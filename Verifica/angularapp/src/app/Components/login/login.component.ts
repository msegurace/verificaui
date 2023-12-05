import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  UntypedFormControl,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { HeaderMenusService } from 'src/app/Services/header-menus.service';
import { Observable, catchError, firstValueFrom, throwError } from 'rxjs';
import { UsuarioDto } from '../../Models/usuario.dto';
import { LoginService } from '../../Services/login.service';
import { SharedService } from '../../Services/shared.service';
import { LoginInformation } from '../../Models/login-information.dto';
import { HeaderMenus } from '../../Models/header-menu.dto';
import { AplicacionDto } from '../../Models/aplicacion.dto';
import { EvaluateRiskInformation } from '../../Models/evaluaterisk-information.dto';
import { TokenService } from '../../Services/token.service';

@Component({
  selector: 'login',
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
  idApp?: number = 1;
  app?: AplicacionDto;
  result2FA?: boolean;
  evaluateriskinfo?: EvaluateRiskInformation;
  

  constructor(
    private formBuilder: FormBuilder,
    private authService: LoginService,
    private tokenService: TokenService,
    private sharedService: SharedService,
    private headerMenusService: HeaderMenusService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
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

  ngOnInit(): void {
    
  }

  async login() {
    this.loginInformation.username = this.username!.value;
    this.loginInformation.password = this.password!.value;
    try {
      await this.handleLogin();
      this.responseOK = this.loginUser != null;
      await this.handle2FA();
    } catch (error: any) {
      this.responseOK = false;
      console.log('login error: ' + error.error);
      this.handleLoginError(error);
    }
  }

  handle2FA = async () => {
    try {
      this.evaluateriskinfo = new EvaluateRiskInformation(
        this.loginUser!.id!,
        this.idApp!
      );
      await this.tokenService.evaluateRisk(this.evaluateriskinfo)
        .then(resp => {
          this.result2FA = resp;
          this.handleLoginToast();
        })
        .catch(error => throwError(() => {
          console.log(error);
        }));
    } catch (error: any) {
      throwError(() => error);
    }

  }

  handleLogin = async () => {
    try {

      await this.authService.login(this.loginInformation)
        .then(user => {
          this.loginUser = user;
          sessionStorage.setItem('username', this.loginUser!.username);
        })
        .catch(error => throwError(() => {
          console.log(error);
        }));
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
    this.result2FA = true;
    if (this.result2FA!) {
      await this.tokenService.createToken(this.evaluateriskinfo!)
        .then(resp => {
          console.log('token: ' + resp)
          this.router.navigate(['/waitauth'], {
            state: { token: resp}
          });
       })
        .catch(error => throwError(() => {
          console.log(error);
        }));
      
    } else {
      this.router.navigateByUrl('/home');
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
  };

  private handleError = (errorResponse: any): void => {
    this.sharedService.errorLog(errorResponse);
  };
}
