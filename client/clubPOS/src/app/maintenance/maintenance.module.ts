import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaintenanceComponent } from './maintenance/maintenance.component';
import { MaintenanceRoutingModule } from './maintenance-routing.module';
import { SharedModule } from 'app/shared/shared.module';
import { ClearOutOfServiceComponent } from './clear-out-of-service/clear-out-of-service.component';
import { FormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { OutOfServiceListComponent } from './out-of-service-list/out-of-service-list.component';



@NgModule({
  declarations: [
    MaintenanceComponent,
    ClearOutOfServiceComponent,
    OutOfServiceListComponent
  ],
  imports: [
    CommonModule,
    MaintenanceRoutingModule,
    SharedModule,
    FormsModule,
    MatDialogModule
  ]
})
export class MaintenanceModule { }
