import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdministrativeDashboardComponent } from './administrative-dashboard/administrative-dashboard.component';

const routes: Routes = [
  { path: '', component: AdministrativeDashboardComponent },
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule,
    // CoreModule
  ]
})
export class AdministrativeRoutingModule { }
