import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AccommodationStatusTypeMaintenanceLogWaitingList } from 'app/shared/models/accommodationWithTypeAndStatus';
import { MaintenanceLogDetail } from 'app/shared/models/maintenanceLogDetail';
import { FrontOfHouseService } from '../front-of-house.service';
import { Router } from '@angular/router';
import { User } from 'app/shared/models/user';
import { AccountService } from 'app/account/account.service';
import { mapApiResponse } from 'app/shared/models/responseStatus/response';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-clear-out-of-service',
  templateUrl: './clear-out-of-service.component.html',
  styleUrls: ['./clear-out-of-service.component.scss']
})
export class ClearOutOfServiceComponent implements OnInit {
  loading = false;
  accommodationWithTypeAndStatus?: AccommodationStatusTypeMaintenanceLogWaitingList;
  maintenanceLogDetailsNotResolved?: MaintenanceLogDetail[] = [];
  maintenanceTaskIdsNotResolved: number[] = [];
  maintenanceTaskIdsToResolve: number[] = [];
  newAccommodationStatusId: number = 3;
  currentUser: User | null = null;

  constructor(@Inject(MAT_DIALOG_DATA) public data: AccommodationStatusTypeMaintenanceLogWaitingList,
    private matDialogRef: MatDialogRef<ClearOutOfServiceComponent>, private fohService: FrontOfHouseService,
    private router: Router, private fb: FormBuilder, private accountService: AccountService, public toastr: ToastrService) {
    this.accommodationWithTypeAndStatus = data;
    this.maintenanceLogDetailsNotResolved = this.accommodationWithTypeAndStatus?.lastMaintenanceLog?.maintenanceLogDetails.filter(mld => mld.isResolved === false);
    if (this.maintenanceLogDetailsNotResolved) {
      this.maintenanceTaskIdsNotResolved = this.maintenanceLogDetailsNotResolved?.map(mldnr => mldnr.maintenanceTaskId);
    }
  }

  ngOnInit(): void {
    this.loadCurrentUser();
  }

  loadCurrentUser() {
    this.accountService.currentUser$.subscribe((user: User | null) => {
      this.currentUser = user;
    });
  }

  clearMaintenanceLogForm = this.fb.group({
    resolveComment: ['']
  });

  refresh() {
    window.location.reload();
  }

  closePopup() {
    this.matDialogRef.close();
  }

  onMaintenanceTaskNotResolvedClick(maintenanceLogDetail: MaintenanceLogDetail) {
    const taskId = maintenanceLogDetail.maintenanceTaskId;
    this.maintenanceTaskIdsToResolve = [...this.maintenanceTaskIdsToResolve, taskId];
  }

  onSubmit() {
    this.loading = true;
    const accommodationId = this.accommodationWithTypeAndStatus?.accommodationId;
    if (this.maintenanceTaskIdsToResolve.length === this.maintenanceTaskIdsNotResolved.length) {
      this.newAccommodationStatusId = 5;
    }
    const newAccommodationStatusId = this.newAccommodationStatusId;
    const maintenanceTaskIds = this.maintenanceTaskIdsToResolve;
    const resolvedComment = this.clearMaintenanceLogForm.value.resolveComment;
    const resolvedUserId = this.currentUser?.userId;
    const needsCleaning = false;

    const requestBody = {
      accommodationId,
      newAccommodationStatusId,
      maintenanceTaskIds,
      resolvedComment,
      resolvedUserId,
      needsCleaning
    };

    if (accommodationId) {
      this.fohService.editMaintenanceLog(requestBody)
      .subscribe({
        next:  apiResponse => {
          const response = mapApiResponse<null>(apiResponse);
          if(response.status.toString() === "Success")
          {
            this.closePopup();
            this.loading = false;
            this.toastr.success(response.message, "Success");
            // Refresh the page
            this.refresh();
          }
          else{
            this.toastr.error(response.message, "Failed");
            this.loading = false;
          }
        },
        error: error => {
          this.toastr.error("Update maintenance log failed", "Failed");
          this.loading = false;
          console.error('Update maintenance log failed', error);
        }
      });
    }
  }
}