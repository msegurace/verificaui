import { Component, Inject, Optional } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { errorMessages } from '../../Validators/form-validator';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { DialogBoxComponent } from '../dialog-box/dialog-box.component';
import { UsuarioDto } from '../../Models/usuario.dto';


@Component({
  selector: 'app-user-dialog',
  templateUrl: './user-dialog.component.html',
  styleUrl: './user-dialog.component.scss'
})
export class UserDialogComponent {
  action: string;
  local_data: any;
  users!: string[];
  cancel: string = 'Cancelar';

  tableForm!: FormGroup;
  errors = errorMessages;

  constructor(
    public dialogRef: MatDialogRef<DialogBoxComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: UsuarioDto,
    private formBuilder: FormBuilder) {
      this.local_data = { ...data };
      this.action = this.local_data.action;
      this.createForm();
      this.tableForm.patchValue(this.local_data);
  }

  createForm(): void {
    this.tableForm = this.formBuilder.group({
      nombre: ['', [Validators.required]],
      apellido1: ['', Validators.required],
      apellido2: ['', Validators.required],
      username: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      telefono: ['', Validators.required],
      admin: ['', Validators.required],
      guid: ['', Validators.required]
    });
  }

  closeDialog() {
    this.dialogRef.close({ data: { action: 'Cancel' } });
  }

  onSubmit(): void {
    this.dialogRef.close({ data: { action: this.action, data: this.tableForm.value } });
  }

}
