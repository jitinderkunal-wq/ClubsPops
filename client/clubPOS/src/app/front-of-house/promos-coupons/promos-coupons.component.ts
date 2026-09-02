import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Coupon } from 'app/shared/models/coupon';

@Component({
  selector: 'app-promos-coupons',
  templateUrl: './promos-coupons.component.html',
  styleUrls: ['./promos-coupons.component.scss']
})
export class PromosCouponsComponent {
  coupons: Coupon[] = [];
  promoPasses: Coupon[] = [];
  requiresCoupons: Coupon[] = [];
  couponId?: number | null;
  promopassId?: number | null;
  flatRate: number = 0;
  discountDollars: number = 0;
  discountPercent: number = 0;

  constructor(@Inject(MAT_DIALOG_DATA) public data: Coupon[], private matDialogRef: MatDialogRef<PromosCouponsComponent>) {
    this.coupons = data;
    this.promoPasses = this.coupons.filter(c => c.isCoupon === false);
    this.requiresCoupons = this.coupons.filter(c => c.isCoupon === true);
  }

  closePopup() {
    this.matDialogRef.close();
  }

  submitDiscount(val: Coupon) {
    this.couponId = null;
    this.promopassId = null;
    if (val.isCoupon) {
      this.couponId = val.couponId;
      if (this.couponId === 2) {
        this.discountPercent = val.discountPercent;
      } else if (this.couponId === 4) {
        this.flatRate = val.flatRate;
      }
    } else {
      this.promopassId = val.couponId;
      if (this.promopassId === 1) {
        this.discountPercent = val.discountPercent;
      } else if (this.promopassId === 3) {
        this.discountDollars = val.discountDollars;
      }
    }
    const data = {
      couponId: this.couponId,
      promopassId: this.promopassId,
      flatRate: this.flatRate,
      discountDollars: this.discountDollars,
      discountPercent: this.discountPercent
    };
    this.matDialogRef.close(data);
  }
}