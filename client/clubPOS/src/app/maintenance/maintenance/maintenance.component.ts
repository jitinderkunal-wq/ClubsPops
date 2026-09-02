import { Component, OnInit } from '@angular/core';
import { AccommodationStatusTypeMaintenanceLogWaitingList } from 'app/shared/models/accommodationWithTypeAndStatus';
import { MaintenanceService } from '../maintenance.service';
import { MatDialog } from '@angular/material/dialog';
import { ClearOutOfServiceComponent } from '../clear-out-of-service/clear-out-of-service.component';
import { OutOfServiceListComponent } from '../out-of-service-list/out-of-service-list.component';
import { SystemProblemComponent } from 'app/front-of-house/system-problem/system-problem.component';
import { AccountService } from 'app/account/account.service';
import { User } from 'app/shared/models/user';
import { environment } from 'environments/environment';

@Component({
  selector: 'app-maintenance',
  templateUrl: './maintenance.component.html',
  styleUrls: ['./maintenance.component.scss']
})
export class MaintenanceComponent  implements OnInit {
  currentorganizationId: number = 0;
  organizationLogo: string = '';
  organizationName: string = '';
  filePathApi: string = environment.logoPathApi;
  accommodations: AccommodationStatusTypeMaintenanceLogWaitingList[] = [];
  outOfServiceAccommodations: AccommodationStatusTypeMaintenanceLogWaitingList[] = [];
  accommodationTypeIdSelected: number = 0;
  accommodationStatusIdSelected: number = 0;
  totalAccommodations:number = 0;
  totalLockers: number = 0;
  lockersInUse: number = 0;
  totalRooms: number = 0;
  roomsInUse: number = 0;
  needMaintenance: number = 0;
  selectedAccommodationInfo: string = "";
  sideToggle: boolean = false;
  currentUser?: User | null;

  constructor(private maintenanceService: MaintenanceService, private matDialog: MatDialog, public accountService: AccountService) { }

  ngOnInit(): void {
    this.getCurrentUser();
    this.getCurrentOrganization();
    this.getMaintenanceAccommodations();
  }

  getCurrentUser() {
    this.accountService.currentUser$.subscribe({
      next: user => {
        this.currentUser = user;
        this.currentorganizationId = user? user.organizationId: 0;
      },
      error: error => console.log(error)
    });
  }

  getCurrentOrganization(){
    this.maintenanceService.currentOrganization(this.currentorganizationId).subscribe({
      next: response => {
        this.organizationLogo = this.filePathApi+response.logo!;
        this.organizationName = response.organizationName!;
      },
      error: error => console.log(error)
    });
  }

  sideMenuToggle(){
    this.sideToggle = !this.sideToggle;
  }

  refresh(){
    window.location.reload();
  }

  handleDisabledClick(event: Event): void {
    event.preventDefault(); // Prevent the default click action
    event.stopPropagation(); // Stop event propagation
  }

  getMaintenanceAccommodations(){
    this.maintenanceService.getAccommodations(this.accommodationTypeIdSelected, this.accommodationStatusIdSelected).subscribe({
      next: response => {
        this.accommodations = response;
        this.outOfServiceAccommodations = this.accommodations.filter(x => x.lastMaintenanceLog !== null);
        this.totalAccommodations = this.accommodations.length;
        this.totalLockers = this.accommodations.filter(accommodation => accommodation.accommodationTypeId === 1).length;
        this.lockersInUse = this.accommodations.filter(accommodation => accommodation.accommodationTypeId === 1 && accommodation.accommodationStatusId === 2).length;
        this.totalRooms = this.totalAccommodations - this.totalLockers;
        this.roomsInUse = this.accommodations.filter(accommodation => accommodation.accommodationTypeId !== 1 && accommodation.accommodationStatusId === 2).length;
        this.needMaintenance = this.accommodations.filter(accomodation => accomodation.accommodationStatusId === 3).length;
      },
      error: error => console.log(error)
    });
  }

  showAccommodationInfoNeedsMaintenance(accommodation: AccommodationStatusTypeMaintenanceLogWaitingList) {
    var maintenanceLogDetails = accommodation.lastMaintenanceLog?.maintenanceLogDetails;
    const taskNames = maintenanceLogDetails?.map(detail => detail.taskName);
    const info = 'Out of service for ' + taskNames?.join(', ');
    this.selectedAccommodationInfo = info;
  }

  openClearOutOfServicePopup(accommodation: AccommodationStatusTypeMaintenanceLogWaitingList) {
    this.matDialog.open(ClearOutOfServiceComponent, {
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