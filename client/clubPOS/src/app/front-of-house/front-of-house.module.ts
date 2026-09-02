import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccommodationComponent } from './accommodation/accommodation.component';
import { FormsModule } from '@angular/forms';
import { BookAccommodationComponent } from './book-accommodation/book-accommodation.component';
import { OutOfServiceComponent } from './out-of-service/out-of-service.component';
import { FohRoutingModule } from './foh-routing.module';
import { MatDialogModule } from '@angular/material/dialog';
import { ClearOutOfServiceComponent } from './clear-out-of-service/clear-out-of-service.component';
import { OutOfServiceListComponent } from './out-of-service-list/out-of-service-list.component';
import { SystemProblemComponent } from './system-problem/system-problem.component';
import { SharedModule } from 'app/shared/shared.module';
import { CurrentActivityListComponent } from './current-activity-list/current-activity-list.component';
import { WaitingListComponent } from './waiting-list/waiting-list.component';
import { AddToWaitingListComponent } from './add-to-waiting-list/add-to-waiting-list.component';
import { OtherDiscountComponent } from './other-discount/other-discount.component';
import { WaitingListAlertComponent } from './waiting-list-alert/waiting-list-alert.component';
import { BookedAccommodationComponent } from './booked-accommodation/booked-accommodation.component';
import { PromosCouponsComponent } from './promos-coupons/promos-coupons.component';
import { TransferAlertComponent } from './transfer-alert/transfer-alert.component';
import { CleaningCompleteComponent } from './cleaning-complete/cleaning-complete.component';
import { TransferComponent } from './transfer/transfer.component';
import { CashDropDrawerSkimsComponent } from './cash-drop-drawer-skims/cash-drop-drawer-skims.component';

@NgModule({
  declarations: [
    AccommodationComponent,
    BookAccommodationComponent,
    OutOfServiceComponent,
    ClearOutOfServiceComponent,
    OutOfServiceListComponent,
    SystemProblemComponent,
    CurrentActivityListComponent,
    WaitingListComponent,
    AddToWaitingListComponent,
    OtherDiscountComponent,
    WaitingListAlertComponent,
    BookedAccommodationComponent,
    PromosCouponsComponent,
    TransferAlertComponent,
    CleaningCompleteComponent,
    TransferComponent,
    CashDropDrawerSkimsComponent
  ],
  imports: [
    CommonModule,
    FohRoutingModule,
    SharedModule,
    FormsModule,
    MatDialogModule
  ]
})
export class FrontOfHouseModule { }