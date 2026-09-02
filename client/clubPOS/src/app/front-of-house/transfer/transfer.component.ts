import { Component, Inject, OnInit } from '@angular/core';
import { FrontOfHouseService } from '../front-of-house.service';
import { AccommodationStatus } from 'app/shared/models/accommodationStatus';
import { AccommodationType } from 'app/shared/models/accommodationType';
import { MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { ClearOutOfServiceComponent } from '../clear-out-of-service/clear-out-of-service.component';
import { ActivatedRoute, Router } from '@angular/router';
import { AccommodationStatusTypeMaintenanceLogWaitingList } from 'app/shared/models/accommodationWithTypeAndStatus';
import { BookedAccommodation } from 'app/shared/models/bookedAccommodation';
import { TransferBooking } from 'app/shared/models/transferBooking';

@Component({
  selector: 'app-transfer',
  templateUrl: './transfer.component.html',
  styleUrls: ['./transfer.component.scss']
})

export class TransferComponent implements OnInit {
  bookedAccommodationId: number;
  accommodations: AccommodationStatusTypeMaintenanceLogWaitingList[] = [];
  bookedAccommodations: BookedAccommodation[] = [];
  outOfServiceAccommodations: AccommodationStatusTypeMaintenanceLogWaitingList[] = [];
  activeWaitingListAccommodations: AccommodationStatusTypeMaintenanceLogWaitingList[] = [];
  accommodationStatuses: AccommodationStatus[] = [];
  selectedStatus: string = '';
  accommodationStatusIdSelected: number = 0;
  accommodationTypes: AccommodationType[] = [];
  selectedType: string = '';
  accommodationTypeIdSelected: number = 0;
  totalLockers: number = 0;
  lockersInUse: number = 0;
  totalRooms: number = 0;
  roomsInUse: number = 0;
  selectedAccommodationInfo: string = "";
  loading = false;
  transfer: any;
  maxOccupants: number = 0;



  constructor(@Inject(MAT_DIALOG_DATA) public data: number, private fohService: FrontOfHouseService, private matDialog: MatDialog, private router: Router) { 
    this.bookedAccommodationId = data;
  }

  ngOnInit(): void {
    this.getAccommodations();
    this.getBookedAccommodations();
  }

  getAccommodations() {
    this.fohService.getAccommodations(this.accommodationTypeIdSelected, this.accommodationStatusIdSelected).subscribe({
      next: response => {
        this.accommodations = response;
        this.outOfServiceAccommodations = this.accommodations.filter(x => x.lastMaintenanceLog !== null);
        this.activeWaitingListAccommodations = this.accommodations.filter(x => x.activeWaitingList !== null);
        const totalAccommodations = this.accommodations.length;
        this.totalLockers = this.accommodations.filter(accommodation => accommodation.accommodationTypeId === 1).length;
        this.lockersInUse = this.accommodations.filter(accommodation => accommodation.accommodationTypeId === 1 && accommodation.accommodationStatusId !== 5).length;
        this.totalRooms = totalAccommodations - this.totalLockers;
        this.roomsInUse = this.accommodations.filter(accommodation => accommodation.accommodationTypeId !== 1 && accommodation.accommodationStatusId !== 5).length;
        // Prevent Degrade while transfering accommodation
        // this.accommodations.forEach(accommodation => {
        //   if (accommodation.accommodationId === this.bookedAccommodationId && accommodation.accommodationStatusId === 5) {
        //     this.maxOccupants = this.accommodationTypes.filter(x=>x.accommodationTypeId === accommodation.accommodationTypeId).find(x=>x.maxOccupants)?? 0;
        //   }
        // });
      },
      error: error => console.log(error)
    });
  }

  getBookedAccommodations() {
    this.fohService.getBookedAccommodations().subscribe({
      next: response => this.bookedAccommodations = response,
      error: error => console.log(error)
    });
  }

  showAccommodationInfo(accommodation: AccommodationStatusTypeMaintenanceLogWaitingList) {
    var info = accommodation.typeName + ': #' + accommodation.number + ' Floor' + ': #' + accommodation.floor;
    if (accommodation.activeWaitingList) {
      info += ' on Wait-List'
    }
    this.selectedAccommodationInfo = info;
  }

  showAccommodationInfoNotAvailable(accommodation: AccommodationStatusTypeMaintenanceLogWaitingList) {
    var maintenanceLogDetails = accommodation.lastMaintenanceLog?.maintenanceLogDetails;
    const taskNames = maintenanceLogDetails?.map(detail => detail.taskName);
    const info = 'Out of service for ' + taskNames?.join(', ');
    this.selectedAccommodationInfo = info;
  }

  transferBooking(id:any){
    this.loading = true;
    this.fohService.transferBooking(this.bookedAccommodationId).subscribe({
      next: response => {
        this.loading = false;   
        this.closePopup();
        this.transfer = response;
        if(this.transfer){
          localStorage.setItem("TransferAccommodationDetails", JSON.stringify(this.transfer))
          this.router.navigate(['/accommodations/' + id]);
        }
      },
      error: error => {
        console.error('Transfer is un-available', error);
        this.loading = false;
      }
    })
  }

  openTransferPopup(){
    this.matDialog.open(TransferComponent,{

    });
  }

  handleDisabledClick(event: Event): void {
    event.preventDefault(); // Prevent the default click action
    event.stopPropagation(); // Stop event propagation
  }
  
  closePopup() {
    this.matDialog.closeAll();
  }
}