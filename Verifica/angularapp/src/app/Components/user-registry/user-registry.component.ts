import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { tap, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { CustomValidators } from '../../Validators/CustomValidators';
import { UserService } from '../../Services/user.service';
import { UsuarioDto } from '../../Models/usuario.dto';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-user-registry',
  templateUrl: './user-registry.component.html',
  styleUrls: ['./user-registry.component.scss']
})
export class UserRegistryComponent {
  registerForm = new FormGroup({
    nombre: new FormControl<string>("", [Validators.required]),
    apellido1: new FormControl<string>("", [Validators.required]),
    apellido2: new FormControl<string>(""),
    username: new FormControl<string>("", [Validators.required]),
    password: new FormControl<string>("", [Validators.required]),
    passwordConfirm: new FormControl<string>("", [Validators.required]),
    guid: new FormControl<string>(""),
    email: new FormControl<string>("", [Validators.required, Validators.email]),
    telefono: new FormControl<string>("", [Validators.required]),
    admin: new FormControl<string>("")
  },
    // add custom Validators to the form, to make sure that password and passwordConfirm are equal
    { validators: CustomValidators.passwordsMatching }
  )

  constructor(
    private router: Router,
    private userService: UserService,
    private snackbar: MatSnackBar
  ) { }

  async register() {
    if (!this.registerForm.valid) {
      return;
    }

    const user = new UsuarioDto(
      this.registerForm.value.nombre!,
      this.registerForm.value.apellido1!,
      this.registerForm.value.apellido2!,
      this.registerForm.value.username!,
      this.registerForm.value.password!,
      this.registerForm.value.guid!,
      this.registerForm.value.email!,
      this.registerForm.value.telefono!,
      false
    )

    try {
      await this.userService.register(user)
        .then(u => {
          this.snackbar.open(`User created successfully`, 'Close', {
            duration: 2000, horizontalPosition: 'right', verticalPosition: 'top'
          })
        })
        .catch(error => {
          this.snackbar.open(`ERROR Creating user ${error}`, 'Close', {
            duration: 2000, horizontalPosition: 'right', verticalPosition: 'top'
          })
        });
    } catch (error: any) {
      throwError(() => error);
    }
     
  }
}
