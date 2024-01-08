import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApplicationService } from '../../Services/application.service';
import { UserService } from '../../Services/user.service';
import { Token2FADto } from '../../Models/token2fa.dto';
import { Router } from '@angular/router';
import { AplicacionDto } from '../../Models/aplicacion.dto';
import { UsuarioDto } from '../../Models/usuario.dto';

@Component({
  selector: 'app-result-page',
  templateUrl: './result-page.component.html',
  styleUrl: './result-page.component.scss'
})
export class ResultPageComponent implements OnInit {
  application?: string;
  username?: string;
  token?: Token2FADto;

  constructor(private router: Router,
    private appService: ApplicationService,
    private userService: UserService) {
    var state = this.router.getCurrentNavigation()!.extras.state;
    this.token = state!['token'];
    this.username = state!['username'];
  }

  async ngOnInit(): Promise<void> {
    try {
      await this.appService.get(this.token!.idaplicacion.toString()).then(
        resp => {
            const app: AplicacionDto = resp;
            this.application = app.descripcion;
          }
      );

      await this.userService.get(this.token!.idusuario.toString()).then(
        resp => {
            const usr: UsuarioDto = resp;
            this.username = `${usr.username} - ${usr.nombre} ${usr.apellido1} ${usr.apellido2}`;
        }
      );

    } catch (error: any) {
      console.log(error);
    }
  }
}
