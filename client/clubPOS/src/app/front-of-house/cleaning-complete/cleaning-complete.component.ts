import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AccommodationStatusTypeMaintenanceLogWaitingList } from 'app/shared/models/accommodationWithTypeAndStatus';
import { FrontOfHouseService } from '../front-of-house.service';
import { Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { CleaningTask } from 'app/shared/models/cleaningTask';
import { User } from 'app/shared/models/user';
import { AccountService } from 'app/account/account.service';
import { mapApiResponse } from 'app/shared/models/responseStatus/response';
import { ToastrService } from 'ngx-toastr';



@Component({
  selector: 'app-cleaning-complete',
  templateUrl: './cleaning-complete.component.html',
  styleUrls: ['./cleaning-complete.component.scss']
})
export class CleaningCompleteComponent implements OnInit {
  loading = false;
  accommodationWithTypeAndStatus?: AccommodationStatusTypeMaintenanceLogWaitingList;
  cleaningTasks: CleaningTask[] = [];
  cleaningTaskIdsToResolve: number[] = [];
  newAccommodationStatusId: number = 4;
  currentUser: User | null = null;

  cleaningCompletionForm = this.fb.group({
    comment: ['']
  });

  constructor(@Inject(MAT_DIALOG_DATA) public data: AccommodationStatusTypeMaintenanceLogWaitingList,
    private matDialogRef: MatDialogRef<CleaningCompleteComponent>, private fohService: FrontOfHouseService,
    private router: Router, private fb: FormBuilder, private accountService: AccountService, public toastr: ToastrService) {
    this.accommodationWithTypeAndStatus = data;
  }

  ngOnInit(): void {
    this.getCleaningTasks();
    this.loadCurrentUser();
  }

  loadCurrentUser() {
    this.accountService.currentUser$.subscribe((user: User | null) => {
      this.currentUser = user;
    });
  }

  closePopup() {
    this.matDialogRef.close();
  }

  getCleaningTasks() {
    this.fohService.getCleaningTasks().subscribe({
      next: response => this.cleaningTasks = response,
      error: error => console.log(error)
    });
  }

  onIncompleteCleaningTaskClick(cleaningTask: CleaningTask) {
    const taskId = cleaningTask.cleaningTaskId;
    this.cleaningTaskIdsToResolve = [...this.cleaningTaskIdsToResolve, taskId];
  }

  onSubmit() {
    this.loading = true;
    const accommodationId = this.accommodationWithTypeAndStatus?.accommodationId;
    if (this.cleaningTaskIdsToResolve.length === this.cleaningTasks.length) {
      this.newAccommodationStatusId = 5;
    }
    const newAccommodationStatusId = this.newAccommodationStatusId;
    const cleaningTaskIds = this.cleaningTaskIdsToResolve;
    const comment = this.cleaningCompletionForm.value.comment;
    const userId = this.currentUser?.userId;
    const needsMaintenance = false;

    const requestBody = {
      accommodationId,
      newAccommodationStatusId,
      cleaningTaskIds,
      comment,
      userId,
      needsMaintenance
    };

    if (accommodationId) {
      this.fohService.createCleaningLog(requestBody)
        .subscribe({
          next:  apiResponse => {
            const response = mapApiResponse<null>(apiResponse);
            if(response.status.toString() === "Success")
            {
              this.closePopup();
              this.loading = false;
              this.toastr.success(response.message, "Success");
              this.router.navigateByUrl('/accommodations');
              window.location.reload();
            }
            else{
              this.toastr.error(response.message, "Failed");
              this.loading = false;
            }
          },
          error: error => {
            this.toastr.error("Failed to add waiting list", "Failed");
            this.loading = false;
            console.error('Failed to add waiting list', error);
          }
        });
    }
  }
}