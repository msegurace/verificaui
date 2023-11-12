import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppListRoutingModule } from './app-list-routing.module';
import { AppListComponent } from './app-list.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [CommonModule, ReactiveFormsModule, AppListRoutingModule],
  declarations: [AppListComponent],
})
export class AppListModule { }
