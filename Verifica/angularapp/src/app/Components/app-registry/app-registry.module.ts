import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppRegistryRoutingModule } from './app-resgistry-routing.module';
import { AppRegistryComponent } from './app-registry.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [CommonModule, ReactiveFormsModule, AppRegistryRoutingModule],
  declarations: [AppRegistryComponent],
})
export class AppRegistryModule { }
