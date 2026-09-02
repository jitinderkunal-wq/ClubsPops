import { Component, OnInit } from '@angular/core';
import { AdministrativeService } from '../administrative.service';
import { FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { AccommodationStatusTypeMaintenanceLogWaitingList } from 'app/shared/models/accommodationWithTypeAndStatus';
import { AccommodationType } from 'app/shared/models/accommodationType';

@Component({
  selector: 'app-room-setup',
  templateUrl: './room-setup.component.html',
  styleUrls: ['./room-setup.component.scss']
})
export class RoomSetupComponent implements OnInit {
  rooms: AccommodationStatusTypeMaintenanceLogWaitingList[] = [];
  selectedRoom?: AccommodationStatusTypeMaintenanceLogWaitingList;
  accommodationTypes: AccommodationType[] = [];
  accommodationStatus = {
    "accommodationStatusId": 5,
    "statusName": "Available"
  };
  saveButtonText: string = 'Create';
  convertToEmployeeRoomButtonText = 'Convert to Employee Room';
  showConvertToEmployeeRoomButton = false;
  showConvertToEmployeeRoomButtonDisabled = false;
  errors = '';

  constructor(private matDialogRef: MatDialogRef<RoomSetupComponent>, private administrativeService: AdministrativeService, private fb: FormBuilder) { }

  roomSetupForm = this.fb.group({
    accommodationId: [0],
    accommodationTypeId: [2, Validators.required],
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
    this.getRooms();
    this.getAccommodationTypes();
  }

  closePopup() {
    this.matDialogRef.close();
  }

  getAccommodationTypes() {
    this.administrativeService.getAccommodationTypes().subscribe({
      next: response => this.accommodationTypes = response.filter(at => at.accommodationTypeId !== 1),
      error: error => {
        this.errors = error.errors;
        console.log(error);
      }
    });
  }

  getRooms() {
    this.administrativeService.getAccommodations().subscribe({
      next: response => this.rooms = response.filter(a => a.accommodationTypeId !== 1),
      error: error => console.log(error)
    });
  }

  onConvertToEmployeeRoomButtonClick() {
    if (this.selectedRoom?.accommodationStatusId === 5) {
      this.administrativeService.editAccommodationStatus(this.selectedRoom.accommodationId, 7).subscribe({
        next: () => {
          console.log('Room converted to employee room');
        },
        error: error => console.log(error)
      });
    } else if (this.selectedRoom?.accommodationStatusId === 7) {
      this.administrativeService.editAccommodationStatus(this.selectedRoom.accommodationId, 5).subscribe({
        next: () => {
          console.log('Room removed from employee room');
        },
        error: error => console.log(error)
      });
    }
    this.closePopup();
  }

  onAccommodationTypeChange(selectedAccommodationType: any) {
    // console.log(selectedAccommodationType);
  }

  onRoomChange(selectedRoom: any) {
    // console.log(selectedRoom);

    if (selectedRoom) {
      this.selectedRoom = selectedRoom;
      this.saveButtonText = 'Edit';
      this.showConvertToEmployeeRoomButton = true;
      if (selectedRoom.accommodationStatusId === 5) {
        this.showConvertToEmployeeRoomButtonDisabled = false;
        this.convertToEmployeeRoomButtonText = 'Convert to Employee Room';
      }
      else if (selectedRoom.accommodationStatusId === 7) {
        this.showConvertToEmployeeRoomButtonDisabled = false;
        this.convertToEmployeeRoomButtonText = 'Remove from Employee Room';
      }
      else {
        this.showConvertToEmployeeRoomButtonDisabled = true;
        this.convertToEmployeeRoomButtonText = 'Can not convert to employee room';
      }
    }

    this.accommodationStatus.accommodationStatusId = selectedRoom.accommodationStatusId;
    this.accommodationStatus.statusName = selectedRoom.statusName;

    this.roomSetupForm.patchValue({
      accommodationId: selectedRoom.accommodationId,
      accommodationTypeId: selectedRoom.accommodationTypeId,
      accommodationStatusId: selectedRoom.accommodationStatusId,
      number: selectedRoom.number,
      floor: selectedRoom.floor,
      deActivate: selectedRoom.deActivate,
      requiresCreditCard: selectedRoom.requiresCreditCard,
      hasMaintenanceReservation: selectedRoom.hasMaintenanceReservation,
      employeeLocker: selectedRoom.employeeLocker,
      isGhost: selectedRoom.isGhost,
      hasGloryHole: selectedRoom.hasGloryHole,
    });
  }

  onSubmit() {
    if (this.roomSetupForm.value.accommodationId) {
      // console.log(this.roomSetupForm.value);
      // console.log("Edit room");

      this.administrativeService.editAccommodation(this.roomSetupForm.value).subscribe({
        next: () => {
          this.roomSetupForm.reset();
          this.closePopup();
        },
        error: error => {
          console.log(error);
          this.errors = error.errors
        }
      });
    } else {
      // console.log(this.roomSetupForm.value);
      // console.log("Create room");

      this.administrativeService.createAccommodation(this.roomSetupForm.value).subscribe({
        next: () => {
          this.roomSetupForm.reset();
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