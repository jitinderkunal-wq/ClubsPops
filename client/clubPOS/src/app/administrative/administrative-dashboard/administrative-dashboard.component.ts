import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ReceiptMessageComponent } from '../receipt-message/receipt-message.component';
import { MembersComponent } from '../members/members.component';
import { TaxesAndExchangeComponent } from '../taxes-and-exchange/taxes-and-exchange.component';
import { CashierReconciliationPopupComponent } from '../cashier-reconciliation-popup/cashier-reconciliation-popup.component'
import { MembershipTypesComponent } from '../membership-types/membership-types.component';
import { PromoPassesComponent } from '../promo-passes/promo-passes.component'
import { RoomSetupComponent } from '../room-setup/room-setup.component';
import { LockerSetupComponent } from '../locker-setup/locker-setup.component';
import { MaintenanceTaskSetupComponent } from '../maintenance-task-setup/maintenance-task-setup.component';
import { CleaningTaskSetupComponent } from '../cleaning-task-setup/cleaning-task-setup.component';
import { ShiftSetupComponent } from '../shift-setup/shift-setup.component';
import { AccommodationRateSetupComponent } from '../accommodation-rate-setup/accommodation-rate-setup.component';
import { AccommodationTypeComponent } from '../accommodation-type/accommodation-type.component';
import { AccommodationStatusComponent } from '../accommodation-status/accommodation-status.component';
import { AdministrativeService } from '../administrative.service';
import { ActiveSessionCashReconcilation } from 'app/shared/models/activeSessionCashReconcilation';


@Component({
  selector: 'app-administrative-dashboard',
  templateUrl: './administrative-dashboard.component.html',
  styleUrls: ['./administrative-dashboard.component.scss']
})
export class AdministrativeDashboardComponent {
  activeSalesSessionsCount: number = 0;

  constructor(private matDialog: MatDialog, private administrativeService: AdministrativeService) { }

  ngOnInit(): void {
    this.getActiveSession();
  }

  getActiveSession(){
    this.administrativeService.getActiveSession().subscribe({
      next: response => {
         this.activeSalesSessionsCount = response.length;
      },
      error: error => console.log(error)
    });
  }

  openReceiptMessagePopup() {
    this.matDialog.open(ReceiptMessageComponent, {
    });
  }

  openMembersPopup() {
    this.matDialog.open(MembersComponent, {
    });
  }

  openTaxesAndExchangePopup() {
    this.matDialog.open(TaxesAndExchangeComponent, {
    });
  }

  openCashReconciliationsPopup() {
    this.matDialog.open(CashierReconciliationPopupComponent, {});
  }

  openAccommodationRateSetupPopup() {
    this.matDialog.open(AccommodationRateSetupComponent, {
    });
  }

  openAccommodationTypePopup() {
    this.matDialog.open(AccommodationTypeComponent, {
    });
  }

  openAccommodationStatusPopup() {
    this.matDialog.open(AccommodationStatusComponent, {
    });
  }

  openMembershipTypesPopup() {
    this.matDialog.open(MembershipTypesComponent, {
    });
  }

  openPromoPassPopup() {
    this.matDialog.open(PromoPassesComponent, {

    });
  }

  openShiftSetupPopup() {
    this.matDialog.open(ShiftSetupComponent, {

    });
  }

  openRoomSetupPopup() {
    this.matDialog.open(RoomSetupComponent, {

    });
  }

  openLockerSetupPopup() {
    this.matDialog.open(LockerSetupComponent, {

    });
  }

  openMaintenanceSetupPopup() {
    this.matDialog.open(MaintenanceTaskSetupComponent, {

    });
  }

  openCleaningSetupPopup() {
    this.matDialog.open(CleaningTaskSetupComponent, {

    });
  }
}