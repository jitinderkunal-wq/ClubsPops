import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FrontOfHouseService } from '../front-of-house.service';
import { AccommodationType } from 'app/shared/models/accommodationType';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MembershipType } from 'app/shared/models/membershipType';
import { AccountService } from 'app/account/account.service';
import { Observable } from 'rxjs';
import { User } from 'app/shared/models/user';
import { ToastrService } from 'ngx-toastr';
import { ResponseEnum, mapApiResponse } from 'app/shared/models/responseStatus/response';


@Component({
  selector: 'app-add-to-waiting-list',
  templateUrl: './add-to-waiting-list.component.html',
  styleUrls: ['./add-to-waiting-list.component.scss']
})
export class AddToWaitingListComponent implements OnInit {
  loading = false;
  accommodationId?: number;
  accommodationTypes: AccommodationType[] = [];
  activeMembershipLists: MembershipType[] = [];
  isFormValid: boolean = false;
  currentUser: User | null = null;

  constructor(@Inject(MAT_DIALOG_DATA) public data: number, private matDialogRef: MatDialogRef<AddToWaitingListComponent>,
    private fohService: FrontOfHouseService, private activatedRoute: ActivatedRoute, private router: Router, private fb: FormBuilder,
    public toastr: ToastrService, private accountService: AccountService) {
    this.accommodationId = data;
  }

  createAddToWaitingListForm = this.fb.group({
    membershipTypeId: [0, Validators.required],
    accommodationTypeId: [0],
    phone: new FormControl('', [Validators.required, Validators.maxLength(10)]),
    comment: new FormControl('', Validators.required)
  });

  ngOnInit(): void {
    this.getAccommodationTypes();
    this.getMembershipList();
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

  getMembershipList() {
    this.fohService.getMembershipLists().subscribe({
      next: response => {
        this.activeMembershipLists = [{ membershipTypeId: 0, membershipName: 'None' }, ...response];
      },
      error: error => console.log(error)
    });
  }

  getAccommodationTypes() {
    this.fohService.getAccommodationTypes().subscribe({
      next: response => {
        this.accommodationTypes = response.filter(x => x.accommodationTypeId !== 1);
        this.accommodationTypes = [{ accommodationTypeId: 0, typeName: 'None' , maxOccupants: 0, compatibilityValue: 0, isActive: false}, ...this.accommodationTypes];
      },
      error: error => console.log(error)
    });
  }

  onSubmit() {

    this.loading = true;
    const accommodationId = this.accommodationId;
    const userId = this.currentUser?.userId;
    var accommodationTypeId = null;
    if (this.createAddToWaitingListForm.value.accommodationTypeId !== 0) {
      accommodationTypeId = this.createAddToWaitingListForm.value.accommodationTypeId;
      if (accommodationTypeId) {
        accommodationTypeId = +accommodationTypeId;
      }
    }
    var membershipTypeId = null;
    if (this.createAddToWaitingListForm.value.membershipTypeId !== 0) {
      membershipTypeId = this.createAddToWaitingListForm.value.membershipTypeId;
      if (membershipTypeId) {
        membershipTypeId = +membershipTypeId;
      }
    }
    const phone = String(this.createAddToWaitingListForm.value.phone);
    const comment = this.createAddToWaitingListForm.value.comment;

    if (accommodationTypeId == null || membershipTypeId == null || phone == null || comment == null) {
      this.isFormValid = true;
    }
    const requestBody = {
      accommodationId,
      accommodationTypeId,
      membershipTypeId,
      userId,
      phone,
      comment
    };

    if (accommodationId) {
      this.fohService.createWaitingList(requestBody)
      .subscribe({
        next:  apiResponse => {
          const response = mapApiResponse<null>(apiResponse);
          if(response.status.toString() === "Success")
          {
            this.closePopup();
            this.loading = false;
            this.toastr.success(response.message, "Success");
            this.router.navigateByUrl('/accommodations');
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
