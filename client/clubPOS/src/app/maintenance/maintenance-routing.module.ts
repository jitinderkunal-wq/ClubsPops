import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MaintenanceComponent } from './maintenance/maintenance.component';

const routes: Routes = [
  { path: '', component: MaintenanceComponent },
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
export class MaintenanceRoutingModule { }
