import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AccommodationStatusTypeMaintenanceLogWaitingList } from 'app/shared/models/accommodationWithTypeAndStatus';
import { Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { CleaningTask } from 'app/shared/models/cleaningTask';
import { CleaningService } from '../cleaning.service';
import { AccountService } from 'app/account/account.service';
import { User } from 'app/shared/models/user';



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
    private matDialogRef: MatDialogRef<CleaningCompleteComponent>, private cleaningService: CleaningService,
    private router: Router, private fb: FormBuilder, private accountService: AccountService) {
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
    this.cleaningService.getCleaningTasks().subscribe({
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
    const userId = this.currentUser?.userId;;
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
      this.cleaningService.createCleaningLog(requestBody).subscribe({
        next: response => {
          console.log('Cleaning Log Created', response);
          this.loading = false;
          this.closePopup();

          // Navigate to the cleaning page
          this.router.navigateByUrl('/cleaning').then(() => {
            // Reload the page after navigation
            window.location.reload();
          });
        },
        error: error => {
          console.error('Failed to update accommodation Log', error);
          this.loading = false;
        }
      });
    }
  }
}