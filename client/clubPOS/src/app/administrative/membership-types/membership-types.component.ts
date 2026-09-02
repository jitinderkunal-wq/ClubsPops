import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { AdministrativeService } from '../administrative.service';
import { FormBuilder, Validators } from '@angular/forms';
import { MembershipType } from 'app/shared/models/membershipType';

@Component({
  selector: 'app-membership-types',
  templateUrl: './membership-types.component.html',
  styleUrls: ['./membership-types.component.scss']
})
export class MembershipTypesComponent implements OnInit {
  membershipTypes: MembershipType[] = [];
  saveButtonText: string = 'Create';
  errors: string[] | null = null;

  constructor(private matDialogRef: MatDialogRef<MembershipTypesComponent>, private administrativeService: AdministrativeService, private fb: FormBuilder) { }

  membershipTypeForm = this.fb.group({
    membershipTypeId: [0],
    membershipTypeName: ['', Validators.required],
    discountFactor: [0],
    discountAmount: [0],
    depositRequired: [true],
    displayOrder: [0],
    canReserve: [false],
    canTab: [false],
    isActive: [true],
    memberShipPrice: [0],
  });

  ngOnInit(): void {
    this.getMembershipTypes();
  }

  closePopup() {
    this.matDialogRef.close();
  }

  getMembershipTypes() {
    this.administrativeService.getMembershipTypes().subscribe({
      next: response => this.membershipTypes = response,
      error: error => console.log(error)
    });
  }

  onMembershipTypeChange(selectedMembershipType: any) {
    // console.log(selectedMembershipType);
    this.saveButtonText = 'Edit';

    this.membershipTypeForm.patchValue({
      membershipTypeId: selectedMembershipType.membershipTypeId,
      membershipTypeName: selectedMembershipType.membershipTypeName,
      discountFactor: selectedMembershipType.discountFactor,
      discountAmount: selectedMembershipType.discountAmount,
      depositRequired: selectedMembershipType.depositRequired,
      displayOrder: selectedMembershipType.displayOrder,
      canReserve: selectedMembershipType.canReserve,
      canTab: selectedMembershipType.canTab,
      isActive: selectedMembershipType.isActive,
      memberShipPrice: selectedMembershipType.memberShipPrice
    });
  }

  onSubmit() {
    if (this.membershipTypeForm.value.membershipTypeId) {
      // console.log(this.membershipTypeForm.value);
      // console.log("Membership type edit");

      this.administrativeService.editMembershipType(this.membershipTypeForm.value).subscribe({
        next: () => {
          this.membershipTypeForm.reset();
          this.closePopup();
        },
        error: error => {
          console.log(error);
          this.errors = error.errors
        }
      });
    } else {
      // console.log(this.membershipTypeForm.value);
      // console.log("Membership type create");

      this.administrativeService.createMembershipType(this.membershipTypeForm.value).subscribe({
        next: () => {
          this.membershipTypeForm.reset();
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