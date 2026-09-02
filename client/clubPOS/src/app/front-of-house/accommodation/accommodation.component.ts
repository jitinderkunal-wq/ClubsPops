import { Component, OnInit } from '@angular/core';
import { FrontOfHouseService } from '../front-of-house.service';
import { AccommodationStatus } from 'app/shared/models/accommodationStatus';
import { AccommodationType } from 'app/shared/models/accommodationType';
import { MatDialog } from '@angular/material/dialog';
import { ClearOutOfServiceComponent } from '../clear-out-of-service/clear-out-of-service.component';
import { OutOfServiceListComponent } from '../out-of-service-list/out-of-service-list.component';
import { SystemProblemComponent } from '../system-problem/system-problem.component';
import { CurrentActivityListComponent } from '../current-activity-list/current-activity-list.component';
import { AccommodationStatusTypeMaintenanceLogWaitingList } from 'app/shared/models/accommodationWithTypeAndStatus';
import { WaitingListComponent } from '../waiting-list/waiting-list.component';
import { CleaningCompleteComponent } from '../cleaning-complete/cleaning-complete.component';
import { BookedAccommodation } from 'app/shared/models/bookedAccommodation';
import { EndShiftComponent } from 'app/account/end-shift/end-shift.component';
import { AccountService } from 'app/account/account.service';
import { Organization } from 'app/shared/models/organization';
import { environment } from 'environments/environment';
import { Router } from '@angular/router';
import { CashDropDrawerSkimsComponent } from '../cash-drop-drawer-skims/cash-drop-drawer-skims.component';

@Component({
  selector: 'app-accommodation',
  templateUrl: './accommodation.component.html',
  styleUrls: ['./accommodation.component.scss']
})
export class AccommodationComponent implements OnInit {
  currentorganizationId: number = 0;
  currentUserName: string = "";
  organizationLogo: string = '';
  organizationName: string = '';
  shiftNumber: number = 0;
  drawerNumber: number = 0;
  filePathApi: string = environment.logoPathApi;
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
  overDueBookedAccommodations: BookedAccommodation[] = [];
  overdue: number = 0;
  selectedAccommodationInfo: string = "";
  sideToggle: boolean = false;
  date: string = "";

  constructor(private fohService: FrontOfHouseService, private matDialog: MatDialog,public accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
    this.loadCurrentUser();
    this.getCurrentDate();
    this.getActiveShift();
    this.getCurrentOrganization();
    this.getAccommodations();
    this.getAccommodationStatuses();
    this.getAccommodationTypes();
    this.getBookedAccommodations();
    this.getOverDueAccommodations();
  }

  loadCurrentUser(){
    this.accountService.currentUser$.subscribe({
      next: user => {
        this.currentorganizationId = user ? user.organizationId : 0;
        this.currentUserName = user ? user.firstName : "No user found";
      },
      error: error => console.log(error)
    });
  }

  getCurrentDate(){
    var currentDate = new Date();
    // Format the date
    this.date = currentDate.toLocaleDateString('en-US', {
        weekday: 'short',
        month: 'short',
        day: 'numeric',
        year: 'numeric'
    });
  }

  getActiveShift(){
    this.accountService.getActiveSession().subscribe({
      next: response => {
        const activeShift = response.find(x => x.isCurrentlyInUse === true);
        this.shiftNumber = activeShift ? activeShift.shiftNumber : 0;
        this.drawerNumber = activeShift ? activeShift.drawerNumber : 0;
      },
      error: error => console.log(error)
    });
  }

  getCurrentOrganization(){
    this.accountService.currentOrganization(this.currentorganizationId).subscribe({
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

  getAccommodations() {
    this.fohService.getAccommodations(this.accommodationTypeIdSelected, this.accommodationStatusIdSelected).subscribe({
      next: response => {
        this.accommodations = response;
        this.outOfServiceAccommodations = this.accommodations.filter(x => x.lastMaintenanceLog !== null);
        this.activeWaitingListAccommodations = this.accommodations.filter(x => x.activeWaitingList !== null);
        const totalAccommodations = this.accommodations.length;
        this.totalLockers = this.accommodations.filter(accommodation => accommodation.accommodationTypeId === 1).length;
        this.lockersInUse = this.accommodations.filter(accommodation => accommodation.accommodationTypeId === 1 && accommodation.accommodationStatusId === 2).length;
        this.totalRooms = totalAccommodations - this.totalLockers;
        this.roomsInUse = this.accommodations.filter(accommodation => accommodation.accommodationTypeId !== 1 && accommodation.accommodationStatusId === 2).length;
      },
      error: error => console.log(error)
    });
  }

  getOverDueAccommodations(){
    this.fohService.getOverDueAccommodations().subscribe({
      next : response => 
      {
        this.overDueBookedAccommodations = response;
        this.overdue = response.length;
      },
      error: error => console.log(error)
    });
  }

  isOverdue(accommodation: any): boolean {
    return this.overDueBookedAccommodations.some(a => a.bookedAccommodation.accommodationId === accommodation.accommodationId);
  }

  getBookedAccommodations() {
    this.fohService.getBookedAccommodations().subscribe({
      next: response => this.bookedAccommodations = response,
      error: error => console.log(error)
    });
  }

  getAccommodationStatuses() {
    this.fohService.getAccommodationStatuses().subscribe({
      next: response => this.accommodationStatuses = [{ accommodationStatusId: 0, statusName: 'All' , isActive: false}, ...response],
      error: error => console.log(error)
    });
  }

  getAccommodationTypes() {
    this.fohService.getAccommodationTypes().subscribe({
      next: response => this.accommodationTypes = [{ accommodationTypeId: 0, typeName: 'All' , maxOccupants: 0, compatibilityValue: 0, isActive: false}, ...response],
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

  showAccommodationInfoNeedsMaintenance(accommodation: AccommodationStatusTypeMaintenanceLogWaitingList) {
    var maintenanceLogDetails = accommodation.lastMaintenanceLog?.maintenanceLogDetails;
    const taskNames = maintenanceLogDetails?.map(detail => detail.taskName);
    const info = 'Out of service for ' + taskNames?.join(', ');
    this.selectedAccommodationInfo = info;
  }

  showAccommodationInfoNeedsCleaning(accommodation: AccommodationStatusTypeMaintenanceLogWaitingList) {
    const info = 'Needs cleaning';
    this.selectedAccommodationInfo = info;
  }

  showEmployeeAccommodationInfo(accommodation: AccommodationStatusTypeMaintenanceLogWaitingList) {
    var info = 'Employee ' + accommodation.typeName + ': #' + accommodation.number + ' Floor' + ': #' + accommodation.floor;
    this.selectedAccommodationInfo = info;
  }

  onAccommodationTypeSelected(event: Event) {
    const target = event.target as HTMLSelectElement;
    const accommodationTypeId = Number(target.value);
    this.accommodationTypeIdSelected = accommodationTypeId;
    this.getAccommodations();
  }

  onAccommodationStatusSelected(event: Event) {
    const target = event.target as HTMLSelectElement;
    const accommodationStatusId = Number(target.value);
    this.accommodationStatusIdSelected = accommodationStatusId;
    this.getAccommodations();
  }

  openCurrentActivityListPopup(bookedAccommodations: BookedAccommodation[]) {
    this.matDialog.open(CurrentActivityListComponent, {
      data: bookedAccommodations,
    });
  }

  openWaitingListPopup() {
    this.matDialog.open(WaitingListComponent, {
      data: this.activeWaitingListAccommodations,
    });
  }

  openOutOfServiceListPopup(outOfServiceAccommodations: AccommodationStatusTypeMaintenanceLogWaitingList[]) {
    this.matDialog.open(OutOfServiceListComponent, {
      data: outOfServiceAccommodations,
    });
  }

  openClearOutOfServicePopup(accommodation: AccommodationStatusTypeMaintenanceLogWaitingList) {
    this.matDialog.open(ClearOutOfServiceComponent, {
      data: accommodation,
    });
  }

  openCleaningCompletePopup(accommodation: AccommodationStatusTypeMaintenanceLogWaitingList) {
    this.matDialog.open(CleaningCompleteComponent, {
      data: accommodation,
    });
  }

  openSystemProblemPopup() {
    this.matDialog.open(SystemProblemComponent, {
    });
  }

  // Cashier Menu
  openCashDropDrawerPopup() {
    this.matDialog.open(CashDropDrawerSkimsComponent, {
    });
  }

  openEndOfShiftPopup() {
    this.matDialog.open(EndShiftComponent, {
    });
  }

  // Managers Menu
  openLogOffPopup() {
    this.accountService.setIsCurrentlyInUseFalse().subscribe({
      next: response => {
        console.log(response);
        var isShiftLoggedIn = this.accountService.checkIfShiftLoggedIn();
        if(isShiftLoggedIn == false){
          this.router.navigateByUrl('/dashboard');
        }
      },
      error: error => console.log(error)
    });
  }
}
