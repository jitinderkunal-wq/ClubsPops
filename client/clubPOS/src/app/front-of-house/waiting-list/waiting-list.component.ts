import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AccommodationStatusTypeMaintenanceLogWaitingList } from 'app/shared/models/accommodationWithTypeAndStatus';
import { FrontOfHouseService } from '../front-of-house.service';


@Component({
  selector: 'app-waiting-list',
  templateUrl: './waiting-list.component.html',
  styleUrls: ['./waiting-list.component.scss']
})
export class WaitingListComponent {
  activeWaitingListAccommodations: AccommodationStatusTypeMaintenanceLogWaitingList[] = [];
  accommodationId: number=0;
  isSelected: boolean = false;

  constructor(@Inject(MAT_DIALOG_DATA) public data: AccommodationStatusTypeMaintenanceLogWaitingList[], private matDialogRef: MatDialogRef<WaitingListComponent>, private fohService: FrontOfHouseService) {
    this.activeWaitingListAccommodations = data;
  }

  // Gettting Accommodation Id of clicked row
  getAccommodationId(id:number){
  this.accommodationId=id;
  this.isSelected = true;
  }

  // Bump Down Waiting
  // bumpDownWaiting(){
  //   // 
  // }

  // Delete Selected row from Waiting List Table
  deleteWaiting(){
    this.fohService.deleteWaitingList(this.accommodationId).subscribe({
      next : response => 
      {
        if (response == true) {
          console.log("Removed from Waiting List");
          this.closePopup();
          window.location.reload();
        }
        else{
          console.log("Unable to removed from Waiting List");
        }
      }
    })
  }

  closePopup() {
    this.matDialogRef.close();
  }
}
function foreach() {
  throw new Error('Function not implemented.');
}

