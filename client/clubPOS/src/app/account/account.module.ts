import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AccountRoutingModule } from './account-routing.module';
import { SharedModule } from 'app/shared/shared.module';
import { NewShiftComponent } from './new-shift/new-shift.component';
import { ExistingShiftComponent } from './existing-shift/existing-shift.component';
import { ShiftLoginComponent } from './shift-login/shift-login.component';
import { ActiveSessionsComponent } from './active-sessions/active-sessions.component';
import { EndShiftComponent } from './end-shift/end-shift.component';

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    NewShiftComponent,
    ExistingShiftComponent,
    ShiftLoginComponent,
    ActiveSessionsComponent,
    EndShiftComponent
  ],
  imports: [
    CommonModule,
    AccountRoutingModule,
    SharedModule
  ]
})
export class AccountModule { }