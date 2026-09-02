import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { AccountService } from 'app/account/account.service';
import { EndShiftComponent } from 'app/account/end-shift/end-shift.component';
import { mapApiResponse } from 'app/shared/models/responseStatus/response';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-cash-drop-drawer-skims',
  templateUrl: './cash-drop-drawer-skims.component.html',
  styleUrls: ['./cash-drop-drawer-skims.component.scss']
})
export class CashDropDrawerSkimsComponent {
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

  constructor(private matDialogRef: MatDialogRef<EndShiftComponent>, private accountService: AccountService, private fb: FormBuilder, private router: Router, public toastr: ToastrService) { }

  cashDrawerForm = this.fb.group({
    skimShiftCount: 0,
    salesSessionId: 0,
    coinTotal: [], 
    totalCoin: [], 
    qty1Dollar: [],
    qty2Dollar: [],
    qty5Dollar: [],
    qty10Dollar: [],
    qty20Dollar: [],
    qty50Dollar: [],
    qty100Dollar: [],

    total1Dollar: [null as number | null],
    total2Dollar: [null as number | null],
    total5Dollar: [null as number | null],
    total10Dollar: [null as number | null],
    total20Dollar: [null as number | null],
    total50Dollar: [null as number | null],
    total100Dollar: [null as number | null],

    totalCaddollar: [null as number | null],

    qty1DollarUs: [],
    qty2DollarUs: [],
    qty5DollarUs: [],
    qty10DollarUs: [],
    qty20DollarUs: [],
    qty50DollarUs: [],
    qty100DollarUs: [],

    total1DollarUs: [null as number | null],
    total2DollarUs: [null as number | null],
    total5DollarUs: [null as number | null],
    total10DollarUs: [null as number | null],
    total20DollarUs: [null as number | null],
    total50DollarUs: [null as number | null],
    total100DollarUs: [null as number | null],

    totalUsdollars: [null as number | null],

    totalCheques: [null as number | null],
    totalEuros: [null as number | null],
    totalOther: [null as number | null],

    grandTotal: [0]

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
          this.cashDrawerForm.patchValue({skimShiftCount: this.shiftNumber, salesSessionId: this.salesSessionId});
        } else {
          this.shiftNumber = 0;
        }
      },
      error: error => console.log(error)
    });
  }

  totalCaddollarCalculation(value:any){
    const denominations: [string, string, number][] = [
        ['coinTotal', 'totalCoin', 1],
        ['qty1Dollar', 'total1Dollar', 1],
        ['qty2Dollar', 'total2Dollar', 2],
        ['qty5Dollar', 'total5Dollar', 5],
        ['qty10Dollar', 'total10Dollar', 10],
        ['qty20Dollar', 'total20Dollar', 20],
        ['qty50Dollar', 'total50Dollar', 50],
        ['qty100Dollar', 'total100Dollar', 100]
    ];

    // Sort the denominations array by value (ascending order)
    denominations.sort((a, b) => a[2] - b[2]);

    const totals: { [key: string]: number } = {};

    // Iterate over the sorted denominations array to calculate totals
    denominations.forEach(([id, totalId, value]) => {
        const inputValue = (document.getElementById(id) as HTMLInputElement).valueAsNumber || 0;
        const total = inputValue * value;
        totals[totalId] = total;
        (document.getElementById(totalId) as HTMLElement).innerText = `= $ ${total.toFixed(2)}`;
    });
    // Calculate the total CAD amount
    const totalCaddollar = Object.values(totals).reduce((acc, curr) => acc + curr, 0);

    // Update the form group with the calculated totals
    this.cashDrawerForm.patchValue({
      total1Dollar: totals['total1Dollar'] || 0,
      total2Dollar: totals['total2Dollar'] || 0,
      total5Dollar: totals['total5Dollar'] || 0,
      total10Dollar: totals['total10Dollar'] || 0,
      total20Dollar: totals['total20Dollar'] || 0,
      total50Dollar: totals['total50Dollar'] || 0,
      total100Dollar: totals['total100Dollar'] || 0,
      totalCaddollar: totalCaddollar
    });
    // Call the function to update the total CAD amount
    this.updateTotalCaddollar(totalCaddollar);
    this.recalculateTotalCashDrop();
  }

  updateTotalCaddollar(total: number) {
    // Assuming there is an element with ID 'totalCaddollar' to display the total amount
    const totalCaddollarElement = document.getElementById('totalCaddollar') as HTMLElement;
    totalCaddollarElement.innerText = `= $ ${total.toFixed(2)}`;
  }

  totalUsdollarCalculation(value:any){
    // Define the denominations and their values in an array of tuples
    const denominations: [string, string, number][] = [
        ['qty1DollarUs', 'total1DollarUs', 1],
        ['qty2DollarUs', 'total2DollarUs', 2],
        ['qty5DollarUs', 'total5DollarUs', 5],
        ['qty10DollarUs', 'total10DollarUs', 10],
        ['qty20DollarUs', 'total20DollarUs', 20],
        ['qty50DollarUs', 'total50DollarUs', 50],
        ['qty100DollarUs', 'total100DollarUs', 100]
    ];

    // Sort the denominations array by value (ascending order)
    denominations.sort((a, b) => a[2] - b[2]);

    const totals: { [key: string]: number } = {};

    // Iterate over the sorted denominations array to calculate totals
    denominations.forEach(([id, totalId, value]) => {
        const inputValue = (document.getElementById(id) as HTMLInputElement).valueAsNumber || 0;
        const total = inputValue * value;
        totals[totalId] = total;

        (document.getElementById(totalId) as HTMLElement).innerText = `= $ ${total.toFixed(2)}`;
    });

    // Calculate the total USD amount
    const totalUsdollars = Object.values(totals).reduce((acc, curr) => acc + curr, 0);

    // Update the form group with the calculated totals
    this.cashDrawerForm.patchValue({
        total1DollarUs: totals['total1DollarUs'] || 0,
        total2DollarUs: totals['total2DollarUs'] || 0,
        total5DollarUs: totals['total5DollarUs'] || 0,
        total10DollarUs: totals['total10DollarUs'] || 0,
        total20DollarUs: totals['total20DollarUs'] || 0,
        total50DollarUs: totals['total50DollarUs'] || 0,
        total100DollarUs: totals['total100DollarUs'] || 0,
        totalUsdollars: totalUsdollars
    });

    // Call the function to update the total USD amount
    this.updateTotalUsdollars(totalUsdollars);
    this.recalculateTotalCashDrop();
    console.log(this.cashDrawerForm);
  }

  // Define the updateTotalUsdollars function
  updateTotalUsdollars(total: number) {
      // Assuming there is an element with ID 'totalUsdollars' to display the total amount
      const totalUsdollarsElement = document.getElementById('totalUsdollars') as HTMLElement;
      totalUsdollarsElement.innerText = `= $ ${total.toFixed(2)}`;
  }

  totalCashDropCalculation(event: Event) {
    const inputElement = event.target as HTMLInputElement;
    const elementId = inputElement.id;
    const elementValue = inputElement.valueAsNumber;

    if (elementId) {
      switch (elementId) {
        case 'totalEuros':
          this.cashDrawerForm.patchValue({totalEuros: elementValue});
          this.cashDrawerForm.patchValue({totalCheques: 0});
          this.cashDrawerForm.patchValue({totalOther: 0});
          break;
        case 'totalOther':
          this.cashDrawerForm.patchValue({totalEuros: elementValue});
          this.cashDrawerForm.patchValue({totalEuros: 0});
          this.cashDrawerForm.patchValue({totalCheques: 0});
          break;
        case 'totalCheques':
          this.cashDrawerForm.patchValue({totalEuros: elementValue});
          this.cashDrawerForm.patchValue({totalEuros: 0});
          this.cashDrawerForm.patchValue({totalOther: 0});
          break;
        default:
          break; 
      }

    } 
    this.recalculateTotalCashDrop();
  }

  recalculateTotalCashDrop() {
    const totalCaddollar = this.cashDrawerForm.controls['totalCaddollar'].value || 0;
    const totalUsdollars = this.cashDrawerForm.controls['totalUsdollars'].value || 0;
    const totalEuros = this.cashDrawerForm.controls['totalEuros'].value || 0;
    const totalCheques = this.cashDrawerForm.controls['totalCheques'].value || 0;
    const totalOther = this.cashDrawerForm.controls['totalOther'].value || 0;

    const totalCashDrop = totalCaddollar + totalUsdollars + totalEuros + totalCheques + totalOther;

    this.cashDrawerForm.patchValue({ grandTotal: totalCashDrop });

    (document.getElementById('grandTotal') as HTMLElement).innerText = `= $ ${totalCashDrop.toFixed(2)}`;
  }
  onSubmit() {
    this.accountService.createCashDropDrawerSkim(this.cashDrawerForm.value)
    .subscribe({
      next:  apiResponse => {
        const response = mapApiResponse<null>(apiResponse);
        if(response.status.toString() === "Success")
        {
          this.toastr.success(response.message, "Success");
          this.closePopup();
        }
        else{
          this.toastr.error(response.message, "Failed");
        }
      },
      error: error => {
        this.toastr.error("Failed to create cash drop drawer skim", "Failed");
        console.error('Failed to create cash drop drawer skim', error);
      }
    });
  }

  closePopup() {
    this.matDialogRef.close();
  }
}