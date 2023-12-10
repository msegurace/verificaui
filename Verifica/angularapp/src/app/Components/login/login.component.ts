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
import { EvaluateRiskResult } from '../../Models/evaluaterisk-result.dto';
import { MatSnackBar } from '@angular/material/snack-bar';

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
  idApp: string;
  app?: AplicacionDto;
  result2FA?: EvaluateRiskResult;
  evaluateriskinfo?: EvaluateRiskInformation;
  

  constructor(
    private formBuilder: FormBuilder,
    private authService: LoginService,
    private tokenService: TokenService,
    private sharedService: SharedService,
    private headerMenusService: HeaderMenusService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private snackbar: MatSnackBar) {
    this.loginInformation = new LoginInformation('', '');

    this.idApp = this.activatedRoute.snapshot.paramMap.get('id')!;
    if (this.idApp === null || this.idApp === undefined || this.idApp == "") {
      this.idApp = "1";
    }
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
      //this.handleLoginError(error);
      this.snackbar.open(`Error en login ${error.error}`, 'Close', {
        duration: 2000, horizontalPosition: 'right', verticalPosition: 'top'
      })
    }
  }

  handle2FA = async () => {
    try {
     
        this.evaluateriskinfo = new EvaluateRiskInformation(
          this.loginUser!.id!,
          +this.idApp
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
          this.authService.setLoggedIn(true);
        })
        .catch(error => throwError(() => {
          console.log(error);
        }));
    } catch (error: any) {
      throwError(() => error);
    }
  };

  handleLoginToast = async () => {
 
    if (!this.result2FA?.error!) {
      if (this.result2FA?.admin! + this.result2FA?.origin! + this.result2FA?.classification! + this.result2FA?.time! >= 20) {
        await this.tokenService.createToken(this.evaluateriskinfo!)
          .then(resp => {
            this.snackbar.open('La evaluación del riesgo inica que es necesario un 2FAx. Redireccionando.', 'Close', {
              duration: 5000, horizontalPosition: 'right', verticalPosition: 'top'
            })
            this.router.navigate(['/waitauth'], {
              state: { token: resp, result2fa: this.result2FA! }
            });
          })
          .catch(error => throwError(() => {
            console.log(error);
          }));
      } else {
        this.snackbar.open("Inicio de sesión correcto, NO hace falta 2FA... Redireccionando", 'Close', {
          duration: 5000, horizontalPosition: 'right', verticalPosition: 'top'
        })
        this.router.navigateByUrl('/resultpage');
      }      
    } else {
      this.snackbar.open(`Error Evaluando 2FA, se retorna al inicio.`, 'Close', {
        duration: 5000, horizontalPosition: 'right', verticalPosition: 'top'
      })
      this.router.navigateByUrl('/home');
    }
  };
  
 
}
