import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ForgotPwdRoutingModule } from './forgot-pwd-routing.module';
import { ForgotPwdComponent } from './forgot-pwd.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [CommonModule, ReactiveFormsModule, ForgotPwdRoutingModule],
  declarations: [ForgotPwdComponent],
})
export class ForgotPwdModule { }
