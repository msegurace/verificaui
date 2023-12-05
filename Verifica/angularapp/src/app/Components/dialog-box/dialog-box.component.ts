import { Component, Inject, Optional } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { errorMessages, regExps } from '../../Validators/form-validator';

export interface UsersData {
  name: string;
  id: number;
}

@Component({
  selector: 'app-dialog-box',
  templateUrl: './dialog-box.component.html',
  styleUrls: ['./dialog-box.component.scss']
})
export class DialogBoxComponent {

  action: string;
  local_data: any;
  countries!: string[];
  cancel: string = 'Cancelar';

  tableForm!: FormGroup;
  errors = errorMessages;

  constructor(
    public dialogRef: MatDialogRef<DialogBoxComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: UsersData,
    private formBuilder: FormBuilder,) {
      this.local_data = { ...data };
      this.action = this.local_data.action;
      this.creatForm();
      this.tableForm.patchValue(this.local_data);
    }

  creatForm(): void {
    this.tableForm = this.formBuilder.group({
      descripcion: ['', [Validators.required]],
      url: ['', [Validators.required, Validators.pattern(regExps['url'])]],
      origen: ['', Validators.required],
      clasificacion: ['', Validators.required],
    });
  }

  closeDialog() {
    this.dialogRef.close({ data: { action: 'Cancel' } });
  }

  onSubmit(): void {
    this.dialogRef.close({ data: { action: this.action, data: this.tableForm.value } });
  }
}
