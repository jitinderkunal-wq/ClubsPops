import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdministrativeDashboardComponent } from './administrative-dashboard/administrative-dashboard.component';
import { AdministrativeRoutingModule } from './administrative-routing.module';
import { SharedModule } from 'app/shared/shared.module';
import { ReceiptMessageComponent } from './receipt-message/receipt-message.component';
import { MembersComponent } from './members/members.component';
import { TaxesAndExchangeComponent } from './taxes-and-exchange/taxes-and-exchange.component';
import { FormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { MembershipTypesComponent } from './membership-types/membership-types.component';
import { PromoPassesComponent } from './promo-passes/promo-passes.component';
import { RoomSetupComponent } from './room-setup/room-setup.component';
import { LockerSetupComponent } from './locker-setup/locker-setup.component';
import { MaintenanceTaskSetupComponent } from './maintenance-task-setup/maintenance-task-setup.component';
import { CleaningTaskSetupComponent } from './cleaning-task-setup/cleaning-task-setup.component';
import { ShiftSetupComponent } from './shift-setup/shift-setup.component';
import { AccommodationRateSetupComponent } from './accommodation-rate-setup/accommodation-rate-setup.component';
import { CashierReconciliationPopupComponent } from './cashier-reconciliation-popup/cashier-reconciliation-popup.component';
import { CashierReconciliationComponent } from './cashier-reconciliation/cashier-reconciliation.component';
import { AccommodationTypeComponent } from './accommodation-type/accommodation-type.component';
import { AccommodationStatusComponent } from './accommodation-status/accommodation-status.component';



@NgModule({
  declarations: [
    AdministrativeDashboardComponent,
    ReceiptMessageComponent,
    MembersComponent,
    TaxesAndExchangeComponent,
    MembershipTypesComponent,
    PromoPassesComponent,
    RoomSetupComponent,
    LockerSetupComponent,
    MaintenanceTaskSetupComponent,
    CleaningTaskSetupComponent,
    ShiftSetupComponent,
    AccommodationRateSetupComponent,
    CashierReconciliationPopupComponent,
    CashierReconciliationComponent,
    AccommodationTypeComponent,
    AccommodationStatusComponent
  ],
  imports: [
    CommonModule,
    AdministrativeRoutingModule,
    SharedModule,
    FormsModule,
    MatDialogModule
  ]
})
export class AdministrativeModule { }