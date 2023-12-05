import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AlertBoxComponent } from '../Components/alert-box/alert-box.component'

@Injectable({
  providedIn: 'root'
})
export class AlertService {

  dialogRef!: MatDialogRef<AlertBoxComponent>;

  constructor(private dialog: MatDialog) { }

  public open(options: any) {
    this.dialogRef = this.dialog.open(AlertBoxComponent, {
      data: {
        title: options.title,
        message: options.message,
        cancelText: options.cancelText,
        confirmText: options.confirmText
      },
    });
  }

  public confirmed(): Observable<any> {
    return this.dialogRef.afterClosed().pipe(take(1), map(res => {
      return res;
    }
    ));
  }
}
