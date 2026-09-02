import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AccommodationStatusTypeMaintenanceLogWaitingList } from 'app/shared/models/accommodationWithTypeAndStatus';
import { BookedAccommodation } from 'app/shared/models/bookedAccommodation';

@Component({
  selector: 'app-current-activity-list',
  templateUrl: './current-activity-list.component.html',
  styleUrls: ['./current-activity-list.component.scss']
})
export class CurrentActivityListComponent {
  bookedAccommodations: BookedAccommodation[];
  
  constructor(@Inject(MAT_DIALOG_DATA) public data: BookedAccommodation[], private matDialogRef: MatDialogRef<CurrentActivityListComponent>) {
    this.bookedAccommodations = data;
  }

  closePopup() {
    this.matDialogRef.close();
  }
}