import { Component, OnInit } from '@angular/core';
import { FrontOfHouseService } from '../front-of-house.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { OutOfServiceComponent } from '../out-of-service/out-of-service.component';
import { PromosCouponsComponent } from '../promos-coupons/promos-coupons.component';
import { OtherDiscountComponent } from '../other-discount/other-discount.component';
import { FormBuilder, FormControl } from '@angular/forms';
import { AccommodationStatusTypeMaintenanceRateAndCoupons } from 'app/shared/models/accommodationStatusTypeMaintenanceRateAndCoupons';
import { User } from 'app/shared/models/user';
import { AccountService } from 'app/account/account.service';
import { environment } from 'environments/environment';

@Component({
  selector: 'app-book-accommodation',
  templateUrl: './book-accommodation.component.html',
  styleUrls: ['./book-accommodation.component.scss']
})
export class BookAccommodationComponent implements OnInit {
  currentorganizationId: number = 0;
  currentUserName: string = "";
  organizationLogo: string = '';
  organizationName: string = '';
  filePathApi: string = environment.logoPathApi;
  transferAccommodationDetails: any;
  loading = false;
  accommodation?: AccommodationStatusTypeMaintenanceRateAndCoupons;
  accommodationTypeName: string = '';
  accommodationNumber: string = '';
  checkinTime: Date = new Date();
  checkoutTime: Date = new Date(new Date().setHours(new Date().getHours() + 6));;
  netAmount: number = 0;
  otherDiscountDollar: number = 0;
  otherDiscountReason: string = '';
  couponId: number = 0;
  promopassId: number = 0;
  couponsDiscount: number = 0;
  promoDiscount: number = 0;
  totalAmount: number = 0;
  dueAmount: number = 0;
  currentDueAmount: number = 0;
  currentUser: User | null = null;

  enteredValue: string = '';
  tenderedValue: number = 0;
  discoverTendered: number = 0;
  amexTendered: number = 0;
  masterTendered: number = 0;
  visaTendered: number = 0;
  debitTendered: number = 0;
  usTendered: number = 0;
  euroTendered: number = 0;

  constructor(private fohService: FrontOfHouseService, private accountService: AccountService, private activatedRoute: ActivatedRoute, private matDialog: MatDialog, private fb: FormBuilder, private router: Router) {
    this.calculateDueAmount = this.calculateDueAmount.bind(this);
  }

  ngOnInit(): void {
    this.getCurrentUser();
    this.getCurrentOrganization();
    this.getTransferAccommodationDetails();
    this.getAccommodation();
  }

  getCurrentUser() {
    this.accountService.currentUser$.subscribe({
      next: user => {
        this.currentUser = user;
        this.currentorganizationId = user ? user.organizationId : 0;
        this.currentUserName = user ? user.firstName : "No user found";
      },
      error: error => console.log(error)
    });
  }

  getCurrentOrganization() {
    this.fohService.currentOrganization(this.currentorganizationId).subscribe({
      next: response => {
        this.organizationLogo = this.filePathApi + response.logo!;
        this.organizationName = response.organizationName!;
      },
      error: error => console.log(error)
    });
  }

  getTransferAccommodationDetails() {
    const storedData = localStorage.getItem("TransferAccommodationDetails");
    this.transferAccommodationDetails = storedData ? JSON.parse(storedData) : null;
    localStorage.removeItem("TransferAccommodationDetails");
  }

  taxesControl = new FormControl({ value: 0, disabled: true });
  rateTotalControl = new FormControl({ value: 0, disabled: true });


  bookingForm = this.fb.group({
    taxes: this.taxesControl,
    rateTotal: this.rateTotalControl,
    rateDiscount: [{ value: 0, disabled: true }],
    otherDiscount: [{ value: 0, disabled: true }],
    promoOrPass: [{ value: 0, disabled: true }],
    totalDiscount: [{ value: 0, disabled: true }],
    netAmount: [{ value: 0, disabled: true }],                   // rateTotal - totalDiscount
    totalDeposits: [{ value: 0, disabled: true }],
    total: [{ value: 0, disabled: true }],
    tendered: [{ value: 0, disabled: false }],
    amountDue: [{ value: 0, disabled: true }],
    // employeeName: ['testUser'],
    debit: [{ value: 0, disabled: true }],
    amex: [{ value: 0, disabled: true }],
    masterCard: [{ value: 0, disabled: true }],
    visa: [{ value: 0, disabled: true }],
    discoveryCard: [{ value: 0, disabled: true }],
    us: [{ value: 0, disabled: true }],
    euro: [{ value: 0, disabled: true }],
    cash: [0],
    unionPay: [0],
    diners: [0],
    // totalUSDollars: [''],
    // usExchangePaid: [''],
    // usExchangeReceived: [''],
    change: [0],
    comment: ['']
  });

  clickedNumber(num: string) {
    this.currentDueAmount = this.netAmount - this.usTendered - this.euroTendered - this.discoverTendered - this.amexTendered - this.masterTendered - this.visaTendered - this.debitTendered;
    this.enteredValue += num;
    this.tenderedValue = parseInt(this.enteredValue);
    this.calculateDueAmount(this.tenderedValue);
  }

  usFunction() {
    if (this.tenderedValue) {
      this.usTendered = this.tenderedValue;
      this.bookingForm.patchValue({ us: this.usTendered });
    }
    if (this.usTendered && this.tenderedValue == 0) {
      this.recalculateDueAmount(this.usTendered);
      this.usTendered = 0;
    }
    this.enteredValue = '';
    this.tenderedValue = 0;
  }

  euroFunction() {
    if (this.tenderedValue) {
      this.euroTendered = this.tenderedValue;
      this.bookingForm.patchValue({ euro: this.tenderedValue });
    }
    if (this.euroTendered && this.tenderedValue == 0) {
      this.recalculateDueAmount(this.euroTendered);
      this.euroTendered = 0;
    }
    this.enteredValue = '';
    this.tenderedValue = 0;
  }

  discoverFunction() {
    if (this.tenderedValue) {
      this.discoverTendered = this.tenderedValue;
      this.bookingForm.patchValue({ discoveryCard: this.tenderedValue });
    }
    if (this.discoverTendered && this.tenderedValue == 0) {
      this.recalculateDueAmount(this.discoverTendered);
      this.discoverTendered = 0;
    }
    this.enteredValue = '';
    this.tenderedValue = 0;
  }

  amexFunction() {
    if (this.tenderedValue) {
      this.amexTendered = this.tenderedValue;
      this.bookingForm.patchValue({ amex: this.tenderedValue });
    }
    if (this.amexTendered && this.tenderedValue == 0) {
      this.recalculateDueAmount(this.amexTendered);
      this.amexTendered = 0;
    }
    this.enteredValue = '';
    this.tenderedValue = 0;
  }

  masterFunction() {
    if (this.tenderedValue) {
      this.masterTendered = this.tenderedValue;
      this.bookingForm.patchValue({ masterCard: this.tenderedValue });
    }
    if (this.masterTendered && this.tenderedValue == 0) {
      this.recalculateDueAmount(this.masterTendered);
      this.masterTendered = 0;
    }
    this.enteredValue = '';
    this.tenderedValue = 0;
  }

  visaFunction() {
    if (this.tenderedValue) {
      this.visaTendered = this.tenderedValue;
      this.bookingForm.patchValue({ visa: this.tenderedValue });
    }
    if (this.visaTendered && this.tenderedValue == 0) {
      this.recalculateDueAmount(this.visaTendered);
      this.visaTendered = 0;
    }
    this.enteredValue = '';
    this.tenderedValue = 0;
  }

  debitFunction() {
    if (this.tenderedValue) {
      this.debitTendered = this.tenderedValue;
      this.bookingForm.patchValue({ debit: this.tenderedValue });
    }
    if (this.debitTendered && this.tenderedValue == 0) {
      this.recalculateDueAmount(this.debitTendered);
      this.debitTendered = 0;
    }
    this.enteredValue = '';
    this.tenderedValue = 0;
  }

  clear() {
    this.enteredValue = '';
    this.tenderedValue = 0;
    this.usTendered = 0;
    this.euroTendered = 0;
    this.discoverTendered = 0;
    this.amexTendered = 0;
    this.masterTendered = 0;
    this.visaTendered = 0;
    this.debitTendered = 0;
    this.currentDueAmount = this.dueAmount;
    this.bookingForm.patchValue({ amountDue: parseFloat((this.currentDueAmount).toFixed(2)) }, { emitEvent: false });
  }

  calculateDueAmount(newValue: number) {
    const rateTotal = this.accommodation?.accommodationRate?.totalPrice;
    if (rateTotal) {
      this.currentDueAmount = this.currentDueAmount - newValue;
    }
    this.bookingForm.patchValue({ amountDue: parseFloat((this.currentDueAmount).toFixed(2)) }, { emitEvent: false });
  }

  recalculateDueAmount(oldValue: number) {
    const rateTotal = this.accommodation?.accommodationRate?.totalPrice;
    if (rateTotal) {
      this.currentDueAmount = this.currentDueAmount + oldValue;
    }
    this.bookingForm.patchValue({ amountDue: parseFloat((this.currentDueAmount).toFixed(2)) }, { emitEvent: false });
  }

  getAccommodation() {
    const accommodationId = this.activatedRoute.snapshot.paramMap.get('accommodationId');
    //this.checkinTime = this. datePipe. transform(new Date(), 'yyyy-MM-dd HH:mm:ss');
    if (accommodationId) {
      this.fohService.getAccommodation(+accommodationId).subscribe({
        next: accommodation => {
          this.accommodation = accommodation;
          this.accommodationTypeName = accommodation.typeName;
          this.accommodationNumber = accommodation.number;
          if (accommodation.accommodationRate?.totalPrice) {
            this.dueAmount = accommodation.accommodationRate?.totalPrice;
            this.currentDueAmount = this.dueAmount;
          }
          this.netAmount = accommodation.accommodationRate?.totalPrice!;
          this.totalAmount = accommodation.accommodationRate?.totalPrice ? accommodation.accommodationRate?.totalPrice : 0,
          this.bookingForm.patchValue({
            taxes: parseFloat((accommodation.accommodationRate?.totalPrice! - accommodation.accommodationRate?.price!).toFixed(2)),
            rateTotal: accommodation.accommodationRate?.totalPrice,
            netAmount: this.netAmount,
            total: accommodation.accommodationRate?.totalPrice,
            amountDue: parseFloat((this.dueAmount).toFixed(2)),
          });
          // Transfer Accommodation if local stores "transferAccommodationDetails" has value
          if (this.transferAccommodationDetails) {
            this.netAmount = this.dueAmount - (this.transferAccommodationDetails.price * this.transferAccommodationDetails.qty);
            if (this.netAmount < 0) {
              window.alert("Sorry! you can not degrade this acoommodation");
              window.history.back();
            } else {
              const totalPriceOfBookedAccommodation = this.transferAccommodationDetails.totalPrice - this.transferAccommodationDetails.price;
              const totalPrice = accommodation.accommodationRate?.totalPrice! - accommodation.accommodationRate?.price!;
              this.bookingForm.patchValue({
                taxes: parseFloat((totalPriceOfBookedAccommodation - totalPrice).toFixed(2)),
                rateTotal: this.transferAccommodationDetails.totalPrice - accommodation.accommodationRate?.totalPrice!,
                netAmount: this.netAmount,
                total: this.transferAccommodationDetails.totalPrice - accommodation.accommodationRate?.totalPrice!,
                amountDue: parseFloat((this.netAmount).toFixed(2)),
              });
              this.fohService.checkOut(this.transferAccommodationDetails.accommodationId).subscribe();
            }
          }
        },
        error: error => console.log(error)
      });
    }
  }

  onSubmit() {
    this.loading = true;
    const accommodationId = this.accommodation?.accommodationId;
    const accommodationRateId = this.accommodation?.accommodationRate?.accommodationRateId;
    const hours = this.accommodation?.accommodationRate?.hours;
    const totalPrice = this.accommodation?.accommodationRate?.totalPrice;
    const userId = this.currentUser?.userId;
    const comment = this.bookingForm.value.comment;
    const debit = this.debitTendered;
    const amex = this.amexTendered;
    const masterCard = this.masterTendered;
    const visa = this.visaTendered;
    const discoveryCard = this.discoverTendered;
    const us = this.usTendered;
    const euro = this.euroTendered;
    const unionPay = this.bookingForm.value.unionPay;
    const diners = this.bookingForm.value.diners;
    const otherDiscountDollar = -this.otherDiscountDollar;
    const otherDiscountReason = this.otherDiscountReason;
    const couponId = this.couponId;
    const promopassId = this.promopassId;
    const couponsDiscount = this.couponsDiscount;
    const promoDiscount = this.promoDiscount;

    const cash = this.tenderedValue > 0 ? this.tenderedValue : this.accommodation?.accommodationRate?.totalPrice;
    const change = this.currentDueAmount < 0 ? this.currentDueAmount : 0;

    const requestBody = {
      accommodationId,
      accommodationRateId,
      hours,
      totalPrice,
      userId,
      debit,
      amex,
      masterCard,
      visa,
      discoveryCard,
      us,
      euro,
      cash,
      unionPay,
      diners,
      otherDiscountDollar,
      otherDiscountReason,
      couponId,
      promopassId,
      couponsDiscount,
      promoDiscount,
      change,
      comment
    };
    // console.log(requestBody);
    this.fohService.bookAccommodation(requestBody).subscribe({
      next: response => {
        console.log('Booking Completed', response);
        this.loading = false;
        this.router.navigateByUrl('/accommodations');
      },
      error: error => {
        console.error('Failed to book accommodation', error);
        this.loading = false;
      }
    });
  }

  openOutOfServicePopup() {
    this.matDialog.open(OutOfServiceComponent, {
      data: this.accommodation,
    });
  }

  openPromosCouponsPopup() {
    const dialogRef = this.matDialog.open(PromosCouponsComponent, {
      data: this.accommodation?.coupons
    });

    dialogRef.afterClosed().subscribe((data) => {
      if (data) {
        // Handle the received data here and update the form or do any processing
        console.log('Received Discount Data:', data);
        // Example: Update the form with received data
        const rateTotal = this.accommodation?.accommodationRate?.totalPrice;
        let promoOrPass = 0;
        if (data.couponId !== null) {
          this.couponId = data.couponId;
          if (this.couponId === 2 && data.discountPercent !== 0) {
            this.couponsDiscount = (data.discountPercent / 100) * rateTotal!;
            promoOrPass = this.couponsDiscount;
          } else if (this.couponId === 4 && data.flatRate !== 0) {
            promoOrPass = rateTotal! - data.flatRate;
          }
        } else if (data.promopassId !== null) {
          this.promopassId = data.promopassId;
          if (this.promopassId === 1 && data.discountPercent !== 0) {
            this.promoDiscount = (data.discountPercent / 100) * rateTotal!;
            promoOrPass = this.promoDiscount;
          } else if (this.promopassId === 3 && data.discountDollars !== 0) {
            this.promoDiscount = data.discountDollars;
            promoOrPass = this.promoDiscount;
          }
        }

        this.netAmount -= promoOrPass;
        this.currentDueAmount = this.netAmount;

        this.bookingForm.patchValue({
          promoOrPass: promoOrPass,
          totalDiscount: promoOrPass,
          netAmount: this.netAmount,
          total: rateTotal! - promoOrPass,
          amountDue: parseFloat((this.currentDueAmount).toFixed(2))
        });
        console.log(promoOrPass);
      }
    });
  }

  openOtherDiscountPopup() {
    const dialogRef = this.matDialog.open(OtherDiscountComponent, {});

    dialogRef.afterClosed().subscribe((data) => {
      if (data) {
        // Handle the received data here and update the form or do any processing
        console.log('Received Discount Data:', data);
        // Example: Update the form with received data
        const rateTotal = this.accommodation?.accommodationRate?.totalPrice;
        const calculatedDiscountDollar = (data.discountPercentage / 100) * rateTotal!;
        if (calculatedDiscountDollar !== 0) {
          this.otherDiscountDollar = calculatedDiscountDollar;
        } else if (data.dollarAmount !== 0) {
          this.otherDiscountDollar = data.dollarAmount;
        }
        this.otherDiscountReason = data.discountReason;

        this.netAmount -= this.otherDiscountDollar;
        this.currentDueAmount = this.netAmount;

        this.bookingForm.patchValue({
          otherDiscount: this.otherDiscountDollar,
          totalDiscount: this.otherDiscountDollar,
          netAmount: rateTotal! - this.otherDiscountDollar,
          total: rateTotal! - this.otherDiscountDollar,
          amountDue: parseFloat((this.currentDueAmount).toFixed(2))
        });
        console.log(this.otherDiscountDollar, this.otherDiscountReason);
      }
    });
  }
}