import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WaitAuthRoutingModule } from './wait-auth-routing.module';
import { WaitAuthComponent } from './wait-auth.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';

@NgModule({
  imports: [CommonModule, ReactiveFormsModule, WaitAuthRoutingModule, MatCardModule],
  declarations: [WaitAuthComponent],
})
export class WaitAuthModule { }
