import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login.component';
import { LoginRoutingModule } from './login-routing.module';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';

@NgModule({
  imports: [CommonModule, ReactiveFormsModule, LoginRoutingModule,
    MatCardModule,
    MatFormFieldModule,
    FlexLayoutModule,
    MatButtonModule,
    MatInputModule
  ],
  declarations: [LoginComponent],
})
export class LoginModule { }
