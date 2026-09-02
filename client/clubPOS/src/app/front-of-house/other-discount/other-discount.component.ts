import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-other-discount',
  templateUrl: './other-discount.component.html',
  styleUrls: ['./other-discount.component.scss']
})
export class OtherDiscountComponent {
  discountPercentage: number = 0;
  dollarAmount: number = 0;
  discountReason: string = '';

  constructor(private matDialogRef: MatDialogRef<OtherDiscountComponent>) { }

  closePopup() {
    this.matDialogRef.close();
  }

  selectDiscountPercentage() {
    this.dollarAmount = 0;
  }

  selectDollarAmount() {
    this.discountPercentage = 0;
  }

  submitDiscount() {
    const data = {
      discountPercentage: this.discountPercentage,
      dollarAmount: this.dollarAmount,
      discountReason: this.discountReason
    };
    this.matDialogRef.close(data);
  }
}