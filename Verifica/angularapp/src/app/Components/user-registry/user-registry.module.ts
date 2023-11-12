import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserRegistryRoutingModule } from './user-registry-routing.module';
import { UserRegistryComponent } from './user-registry.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [CommonModule, ReactiveFormsModule, UserRegistryRoutingModule],
  declarations: [UserRegistryComponent],
})
export class UserRegistryModule { }
