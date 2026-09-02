import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { AccommodationStatusTypeMaintenanceLogWaitingList } from 'app/shared/models/accommodationWithTypeAndStatus';
import { ClearOutOfServiceComponent } from '../clear-out-of-service/clear-out-of-service.component';


@Component({
  selector: 'app-out-of-service-list',
  templateUrl: './out-of-service-list.component.html',
  styleUrls: ['./out-of-service-list.component.scss']
})
export class OutOfServiceListComponent {
  outOfServiceAccommodations: AccommodationStatusTypeMaintenanceLogWaitingList[] = [];

  constructor(@Inject(MAT_DIALOG_DATA) public data: AccommodationStatusTypeMaintenanceLogWaitingList[], private matDialogRef: MatDialogRef<OutOfServiceListComponent>, private matDialog: MatDialog) {
    this.outOfServiceAccommodations = data;
  }

  onRowClick(outOfServiceAccommodation: any) {
    this.openClearOutOfServicePopup(outOfServiceAccommodation);
  }

  openClearOutOfServicePopup(outOfServiceAccommodation: AccommodationStatusTypeMaintenanceLogWaitingList) {
    this.matDialog.open(ClearOutOfServiceComponent, {
      data: outOfServiceAccommodation,
    });
  }

  closePopup() {
    this.matDialogRef.close();
  }
}