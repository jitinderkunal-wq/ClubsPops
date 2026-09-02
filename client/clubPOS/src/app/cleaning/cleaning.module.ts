import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CleaningComponent } from './cleaning/cleaning.component';
import { CleaningRoutingModule } from './cleaning-routing.module';
import { SharedModule } from 'app/shared/shared.module';
import { CleaningCompleteComponent } from './cleaning-complete/cleaning-complete.component';
import { FormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';



@NgModule({
  declarations: [
    CleaningComponent,
    CleaningCompleteComponent
  ],
  imports: [
    CommonModule,
    CleaningRoutingModule,
    SharedModule,
    FormsModule,
    MatDialogModule
  ]
})
export class CleaningModule { }
