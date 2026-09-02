import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccommodationComponent } from './accommodation/accommodation.component';
import { BookAccommodationComponent } from './book-accommodation/book-accommodation.component';
import { OutOfServiceComponent } from './out-of-service/out-of-service.component';
import { CoreModule } from 'app/core/core.module';
import { BookedAccommodationComponent } from './booked-accommodation/booked-accommodation.component';

const routes: Routes = [
  { path: '', component: AccommodationComponent },
  { path: ':accommodationId', component: BookAccommodationComponent },
  { path: ':accommodationId/bookedAccommodation', component: BookedAccommodationComponent },
  { path: ':accommodationId/makeOutOfService', component: OutOfServiceComponent }
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule,
    CoreModule
  ]
})
export class FohRoutingModule { }