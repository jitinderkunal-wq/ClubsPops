import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {MatDialog, MatDialogRef } from '@angular/material/dialog';
import { AccountService } from '../account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from 'app/shared/models/user';
import { ExistingShiftComponent } from '../existing-shift/existing-shift.component';
import { NewShiftComponent } from '../new-shift/new-shift.component';
import { ActiveSalesSession } from 'app/shared/models/activeSalesSession';
// import { NewShiftComponent } from '../new-shift/new-shift.component';

@Component({
  selector: 'app-shift-login',
  templateUrl: './shift-login.component.html',
  styleUrls: ['./shift-login.component.scss']
})
export class ShiftLoginComponent {

  currentUser: string = '';
  shiftSessionId: number = 0;
  user: any;
  activeSalesSession: ActiveSalesSession[] = [];
  
  constructor(private accountService: AccountService, private router: Router, private activatedRoute: ActivatedRoute, private matDialog: MatDialog, private matDialogRef: MatDialogRef<ShiftLoginComponent>) {
  }

  complexPassword = "(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$";


  ngOnInit(): void {
    this.getCurrentUser();
    this.getActiveSession();
  }

  getCurrentUser() {
    this.accountService.currentUser$.subscribe({
      next: user => {
        this.currentUser = user? user.firstName: '';
      },
      error: error => console.log(error)
    });
  }

  getActiveSession(){
    this.accountService.getActiveSession().subscribe({
      next: response =>{
        this.activeSalesSession = response;
      },
      error: error => console.log(error)
    })
  }

  shiftloginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.pattern(this.complexPassword)])
  })

  onSubmit() {
    this.getActiveSession();
    this.accountService.login(this.shiftloginForm.value).subscribe({
      next: () => {
        if(this.activeSalesSession.length >= 1){
          this.matDialog.open(ExistingShiftComponent,{});
        }
        else if(this.activeSalesSession.length == 0){
          this.matDialog.open(NewShiftComponent,{});
        }
        this.closePopup();
      },
      error: error => console.log(error)
    })
  }
  
  closePopup() {
    this.matDialogRef.close();
  }
}
