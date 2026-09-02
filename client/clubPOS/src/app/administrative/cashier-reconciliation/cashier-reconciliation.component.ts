import { Component, Inject } from '@angular/core';
import { AdministrativeService } from '../administrative.service';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';


@Component({
  selector: 'app-cashier-reconciliation',
  templateUrl: './cashier-reconciliation.component.html',
  styleUrls: ['./cashier-reconciliation.component.scss'],
})
export class CashierReconciliationComponent {

  data: any;
  activeSession: any;
  currentDateTime: Date = new Date();
  private myKey = 'selectedSession';

  //#region boolean value declaration
    accommodationSalesPolledModify: boolean = false;
    refundAccommodationPolledModify: boolean = false;
    voidedAccommodationsPolledModify: boolean = false;
    rateDiscountsPolledModify: boolean = false;
    couponDiscountsPolledModify: boolean = false;
    promoDiscountsPolledModify: boolean = false;
    otherDiscountsRolledModify: boolean = false;
    memberDiscountsPolledModify: boolean = false;
    depositsCollectedPolledModify: boolean = false;
    depositsReturnedPolledModify: boolean = false;
    membershipSalesPolledModify: boolean = false;
    otherSalesPolledModify: boolean = false;
    barSalesPolledModify: boolean = false;
    debitPolledModify: boolean = false;
    masterCardPolledModify: boolean = false;
    visaPolledModify: boolean = false;
    amexPolledModify: boolean = false;
    discoverPolledModify: boolean = false;
    otherCreditCardPolledModify: boolean = false;
    totalUsdollarsPolledModify: boolean = false;
    totalEuroDollarsPolledModify: boolean = false;
    finalCashDepositPolledModify: boolean = false;
    paidInsPolledModify: boolean = false;
    receiptsPaidOutsPolledModify: boolean = false;
  //#endregion

  constructor(private router: Router, private fb: FormBuilder,private administrativeService: AdministrativeService) {
  }

  cashierReconciliationForm = this.fb.group({
    salesSessionId :[0],
    accommodationSalesPolled : [{value: 0, disabled: true }],
    refundAccommodationPolled : [{value: 0, disabled: true }],
    voidedAccommodationsPolled : [{value: 0, disabled: true }],
    rateDiscountsPolled : [{value: 0, disabled: true }],
    couponDiscountsPolled : [{value: 0, disabled: true }],
    promoDiscountsPolled : [{value: 0, disabled: true }],
    otherDiscountsRolled : [{value: 0, disabled: true }],
    memberDiscountsPolled : [{value: 0, disabled: true }],
    totalAccommodationSalesPolled : [0],
    depositsCollectedPolled : [{value: 0, disabled: true }],
    depositsReturnedPolled : [{value: 0, disabled: true }],
    membershipSalesPolled : [{value: 0, disabled: true }],
    otherSalesPolled : [{value: 0, disabled: true }],
    barSalesPolled : [{value: 0, disabled: true }],
    totalFinalSalesPolled : [0],
    debitPolled : [{value: 0, disabled: true }],
    masterCardPolled : [{value: 0, disabled: true }],
    visaPolled : [{value: 0, disabled: true }],
    amexPolled : [{value: 0, disabled: true }],
    discoverPolled : [{value: 0, disabled: true }],
    otherCreditCardPolled : [{value: 0, disabled: true }],
    totalDebitCreditPolled : [0],
    eosDebitCreditTotalPolled : [0],
    totalUsdollarsPolled : [{value: 0, disabled: true }],
    totalEuroDollarsPolled : [{value: 0, disabled: true }],
    finalCashDepositPolled : [{value: 0, disabled: true }],
    paidInsPolled : [{value: 0, disabled: true }],
    receiptsPaidOutsPolled : [{value: 0, disabled: true }],
    cashDropsTotalPolled : [0],
    cashOverShortPolled : [0],
  });

  ngOnInit() : void{
    this.getActiveSession();
    this.getCashReconcilation();
  }

  getActiveSession(){
    const storedData = localStorage.getItem(this.myKey);
    this.activeSession = storedData ? JSON.parse(storedData) : null;
  }

  enableModifyClicked(event: any): void {
    switch (event) {
      case 'accommodationSalesPolled':
        this.accommodationSalesPolledModify = !this.accommodationSalesPolledModify;
        this.accommodationSalesPolledModify
          ? this.cashierReconciliationForm.get(event)?.enable()
          : this.cashierReconciliationForm.get(event)?.disable();
        break;
      case 'refundAccommodationPolled':
        this.refundAccommodationPolledModify = !this.refundAccommodationPolledModify;
        this.refundAccommodationPolledModify
          ? this.cashierReconciliationForm.get(event)?.enable()
          : this.cashierReconciliationForm.get(event)?.disable();
        break;
      case 'voidedAccommodationsPolled':
        this.voidedAccommodationsPolledModify = !this.voidedAccommodationsPolledModify;
        this.voidedAccommodationsPolledModify
          ? this.cashierReconciliationForm.get(event)?.enable()
          : this.cashierReconciliationForm.get(event)?.disable();
        break;
      case 'rateDiscountsPolled':
        this.rateDiscountsPolledModify = !this.rateDiscountsPolledModify;
        this.rateDiscountsPolledModify
          ? this.cashierReconciliationForm.get(event)?.enable()
          : this.cashierReconciliationForm.get(event)?.disable();
        break;
      case 'couponDiscountsPolled':
        this.couponDiscountsPolledModify = !this.couponDiscountsPolledModify;
        this.couponDiscountsPolledModify
          ? this.cashierReconciliationForm.get(event)?.enable()
          : this.cashierReconciliationForm.get(event)?.disable();
        break;
      case 'promoDiscountsPolled':
        this.promoDiscountsPolledModify = !this.promoDiscountsPolledModify;
        this.promoDiscountsPolledModify
          ? this.cashierReconciliationForm.get(event)?.enable()
          : this.cashierReconciliationForm.get(event)?.disable();
        break;
      case 'otherDiscountsRolled':
        this.otherDiscountsRolledModify = !this.otherDiscountsRolledModify;
        this.otherDiscountsRolledModify
          ? this.cashierReconciliationForm.get(event)?.enable()
          : this.cashierReconciliationForm.get(event)?.disable();
        break;
      case 'memberDiscountsPolled':
        this.memberDiscountsPolledModify = !this.memberDiscountsPolledModify;
        this.memberDiscountsPolledModify
          ? this.cashierReconciliationForm.get(event)?.enable()
          : this.cashierReconciliationForm.get(event)?.disable();
        break;
      case 'depositsCollectedPolled':
        this.depositsCollectedPolledModify = !this.depositsCollectedPolledModify;
        this.depositsCollectedPolledModify
          ? this.cashierReconciliationForm.get(event)?.enable()
          : this.cashierReconciliationForm.get(event)?.disable();
        break;
      case 'depositsReturnedPolled':
        this.depositsReturnedPolledModify = !this.depositsReturnedPolledModify;
        this.depositsReturnedPolledModify
          ? this.cashierReconciliationForm.get(event)?.enable()
          : this.cashierReconciliationForm.get(event)?.disable();
        break;
      case 'membershipSalesPolled':
        this.membershipSalesPolledModify = !this.membershipSalesPolledModify;
        this.membershipSalesPolledModify
          ? this.cashierReconciliationForm.get(event)?.enable()
          : this.cashierReconciliationForm.get(event)?.disable();
        break;
      case 'otherSalesPolled':
        this.otherSalesPolledModify = !this.otherSalesPolledModify;
        this.otherSalesPolledModify
          ? this.cashierReconciliationForm.get(event)?.enable()
          : this.cashierReconciliationForm.get(event)?.disable();
        break;
      case 'barSalesPolled':
        this.barSalesPolledModify = !this.barSalesPolledModify;
        this.barSalesPolledModify
          ? this.cashierReconciliationForm.get(event)?.enable()
          : this.cashierReconciliationForm.get(event)?.disable();
        break;
      case 'debitPolled':
        this.debitPolledModify = !this.debitPolledModify;
        this.debitPolledModify
          ? this.cashierReconciliationForm.get(event)?.enable()
          : this.cashierReconciliationForm.get(event)?.disable();
        break;
      case 'masterCardPolled':
        this.masterCardPolledModify = !this.masterCardPolledModify;
        this.masterCardPolledModify
          ? this.cashierReconciliationForm.get(event)?.enable()
          : this.cashierReconciliationForm.get(event)?.disable();
        break;
      case 'visaPolled':
        this.visaPolledModify = !this.visaPolledModify;
        this.visaPolledModify
          ? this.cashierReconciliationForm.get(event)?.enable()
          : this.cashierReconciliationForm.get(event)?.disable();
        break;
      case 'amexPolled':
        this.amexPolledModify = !this.amexPolledModify;
        this.amexPolledModify
          ? this.cashierReconciliationForm.get(event)?.enable()
          : this.cashierReconciliationForm.get(event)?.disable();
        break;
      case 'discoverPolled':
        this.discoverPolledModify = !this.discoverPolledModify;
        this.discoverPolledModify
          ? this.cashierReconciliationForm.get(event)?.enable()
          : this.cashierReconciliationForm.get(event)?.disable();
        break;
      case 'otherCreditCardPolled':
        this.otherCreditCardPolledModify = !this.otherCreditCardPolledModify;
        this.otherCreditCardPolledModify
          ? this.cashierReconciliationForm.get(event)?.enable()
          : this.cashierReconciliationForm.get(event)?.disable();
        break;
      case 'totalUsdollarsPolled':
        this.totalUsdollarsPolledModify = !this.totalUsdollarsPolledModify;
        this.totalUsdollarsPolledModify
          ? this.cashierReconciliationForm.get(event)?.enable()
          : this.cashierReconciliationForm.get(event)?.disable();
        break;
      case 'totalEuroDollarsPolled':
        this.totalEuroDollarsPolledModify = !this.totalEuroDollarsPolledModify;
        this.totalEuroDollarsPolledModify
          ? this.cashierReconciliationForm.get(event)?.enable()
          : this.cashierReconciliationForm.get(event)?.disable();
        break;
      case 'finalCashDepositPolled':
        this.finalCashDepositPolledModify = !this.finalCashDepositPolledModify;
        this.finalCashDepositPolledModify
          ? this.cashierReconciliationForm.get(event)?.enable()
          : this.cashierReconciliationForm.get(event)?.disable();
        break;
      case 'paidInsPolled':
        this.paidInsPolledModify = !this.paidInsPolledModify;
        this.paidInsPolledModify
          ? this.cashierReconciliationForm.get(event)?.enable()
          : this.cashierReconciliationForm.get(event)?.disable();
        break;
      case 'receiptsPaidOutsPolled':
        this.receiptsPaidOutsPolledModify = !this.receiptsPaidOutsPolledModify;
        this.receiptsPaidOutsPolledModify
          ? this.cashierReconciliationForm.get(event)?.enable()
          : this.cashierReconciliationForm.get(event)?.disable();
        break;
      default:
        break;
    }
  }

  getCashReconcilation(){
    this.administrativeService.getCashReconcilation().subscribe({
      next : response=>{
        this.data=response
        console.log(response);
      }
    });
  }

  onSubmit(){
    this.cashierReconciliationForm.value.salesSessionId = this.activeSession.salesSessionId;
    this.administrativeService.createCashReconcilation(this.cashierReconciliationForm.value).subscribe({
      next : response=>{
        console.log(response);
      }
    });
  }

  // selectedSession(){
  //   this.administrativeService.getCashReconcilation().subscribe({
  //     next : response=>{
        
  //     }
  //   })
  // }

  // closePopup() {
  //   this.matDialogRef.close();
  // }
}
