import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { AdministrativeService } from '../administrative.service';
import { FormBuilder, Validators } from '@angular/forms';
import { Coupon, CouponFormModel } from 'app/shared/models/coupon';
import { DatePipe } from '@angular/common';
import { AccommodationType } from 'app/shared/models/accommodationType';

@Component({
  selector: 'app-promo-passes',
  templateUrl: './promo-passes.component.html',
  styleUrls: ['./promo-passes.component.scss']
})
export class PromoPassesComponent implements OnInit {
  coupons: Coupon[] = [];
  accommodationTypes: AccommodationType[] = [];
  saveButtonText: string = 'Create';
  errors: string[] | null = null;
  currentDate = new Date();
  dateCreated = this.datePipe.transform(this.currentDate.toString(), 'yyyy-MM-dd');

  constructor(private matDialogRef: MatDialogRef<PromoPassesComponent>, private administrativeService: AdministrativeService, private fb: FormBuilder, private datePipe: DatePipe) { }

  couponForm = this.fb.group(new CouponFormModel(0, null, this.dateCreated!, '', '', 0, 0, 0, 0, null, null, true, true, false, false));

  ngOnInit(): void {
    this.getAccommodationTypes();
    this.getCoupons();
  }

  closePopup() {
    this.matDialogRef.close();
  }

  getAccommodationTypes() {
    this.administrativeService.getAccommodationTypes().subscribe({
      next: response => this.accommodationTypes = [{ accommodationTypeId: null, typeName: 'All' , maxOccupants: 0, compatibilityValue: 0, isActive: false}, ...response],
      error: error => console.log(error)
    });
  }

  getCoupons() {
    this.administrativeService.getCoupons().subscribe({
      next: response => this.coupons = response,
      error: error => console.log(error)
    });
  }

  onAccommodationTypeChange(selectedAccommodationType: any) {
    // console.log(selectedAccommodationType);

    this.couponForm.patchValue({
      accommodationTypeId: selectedAccommodationType.accommodationTypeId ? parseInt(selectedAccommodationType.accommodationTypeId, 10) : selectedAccommodationType.accommodationTypeId
    });
  }

  onCouponChange(selectedCoupon: any) {
    // console.log(selectedCoupon);
    this.saveButtonText = 'Edit';

    const formattedDateCreated = this.datePipe.transform(selectedCoupon.dateCreated, 'yyyy-MM-dd');
    this.dateCreated = formattedDateCreated;
    const formattedStartDate = selectedCoupon.startDate ? this.datePipe.transform(selectedCoupon.startDate, 'yyyy-MM-dd') : null;
    const formattedExpiryDate = selectedCoupon.expiryDate ? this.datePipe.transform(selectedCoupon.expiryDate, 'yyyy-MM-dd') : null;

    this.couponForm.patchValue({
      couponId: selectedCoupon.couponId,
      accommodationTypeId: selectedCoupon.accommodationTypeId,
      dateCreated: formattedDateCreated,
      couponName: selectedCoupon.couponName,
      description: selectedCoupon.description,
      couponNumber: selectedCoupon.couponNumber,
      flatRate: selectedCoupon.flatRate,
      discountDollars: selectedCoupon.discountDollars,
      discountPercent: selectedCoupon.discountPercent,
      startDate: formattedStartDate,
      expiryDate: formattedExpiryDate,
      isExclusive: selectedCoupon.isExclusive,
      isActive: selectedCoupon.isActive,
      isCoupon: selectedCoupon.isCoupon,
      returnOnCheckOut: selectedCoupon.returnOnCheckOut,
    });
  }

  onSubmit() {
    if (this.couponForm.value.couponId) {
      // console.log(this.couponForm.value);
      // console.log("Coupon edit");

      this.administrativeService.editCoupon(this.couponForm.value).subscribe({
        next: () => {
          this.couponForm.reset();
          this.closePopup();
        },
        error: error => {
          console.log(error);
          this.errors = error.errors
        }
      });
    } else {
      // console.log(this.couponForm.value);
      // console.log("Coupon create");

      this.administrativeService.createCoupon(this.couponForm.value).subscribe({
        next: () => {
          this.couponForm.reset();
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