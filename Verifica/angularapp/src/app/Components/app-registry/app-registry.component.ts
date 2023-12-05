import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ApplicationService } from '../../Services/application.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AplicacionDto } from '../../Models/aplicacion.dto';
import { throwError } from 'rxjs';

@Component({
  selector: 'app-app-registry',
  templateUrl: './app-registry.component.html',
  styleUrls: ['./app-registry.component.scss']
})
export class AppRegistryComponent {
  registerForm = new FormGroup({
    descripcion: new FormControl<string>("", [Validators.required]),
    url: new FormControl<string>("", [Validators.required]),
    origen: new FormControl<string>(""),
    clasificacion_ens: new FormControl<string>("", [Validators.required]),
  });

  constructor(
    private router: Router,
    private applicationService: ApplicationService,
    private snackbar: MatSnackBar
  ) { }

  async register() {
    if (!this.registerForm.valid) {
      return;
    }

    const app = new AplicacionDto(
      this.registerForm.value.descripcion!,
      this.registerForm.value.url!,
      this.registerForm.value.origen!,
      this.registerForm.value.clasificacion_ens!
    )

    try {
      await this.applicationService.register(app)
        .then(u => {
          this.snackbar.open(`Aplicación creada con éxito`, 'Close', {
            duration: 2000, horizontalPosition: 'right', verticalPosition: 'top'
          })
        })
        .catch(error => {
          this.snackbar.open(`ERROR al crear la aplicación ${error}`, 'Close', {
            duration: 2000, horizontalPosition: 'right', verticalPosition: 'top'
          })
        });
    } catch (error: any) {
      throwError(() => error);
    }

  }
}
