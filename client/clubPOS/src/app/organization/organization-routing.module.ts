import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OrganizationComponent } from './organization/organization.component';
import { OrganizationDashboardComponent } from './organization-dashboard/organization-dashboard.component';
import { AdminComponent } from './admin/admin.component';
import { CoreModule } from 'app/core/core.module';

const routes: Routes = [
  { path: '', component: OrganizationDashboardComponent },
  { path: 'organizations', component: OrganizationComponent },
  { path: 'admin', component: AdminComponent },
  //{ path: ':organizationId', component: AdminComponent },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule,CoreModule],
})
export class OrganizationRoutingModule {}
