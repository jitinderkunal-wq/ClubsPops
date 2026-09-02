import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { TransferAlertComponent } from '../transfer-alert/transfer-alert.component';
import { WaitingListAlertComponent } from '../waiting-list-alert/waiting-list-alert.component';
import { AddToWaitingListComponent } from '../add-to-waiting-list/add-to-waiting-list.component';
import { ActivatedRoute, Router } from '@angular/router';
import { FrontOfHouseService } from '../front-of-house.service';
import { TransferComponent } from '../transfer/transfer.component'
import { ConfirmationDialogService } from 'app/shared/confirmationDialogService/confirmationDialogService';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-booked-accommodation',
  templateUrl: './booked-accommodation.component.html',
  styleUrls: ['./booked-accommodation.component.scss']
})
export class BookedAccommodationComponent implements OnInit {
  accommodationId: number = 0;
  bookedAccommodationDetail: any;
  accommodationNumber: string = '';
  accommodationType: string = '';
  hoursOfStay: number = 0;
  rateHours: number = 0;
  checkInTime: string = '';
  checkOutTime: string = '';
  totalDepositCollected: number = 0;
  depositsTransferred: number = 0;
  depositsReturned: number = 0;
  depositsBalance: number = 0;
  changeDue: number = 0;
  comment: string = "";
  loading = false;

  constructor(private matDialog: MatDialog, private router: Router, private activatedRoute: ActivatedRoute, private frontOfHouseService: FrontOfHouseService, private confirmationDialogService: ConfirmationDialogService) { }

  ngOnInit() {
    const accommodationId = this.activatedRoute.snapshot.paramMap.get('accommodationId');
    if (accommodationId) {
      this.accommodationId = +accommodationId;
    }
    this.getDetails();
  }

  refundAmount(){
    const id = this.activatedRoute.snapshot.paramMap.get('accommodationId');
    if (id) {
      this.confirmationDialogService.showConfirmationDialog("Are you sure you want to refund for this accommodation?")
        .then((confirmed) => {
          if (confirmed) {
            this.frontOfHouseService.refundAmount(+id).subscribe({
              next: res => {
                console.log(res);
                if (res == true) {
                  this.router.navigate(['/accommodations']);
                }
                else {
                  Swal.fire('Unable to refund', '', 'error');
                }
              },
              error: err => console.log(err)
            });
          }
        });
    }
  }

  renewBooking() {
    const id = this.activatedRoute.snapshot.paramMap.get('accommodationId');
    if (id) {
      this.confirmationDialogService.showConfirmationDialog("Are you sure you want to renew this accommodation?")
        .then((confirmed) => {
          if (confirmed) {
            this.frontOfHouseService.checkOut(+id).subscribe({
              next: res => {
                console.log(res);
                this.router.navigate(['/accommodations', id]);
              },
              error: err => console.log(err)
            });
          }
        });
    }
  }

  openTransferPopup() {
    this.matDialog.open(TransferComponent, {
      data: this.accommodationId
    });
  }

  getDetails() {
    this.frontOfHouseService.getBookedAccommodationDetail(this.accommodationId).subscribe({
      next: response => {
        this.bookedAccommodationDetail = response;
        this.accommodationNumber = this.bookedAccommodationDetail.accommodationNumber,
          this.accommodationType = this.bookedAccommodationDetail.accommodationType,
          this.hoursOfStay = this.bookedAccommodationDetail.hoursOfStay,
          this.rateHours = this.bookedAccommodationDetail.rateHours,
          this.checkInTime = this.bookedAccommodationDetail.checkInTime,
          this.checkOutTime = this.bookedAccommodationDetail.checkOutTime,
          this.totalDepositCollected = this.bookedAccommodationDetail.totalDepositCollected,
          this.depositsTransferred = this.bookedAccommodationDetail.depositsTransferred,
          this.depositsReturned = this.bookedAccommodationDetail.depositsReturned,
          this.depositsBalance = this.bookedAccommodationDetail.depositsBalance,
          this.changeDue = this.bookedAccommodationDetail.changeDue,
          this.comment = this.bookedAccommodationDetail.comment,

        console.log(this.bookedAccommodationDetail);
      },
      error: error => console.log(error)
    });

  }

  completeCheckout() {
    const accommodationId = this.activatedRoute.snapshot.paramMap.get('accommodationId');
    if (accommodationId) {
      this.frontOfHouseService.checkOut(+accommodationId).subscribe({
        next: () => {
          this.router.navigateByUrl('/accommodations');
        },
        error: error => console.log(error)
      });
    }
  }

  openTransferAlertPopup() {
    this.matDialog.open(TransferAlertComponent, {
    });
  }

  openWaitingListAlertPopup() {
    this.matDialog.open(WaitingListAlertComponent, {
    });
  }

  openAddToWaitingListPopup() {
    this.matDialog.open(AddToWaitingListComponent, {
      data: this.accommodationId
    });
  }
}