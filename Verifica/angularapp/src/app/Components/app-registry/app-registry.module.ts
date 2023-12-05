import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppRegistryRoutingModule } from './app-resgistry-routing.module';
import { AppRegistryComponent } from './app-registry.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';

@NgModule({
  imports: [CommonModule, ReactiveFormsModule, AppRegistryRoutingModule,
    MatSnackBarModule,
    MatCardModule,
    MatFormFieldModule,
    FlexLayoutModule,
    MatButtonModule,
    MatInputModule,
    MatSelectModule],
  declarations: [AppRegistryComponent],
})
export class AppRegistryModule { }
