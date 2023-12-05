import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { ResultPageRoutingModule } from './result-page-routing.module';
import { ResultPageComponent } from './result-page.component';

@NgModule({
  imports: [CommonModule, ReactiveFormsModule, ResultPageRoutingModule],
  declarations: [ResultPageComponent],
})
export class ResultPageModule { }
