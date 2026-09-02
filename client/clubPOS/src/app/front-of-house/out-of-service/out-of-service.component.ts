import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FrontOfHouseService } from '../front-of-house.service';
import { MaintenanceTask } from 'app/shared/models/maintenanceTask';
import { Router } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';
import { AccommodationStatusTypeMaintenanceRateAndCoupons } from 'app/shared/models/accommodationStatusTypeMaintenanceRateAndCoupons';

@Component({
  selector: 'app-out-of-service',
  templateUrl: './out-of-service.component.html',
  styleUrls: ['./out-of-service.component.scss']
})
export class OutOfServiceComponent implements OnInit {
  maintenanceTasks: MaintenanceTask[] = [];
  accommodation?: AccommodationStatusTypeMaintenanceRateAndCoupons;
  loading = false;
  maintenanceTaskIds: number[] = [];
  isMaintenanceTaskSelected: boolean = false; 

  createMaintenanceLogForm = this.fb.group({
    comment: ['']
  });

  constructor(@Inject(MAT_DIALOG_DATA) public data: AccommodationStatusTypeMaintenanceRateAndCoupons, private matDialogRef: MatDialogRef<OutOfServiceComponent>,
    private fohService: FrontOfHouseService, private router: Router, private fb: FormBuilder) {
    this.accommodation = data;
  }

  ngOnInit(): void {
    this.getMaintenanceTasks();
  }

  closePopup() {
    this.matDialogRef.close();
  }

  getMaintenanceTasks() {
    this.fohService.getMaintenanceTasks().subscribe({
      next: response => this.maintenanceTasks = response,
      error: error => console.log(error)
    });
  }

  onMaintenanceTaskClick(maintenanceTask: MaintenanceTask) {
    const taskId = maintenanceTask.maintenanceTaskId;

    if (this.maintenanceTaskIds.includes(taskId)) {
      this.maintenanceTaskIds = this.maintenanceTaskIds.filter(id => id !== taskId);
    } else {
      this.maintenanceTaskIds = [...this.maintenanceTaskIds, taskId];
    }
    // Check if any task is selected
    if (this.maintenanceTaskIds.length > 0) {
      // If yes, set selectedMaintenanceTaskId to true
      this.isMaintenanceTaskSelected = true;
    } else {
      // If not, set selectedMaintenanceTaskId to false
      this.isMaintenanceTaskSelected = false;
    }
    // console.log(this.maintenanceTaskIds);
  }

  onSubmit() {
    this.loading = true;
    const accommodationId = this.accommodation?.accommodationId;
    const newAccommodationStatusId = 3;
    const comment = this.createMaintenanceLogForm.value.comment;
    const maintenanceTaskIds = this.maintenanceTaskIds;

    const requestBody = {
      accommodationId,
      newAccommodationStatusId,
      maintenanceTaskIds,
      comment
    };

    if (accommodationId) {
      this.fohService.createMaintenanceLog(requestBody).subscribe({
        next: response => {
          console.log('Accommodation status updated successfully to needs maintainance.', response);
          this.loading = false;
          this.router.navigateByUrl('/accommodations');
          this.closePopup();
        },
        error: error => {
          console.error('Failed to update accommodation status.', error);
          this.loading = false;
        }
      });
    }
  }
}