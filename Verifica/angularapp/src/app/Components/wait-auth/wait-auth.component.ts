import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { interval, map } from 'rxjs';
import { Token2FADto } from '../../Models/token2fa.dto';
import { TokenService } from '../../Services/token.service';
import { EvaluateRiskResult } from '../../Models/evaluaterisk-result.dto';

@Component({
  selector: 'app-wait-auth',
  templateUrl: './wait-auth.component.html',
  styleUrls: ['./wait-auth.component.scss']
})
export class WaitAuthComponent implements OnInit {
  time!: {
    days: number;
    hours: number;
    minutes: number;
    seconds: number;
  };

  tStatus: string = "A";
  
  finishDate: Date = new Date();
  token?: Token2FADto;
  result2fa?: EvaluateRiskResult;

  constructor(private router: Router,
    private activatedRoute: ActivatedRoute,
    private tokenService: TokenService) {
    var state = this.router.getCurrentNavigation()!.extras.state;
    this.token = state!['token'];
    this.result2fa = state!['result2fa'];
  }

  ngOnInit(): void {
    
    console.log('token ' + this.token);    
    
    // Inicializamos el momento que falta hasta llegaral tiempo objetivo con valores en 0
    this.time = {
      days: 0, hours: 0, minutes: 0, seconds: 0
    };
    // Creamos la fecha a partir de la fecha en formato string AAAA-MM-dd HH:mm:ss
    this.finishDate = new Date(this.token!.creado);
    this.finishDate.setTime(this.finishDate.getTime() + 5 * 60000); //añado los 5 minutos

   // this.start().subscribe(_ => console.log("tik"));

    let counterTimer$ = this.start().subscribe((_) => {
      if (this.time.days < 0) {
        this.time = {
          hours: 0,
          minutes: 0,
          seconds: 0,
          days: 0
        }
        counterTimer$.unsubscribe();
      }
    });

    let tokenTimer$ = this.startToken().subscribe((_) => {
      if (this.tStatus != "A" && this.tStatus != 'V') {
          tokenTimer$.unsubscribe();
        }
    })
  }

  updateTime() {

    const now = new Date();
    
    const diff = this.finishDate.getTime() - now.getTime();
   // console.log(diff)

    // Cálculos para sacar lo que resta hasta ese tiempo objetivo / final
    const days = Math.floor(diff / (1000 * 60 * 60 * 24));
    const hours = Math.floor(diff / (1000 * 60 * 60));
    const mins = Math.floor(diff / (1000 * 60));
    const secs = Math.floor(diff / 1000);

    // La diferencia que se asignará para mostrarlo en la pantalla
    this.time.days = days;
    this.time.hours = hours - days * 24;
    this.time.minutes = mins - hours * 60;
    this.time.seconds = secs - mins * 60;
  }

  // Ejecutamos la acción cada segundo, para obtener la diferencia entre el momento actual y el objetivo
  start() {
    return interval(1000).pipe(
      map((x: number) => {
        this.updateTime();
        return x;
      })
    );
  }

  //Buscamos el token cada 5 segundos para ver si ha cambiado
  startToken() {
    return interval(5000).pipe(
      map(async (x: number) => {
        await this.tokenService.getTokenStatus(this.token!.id)
          .then(r => {
            this.tStatus = r;
            if (r == "A") {
              this.router.navigate(['/resultpage'], {
                state: { token: this.token }
              });
            } else if (r == "R" || r == "E") {
              this.router.navigate(['/home']);
            }
          })
        return x;
      })
    );
  }
}
