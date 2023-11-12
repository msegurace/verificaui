import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home.component';
import { ReactiveFormsModule } from '@angular/forms';
import { FormatDatePipe } from 'src/app/Pipes/format-date.pipe';

@NgModule({
  imports: [CommonModule, ReactiveFormsModule, HomeRoutingModule],
  declarations: [HomeComponent, FormatDatePipe],
})
export class HomeModule { }
