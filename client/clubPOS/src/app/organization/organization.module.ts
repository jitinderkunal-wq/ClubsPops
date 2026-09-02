import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrganizationComponent } from './organization/organization.component';
import { OrganizationDashboardComponent } from './organization-dashboard/organization-dashboard.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'app/shared/shared.module';
import { AdminComponent } from './admin/admin.component';
import { OrganizationRoutingModule } from './organization-routing.module';



@NgModule({
  declarations: [
    OrganizationComponent,
    OrganizationDashboardComponent,
    AdminComponent
    
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    SharedModule,
    OrganizationRoutingModule
  ]
})
export class OrganizationModule { }
