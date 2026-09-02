import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CleaningComponent } from './cleaning/cleaning.component';
import { CoreModule } from 'app/core/core.module';

const routes: Routes = [
  { path: '', component: CleaningComponent },
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
export class CleaningRoutingModule { }
