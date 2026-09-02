import { Component, OnInit } from '@angular/core';
import { AccommodationStatusTypeMaintenanceLogWaitingList } from 'app/shared/models/accommodationWithTypeAndStatus';
import { CleaningService } from '../cleaning.service';
import { MatDialog } from '@angular/material/dialog';
import { CleaningCompleteComponent } from '../cleaning-complete/cleaning-complete.component';
import { AccountService } from 'app/account/account.service';
import { environment } from 'environments/environment';
import { OutOfServiceListComponent } from 'app/maintenance/out-of-service-list/out-of-service-list.component';
import { SystemProblemComponent } from 'app/front-of-house/system-problem/system-problem.component';



@Component({
  selector: 'app-cleaning',
  templateUrl: './cleaning.component.html',
  styleUrls: ['./cleaning.component.scss']
})

export class CleaningComponent implements OnInit {
  currentorganizationId: number = 0;
  organizationLogo: string = '';
  organizationName: string = '';
  filePathApi: string = environment.logoPathApi;
  accommodations: AccommodationStatusTypeMaintenanceLogWaitingList[] = [];
  outOfServiceAccommodations: AccommodationStatusTypeMaintenanceLogWaitingList[] = [];
  accommodationTypeIdSelected: number = 0;
  accommodationStatusIdSelected: number = 0;
  totalLockers: number = 0;
  lockersInUse: number = 0;
  totalRooms: number = 0;
  roomsInUse: number = 0;
  needCleaning: number = 0;
  selectedAccommodationInfo: string = "";
  sideToggle: boolean = false;


  constructor(private cleaningService: CleaningService, public accountService: AccountService, private matDialog: MatDialog) { }

  ngOnInit(): void {
    this.loadCurrentUser();
    this.getCurrentOrganization();
    this.getCleaningAccommodations();
  }

  loadCurrentUser(){
    this.accountService.currentUser$.subscribe({
      next: user => {
        this.currentorganizationId = user? user.organizationId: 0;
      },
      error: error => console.log(error)
    });
  }

  getCurrentOrganization(){
    this.cleaningService.currentOrganization(this.currentorganizationId).subscribe({
      next: response => {
        this.organizationLogo = this.filePathApi+response.logo!;
        this.organizationName = response.organizationName!;
      },
      error: error => console.log(error)
    });
  }

  handleDisabledClick(event: Event): void {
    event.preventDefault(); // Prevent the default click action
    event.stopPropagation(); // Stop event propagation
  }

  refresh(){
    window.location.reload();
  }

  sideMenuToggle(){
    this.sideToggle = !this.sideToggle;
  }

  getCleaningAccommodations() {
    this.cleaningService.getAccommodations(this.accommodationTypeIdSelected, this.accommodationStatusIdSelected).subscribe({
      next: response => {
        this.accommodations = response;
        this.outOfServiceAccommodations = this.accommodations.filter(x => x.lastMaintenanceLog !== null);
        const totalAccommodations = this.accommodations.length;
        this.totalLockers = this.accommodations.filter(accommodation => accommodation.accommodationTypeId === 1).length;
        this.lockersInUse = this.accommodations.filter(accommodation => accommodation.accommodationTypeId === 1 && accommodation.accommodationStatusId === 2).length;
        this.totalRooms = totalAccommodations - this.totalLockers;
        this.roomsInUse = this.accommodations.filter(accommodation => accommodation.accommodationTypeId !== 1 && accommodation.accommodationStatusId === 2).length;
        this.needCleaning = this.accommodations.filter(accomodation => accomodation.accommodationStatusId === 4).length;
      },
      error: error => console.log(error)
    });
  }

  showAccommodationInfoNeedsCleaning(accommodation: AccommodationStatusTypeMaintenanceLogWaitingList) {
    const info = 'Needs cleaning';
    this.selectedAccommodationInfo = info;
  }

  openCleaningCompletePopup(accommodation: AccommodationStatusTypeMaintenanceLogWaitingList) {
    this.matDialog.open(CleaningCompleteComponent, {
      data: accommodation,
    });
  }

  openOutOfServiceListPopup(outOfServiceAccommodations: AccommodationStatusTypeMaintenanceLogWaitingList[]) {
    this.matDialog.open(OutOfServiceListComponent, {
      data: outOfServiceAccommodations,
    });
  }

  openSystemProblemPopup() {
    this.matDialog.open(SystemProblemComponent, {
    });
  }
}