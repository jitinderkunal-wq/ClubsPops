import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { AdministrativeService } from '../administrative.service';
import { FormBuilder, Validators } from '@angular/forms';
import { MembershipType } from 'app/shared/models/membershipType';
import { Membership } from 'app/shared/models/membership';
import { CreditCardType } from 'app/shared/models/creditCardType';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.scss']
})
export class MembersComponent implements OnInit {
  creditCardTypes: CreditCardType[] = [];
  membershipTypes: MembershipType[] = [];
  memberships: Membership[] = [];
  saveButtonText: string = 'Create';
  isCreditCard: boolean = false;
  errors: string[] | null = null;

  constructor(private matDialogRef: MatDialogRef<MembersComponent>, private administrativeService: AdministrativeService,
    private fb: FormBuilder, private datePipe: DatePipe) { }

  ngOnInit(): void {
    this.getCreditCardTypes();
    this.getMembershipTypes();
    this.getMemberships();
  }

  membershipForm = this.fb.group({
    membershipId: [0],
    creditCardTypeId: [null],
    membershipTypeId: [null, Validators.required],
    membershipNum: [0],
    firstName: ['', Validators.required],
    lastName: [null],
    address: [null],
    phone: [null],
    city: [null],
    province: [null],
    country: [null],
    postalZipCode: [null],
    membershipStartDate: ['', Validators.required],
    membershipEndDate: [null],
    creditCardNumber: [null],
    creditCardExpiry: [null],
    csv: [null],
    isActive: [true],
  });

  closePopup() {
    this.matDialogRef.close();
  }

  getCreditCardTypes() {
    this.administrativeService.getCreditCardTypes().subscribe({
      next: response => this.creditCardTypes = [{ creditCardTypeId: 0, creditCardTypeName: 'No Card' }, ...response],
      error: error => console.log(error)
    });
  }

  getMembershipTypes() {
    this.administrativeService.getMembershipTypes().subscribe({
      next: response => this.membershipTypes = response,
      error: error => console.log(error)
    });
  }

  getMemberships() {
    this.administrativeService.getMemberships().subscribe({
      next: response => this.memberships = response,
      error: error => console.log(error)
    });
  }

  onCreditCardTypeChange(selectedCreditCardType: any) {
    // console.log(selectedCreditCardType);

    var creditCardTypeId: any;
    if (selectedCreditCardType.creditCardTypeId === 0) {
      creditCardTypeId = null;
      // creditCardTypeId = parseInt(selectedCreditCardType.creditCardTypeId, 10)
    } else {
      creditCardTypeId = selectedCreditCardType.creditCardTypeId
    }
    this.membershipForm.patchValue({
      creditCardTypeId: creditCardTypeId
    });
  }

  onMembershipTypeChange(selectedMembershipType: any) {
    // console.log(selectedMembershipType);

    this.membershipForm.patchValue({
      membershipTypeId: selectedMembershipType.membershipTypeId ? parseInt(selectedMembershipType.membershipTypeId, 10) : selectedMembershipType.membershipTypeId
    });
    selectedMembershipType.membershipTypeId == 3 ? this.isCreditCard = true : this.isCreditCard = false;
  }

  onMembershipChange(selectedMembership: any) {
    // console.log(selectedMembership);
    this.saveButtonText = 'Edit';

    selectedMembership.membershipTypeId == 3 ? this.isCreditCard = true : this.isCreditCard = false;

    // Format the date before patching it into the form
    const formattedStartDate = this.datePipe.transform(selectedMembership.membershipStartDate, 'yyyy-MM-dd');

    this.membershipForm.patchValue({
      membershipId: selectedMembership.membershipId,
      creditCardTypeId: selectedMembership.creditCardTypeId,
      membershipTypeId: selectedMembership.membershipTypeId,
      membershipNum: selectedMembership.membershipNum,
      firstName: selectedMembership.firstName,
      lastName: selectedMembership.lastName,
      address: selectedMembership.address,
      phone: selectedMembership.phone,
      city: selectedMembership.city,
      province: selectedMembership.province,
      country: selectedMembership.country,
      postalZipCode: selectedMembership.postalZipCode,
      membershipStartDate: formattedStartDate, // Use the formatted date here
      membershipEndDate: selectedMembership.membershipEndDate ? this.datePipe.transform(selectedMembership.membershipEndDate, 'yyyy-MM-dd') : selectedMembership.membershipEndDate,
      creditCardNumber: selectedMembership.creditCardNumber,
      creditCardExpiry: selectedMembership.creditCardExpiry ? this.datePipe.transform(selectedMembership.creditCardExpiry, 'yyyy-MM-dd') : selectedMembership.creditCardExpiry,
      csv: selectedMembership.csv,
      isActive: selectedMembership.isActive,
    });
  }

  onSubmit() {
    if (this.membershipForm.value.membershipId) {
      // console.log(this.membershipForm.value);

      this.administrativeService.editMembership(this.membershipForm.value).subscribe({
        next: () => {
          this.membershipForm.reset();
          this.closePopup();
        },
        error: error => {
          console.log(error);
          this.errors = error.errors
        }
      });
    } else {
      // console.log(this.membershipForm.value);

      this.administrativeService.createMembership(this.membershipForm.value).subscribe({
        next: () => {
          this.membershipForm.reset();
          this.closePopup();
        },
        error: error => {
          console.log(error);
          this.errors = error.errors
        }
      })
    }
  }
}