import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';

@Component({
  selector: 'app-end-shift',
  templateUrl: './end-shift.component.html',
  styleUrls: ['./end-shift.component.scss']
})
export class EndShiftComponent {
  dateToday: Date = new Date();
  currentUserId: string = '';
  currentUser: string = '';
  salesSessionId: number = 0;
  shiftNumber: number = 0;
  isDepositClicked: boolean = false;
  isDebitCreditClicked: boolean = false;
  totalDeposit: number = 0;
  totalDebitCredit: number = 0;
  grandTotal: number = 0;

  constructor(private matDialogRef: MatDialogRef<EndShiftComponent>, private accountService: AccountService, private fb: FormBuilder, private router: Router) { }

  endShiftForm = this.fb.group({
    currentUserId : '',
    salesSessionId : 0,
    finalCashDeposit: [0],
    finalDebitCredit: [0],
    // Deposit values
    nickels: [0],
    dimes: [0],
    quarters: [0],
    qty1Dollar: [0],
    qty2Dollar: [0],
    qty5Dollar: [0],
    qty10Dollar: [0],
    qty20Dollar: [0],
    qty50Dollar: [0],
    qty100Dollar: [0],
    cashDepositTotal: [0],
    // Debit and Credit values
    debit: [0],
    visa: [0],
    masterCard: [0],
    amex: [0],
    discover: [0],
    otherCreditCard: [0],
    usDollars: [0],
    euroDollars: [0],
    debitCreditTotal: [0],
    grandTotal: [0],
  });

  ngOnInit(): void {
    this.loadCurrentUser();
    this.getCurrentShift();
  }

  loadCurrentUser(){
    this.accountService.currentUser$.subscribe({
      next: user => {
        this.currentUserId = user? user.userId: '';
        this.currentUser = user? user.firstName: '';
      },
      error: error => console.log(error)
    });
    return this.currentUser;
  }

  getCurrentShift() {
    this.accountService.getActiveSession().subscribe({
      next: response => {
        const userShifts = response.filter(x => x.userId === this.currentUserId && x.isCurrentlyInUse === true);
        if (userShifts.length > 0) {
          this.shiftNumber = userShifts[0].shiftNumber;
          this.salesSessionId = userShifts[0].shiftSessionId;
        } else {
          this.shiftNumber = 0;
        }
      },
      error: error => console.log(error)
    });
  }

  // Deposite Section Amount Calculations
  dispositeClicked(){
    this.isDepositClicked=!this.isDepositClicked;
    if (this.isDepositClicked) {
      this.totalDeposit = 0;
      const finalDebitCredit = parseInt((this.endShiftForm.value.finalDebitCredit?this.endShiftForm.value.finalDebitCredit:0).toString());
      this.grandTotal = this.totalDeposit + finalDebitCredit;
      this.endShiftForm.patchValue({grandTotal: this.grandTotal});
    }
    else{
      this.grandTotal = this.totalDeposit + this.totalDebitCredit;
      this.endShiftForm.patchValue({grandTotal: this.grandTotal});
    }
    this.endShiftForm.patchValue({nickels: 0, dimes: 0, quarters: 0, qty1Dollar: 0,  qty2Dollar: 0, qty5Dollar: 0, qty10Dollar: 0, qty20Dollar: 0, qty50Dollar: 0, qty100Dollar: 0, finalCashDeposit:0});
    this.isDepositClicked?this.endShiftForm.controls['finalCashDeposit'].disable():this.endShiftForm.controls['finalCashDeposit'].enable();
  }

  finalCashDepositCalculation(value:any){
    value===""?value=0:value;
    this.totalDeposit += parseInt(value);
    const finalDebitCredit = parseInt((this.endShiftForm.value.finalDebitCredit?this.endShiftForm.value.finalDebitCredit:0).toString());
    this.grandTotal = this.totalDeposit + finalDebitCredit;
    this.endShiftForm.patchValue({finalCashDeposit:this.totalDeposit, grandTotal: this.grandTotal});
  }

  finalCashDeposit(value:any){
    value===""?value=0:value;
    if(value!=null){
      this.endShiftForm.patchValue({nickels: 0, dimes: 0, quarters: 0, qty1Dollar: 0,  qty2Dollar: 0, qty5Dollar: 0, qty10Dollar: 0, qty20Dollar: 0, qty50Dollar: 0, qty100Dollar: 0});
      this.isDepositClicked = false;
    }
    this.totalDeposit = parseInt(value);
    this.grandTotalCalculation();
  }

  // Debit & Credit Section Amount Calculations
  debitCreditClicked(){
    this.isDebitCreditClicked=!this.isDebitCreditClicked;
    if (this.isDebitCreditClicked) {
      this.grandTotal = this.totalDeposit;
      this.endShiftForm.patchValue({grandTotal: this.grandTotal});
    }
    else{
      this.grandTotal = this.totalDeposit + this.totalDebitCredit;
      this.endShiftForm.patchValue({grandTotal: this.grandTotal});
    }
    this.endShiftForm.patchValue({debit: 0, visa: 0, masterCard: 0, amex: 0,  discover: 0, otherCreditCard: 0, usDollars: 0, euroDollars: 0, finalDebitCredit:0});
    this.totalDebitCredit = 0;
    this.isDebitCreditClicked?this.endShiftForm.controls['finalDebitCredit'].disable():this.endShiftForm.controls['finalDebitCredit'].enable();
  }

  finalDebitCreditCalculation(value:any){
    value===""?value=0:value;
    this.totalDebitCredit += parseInt(value);
    this.grandTotal = this.totalDeposit + this.totalDebitCredit;
    this.endShiftForm.patchValue({finalDebitCredit:this.totalDebitCredit, debitCreditTotal:this.totalDebitCredit, grandTotal: this.grandTotal});
  } 

  finalDebitCredit(value:any){
    value===""?value=0:value;
    if(value!=null){
      this.endShiftForm.patchValue({debit: 0, visa: 0, masterCard: 0, amex: 0,  discover: 0, otherCreditCard: 0, usDollars: 0, euroDollars: 0});
      this.isDebitCreditClicked = false;
    }
    this.totalDebitCredit = parseInt(value);
    this.grandTotalCalculation();
  }

  grandTotalCalculation(){
    const finalCashDeposit = parseInt((this.endShiftForm.value.finalCashDeposit?this.endShiftForm.value.finalCashDeposit:this.totalDeposit).toString());
    const finalDebitCredit = parseInt((this.endShiftForm.value.finalDebitCredit?this.endShiftForm.value.finalDebitCredit:this.totalDebitCredit).toString());
    this.grandTotal = finalCashDeposit + finalDebitCredit;
    this.endShiftForm.patchValue({grandTotal:this.grandTotal});
  }
  

  onSubmit() {
    this.endShiftForm.value.cashDepositTotal = this.totalDeposit; 
    this.endShiftForm.value.debitCreditTotal = this.totalDebitCredit; 
    this.endShiftForm.value.currentUserId = this.currentUserId; 
    this.endShiftForm.value.salesSessionId = this.salesSessionId; 
    this.accountService.endSession(this.endShiftForm.value).subscribe({
      next: (response) => {
        this.closePopup();
        this.router.navigateByUrl('/dashboard');
      },
      error: error => {
        console.log(error);
      }
    })
  }

  closePopup() {
    this.matDialogRef.close();
  }
}
