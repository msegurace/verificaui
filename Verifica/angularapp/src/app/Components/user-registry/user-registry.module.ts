import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserRegistryRoutingModule } from './user-registry-routing.module';
import { UserRegistryComponent } from './user-registry.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';


@NgModule({
  imports: [CommonModule, ReactiveFormsModule, UserRegistryRoutingModule,
    MatSnackBarModule,
    MatCardModule,
    MatFormFieldModule,
    FlexLayoutModule,
    MatButtonModule,
    MatInputModule],
  declarations: [UserRegistryComponent],
})
export class UserRegistryModule { }
