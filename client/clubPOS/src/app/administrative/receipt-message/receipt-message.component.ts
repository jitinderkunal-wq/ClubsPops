import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AdministrativeService } from '../administrative.service'

@Component({
  selector: 'app-receipt-message',
  templateUrl: './receipt-message.component.html',
  styleUrls: ['./receipt-message.component.scss']
})
export class ReceiptMessageComponent {

  constructor(private matDialogRef: MatDialogRef<ReceiptMessageComponent>, private administrativeService: AdministrativeService) { }

  receiptMessagesAndCouponForm = new FormGroup({
    address: new FormControl(null),
    gstNumber: new FormControl(null),
    disclaimer: new FormControl('', [Validators.required]),
    message1: new FormControl('', [Validators.required]),
    message2: new FormControl(null),
    couponText: new FormControl(null)
  })

  onSubmit() {
    if (this.receiptMessagesAndCouponForm.valid) {
      this.administrativeService.createReceiptMessagesAndCoupon(this.receiptMessagesAndCouponForm.value).subscribe({
        error: error => console.log(error)
      })
      this.closePopup();
    }
  }

  closePopup() {
    this.matDialogRef.close();
  }
}