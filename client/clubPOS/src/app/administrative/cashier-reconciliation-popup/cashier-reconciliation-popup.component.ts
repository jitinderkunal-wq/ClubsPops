import { Component, Inject } from '@angular/core';
import {MatDialogRef } from '@angular/material/dialog';
import { AdministrativeService } from '../administrative.service';
import { FormBuilder } from '@angular/forms';
import { NavigationExtras, Router } from '@angular/router';
import { ActiveSessionCashReconcilation } from 'app/shared/models/activeSessionCashReconcilation';

@Component({
  selector: 'app-cashier-reconciliation-popup',
  templateUrl: './cashier-reconciliation-popup.component.html',
  styleUrls: ['./cashier-reconciliation-popup.component.scss']
})
export class CashierReconciliationPopupComponent {

  private myKey = 'selectedSession';
  activeSalesSessions: ActiveSessionCashReconcilation[] = [];
  constructor(private matDialogRef: MatDialogRef<CashierReconciliationPopupComponent>, private fb: FormBuilder, private administrativeService: AdministrativeService, private router : Router) {
  }

  ngOnInit(): void {
    this.getActiveSession();
  }

  getActiveSession(){
    this.administrativeService.getActiveSession().subscribe({
      next: response => {
         this.activeSalesSessions = response;
      },
      error: error => console.log(error)
    });
  }

  selectedSession(activeSession: any){
    localStorage.setItem(this.myKey, JSON.stringify(activeSession));
    this.router.navigate(['/cashier']);
    this.closePopup();

    // this.administrativeService.getCashReconcilation().subscribe({
    //   next : response=>{
        
    //   }
    // })
  }

  closePopup() {
    this.matDialogRef.close();
  }
}
