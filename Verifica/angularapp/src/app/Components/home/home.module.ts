import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home.component';
import { ReactiveFormsModule } from '@angular/forms';
import { FormatDatePipe } from 'src/app/Pipes/format-date.pipe';
import { MatCardModule } from '@angular/material/card';
import { MatToolbarModule } from '@angular/material/toolbar';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@NgModule({
  imports: [CommonModule, ReactiveFormsModule, HomeRoutingModule,
    MatCardModule,
    FlexLayoutModule,
    MatToolbarModule,
    MatExpansionModule,
    MatProgressSpinnerModule],
  declarations: [HomeComponent, FormatDatePipe],
})
export class HomeModule { }
