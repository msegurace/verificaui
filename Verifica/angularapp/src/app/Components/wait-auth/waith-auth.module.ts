import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WaitAuthRoutingModule } from './wait-auth-routing.module';
import { WaitAuthComponent } from './wait-auth.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [CommonModule, ReactiveFormsModule, WaitAuthRoutingModule],
  declarations: [WaitAuthComponent],
})
export class WaitAuthModule { }
