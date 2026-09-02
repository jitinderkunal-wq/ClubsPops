import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-transfer-alert',
  templateUrl: './transfer-alert.component.html',
  styleUrls: ['./transfer-alert.component.scss']
})
export class TransferAlertComponent {
  constructor(private matDialogRef: MatDialogRef<TransferAlertComponent>) { }

  closePopup() {
    this.matDialogRef.close();
  }
}
