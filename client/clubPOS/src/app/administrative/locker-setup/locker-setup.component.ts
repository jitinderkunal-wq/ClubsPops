import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { AdministrativeService } from '../administrative.service';
import { FormBuilder, Validators } from '@angular/forms';
import { AccommodationStatusTypeMaintenanceLogWaitingList } from 'app/shared/models/accommodationWithTypeAndStatus';

@Component({
  selector: 'app-locker-setup',
  templateUrl: './locker-setup.component.html',
  styleUrls: ['./locker-setup.component.scss']
})
export class LockerSetupComponent implements OnInit {
  lockers: AccommodationStatusTypeMaintenanceLogWaitingList[] = [];
  selectedLocker?: AccommodationStatusTypeMaintenanceLogWaitingList;
  accommodationStatus = {
    "accommodationStatusId": 5,
    "statusName": "Available"
  };
  accommodationType = {
    "accommodationTypeId": 1,
    "typeName": "Locker"
  };
  saveButtonText: string = 'Create';
  convertToEmployeeLockerButtonText = 'Convert to Employee Locker';
  showConvertToEmployeeLockerButton = false;
  showConvertToEmployeeLockerButtonDisabled = false;
  errors = '';

  constructor(private matDialogRef: MatDialogRef<LockerSetupComponent>, private administrativeService: AdministrativeService, private fb: FormBuilder) { }

  lockerSetupForm = this.fb.group({
    accommodationId: [0],
    accommodationTypeId: [this.accommodationType.accommodationTypeId, Validators.required],
    accommodationStatusId: [this.accommodationStatus.accommodationStatusId],
    number: ['', Validators.required],
    floor: [0, Validators.required],
    deActivate: [false],
    requiresCreditCard: [false],
    hasMaintenanceReservation: [false],
    employeeLocker: [null],
    isGhost: [false],
    hasGloryHole: [false],
  });

  ngOnInit(): void {
    this.getLockers();
  }

  closePopup() {
    this.matDialogRef.close();
  }

  getLockers() {
    this.administrativeService.getAccommodations(this.accommodationType.accommodationTypeId).subscribe({
      next: response => this.lockers = response,
      error: error => console.log(error)
    });
  }

  onConvertToEmployeeLockerButtonClick() {
    if (this.selectedLocker?.accommodationStatusId === 5) {
      this.administrativeService.editAccommodationStatus(this.selectedLocker.accommodationId, 7).subscribe({
        next: () => {
          console.log('Locker converted to employee locker');
        },
        error: error => console.log(error)
      });
    } else if (this.selectedLocker?.accommodationStatusId === 7) {
      this.administrativeService.editAccommodationStatus(this.selectedLocker.accommodationId, 5).subscribe({
        next: () => {
          console.log('Locker removed from employee locker');
        },
        error: error => console.log(error)
      });
    }
    this.closePopup();
  }

  onLockerChange(selectedLocker: any) {
    // console.log(selectedLocker);

    if (selectedLocker) {
      this.selectedLocker = selectedLocker;
      this.saveButtonText = 'Edit';
      this.showConvertToEmployeeLockerButton = true;
      if (selectedLocker.accommodationStatusId === 5) {
        this.showConvertToEmployeeLockerButtonDisabled = false;
        this.convertToEmployeeLockerButtonText = 'Convert to Employee Locker';
      }
      else if (selectedLocker.accommodationStatusId === 7) {
        this.showConvertToEmployeeLockerButtonDisabled = false;
        this.convertToEmployeeLockerButtonText = 'Remove from Employee Locker';
      }
      else {
        this.showConvertToEmployeeLockerButtonDisabled = true;
        this.convertToEmployeeLockerButtonText = 'Can not convert to employee locker';
      }
    }

    this.accommodationStatus.accommodationStatusId = selectedLocker.accommodationStatusId;
    this.accommodationStatus.statusName = selectedLocker.statusName;

    this.accommodationType.accommodationTypeId = selectedLocker.accommodationTypeId;
    this.accommodationType.typeName = selectedLocker.typeName;

    this.lockerSetupForm.patchValue({
      accommodationId: selectedLocker.accommodationId,
      accommodationTypeId: selectedLocker.accommodationTypeId,
      accommodationStatusId: selectedLocker.accommodationStatusId,
      number: selectedLocker.number,
      floor: selectedLocker.floor,
      deActivate: selectedLocker.deActivate,
      requiresCreditCard: selectedLocker.requiresCreditCard,
      hasMaintenanceReservation: selectedLocker.hasMaintenanceReservation,
      employeeLocker: selectedLocker.employeeLocker,
      isGhost: selectedLocker.isGhost,
      hasGloryHole: selectedLocker.hasGloryHole,
    });
  }

  onSubmit() {
    if (this.lockerSetupForm.value.accommodationId) {
      // console.log(this.lockerSetupForm.value);
      // console.log("Edit locker");

      this.administrativeService.editAccommodation(this.lockerSetupForm.value).subscribe({
        next: () => {
          this.lockerSetupForm.reset();
          this.closePopup();
        },
        error: error => {
          console.log(error);
          this.errors = error.errors
        }
      });
    } else {
      // console.log(this.lockerSetupForm.value);
      // console.log("Create locker");

      this.administrativeService.createAccommodation(this.lockerSetupForm.value).subscribe({
        next: () => {
          this.lockerSetupForm.reset();
          this.closePopup();
        },
        error: error => {
          console.log(error);
          this.errors = error.errors
        }
      });
    }
  }
}