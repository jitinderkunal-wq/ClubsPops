import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-waiting-list-alert',
  templateUrl: './waiting-list-alert.component.html',
  styleUrls: ['./waiting-list-alert.component.scss']
})
export class WaitingListAlertComponent {
  constructor(private matDialogRef: MatDialogRef<WaitingListAlertComponent>) { }

  closePopup() {
    this.matDialogRef.close();
  }
}
