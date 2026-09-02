import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { MatDialogRef } from '@angular/material/dialog';
import { Shift } from 'app/shared/models/shift';
import { Router } from '@angular/router';
import { ConfirmationDialogService } from 'app/shared/confirmationDialogService/confirmationDialogService';

@Component({
  selector: 'app-new-shift',
  templateUrl: './new-shift.component.html',
  styleUrls: ['./new-shift.component.scss']
})
export class NewShiftComponent {

  dateToday: Date = new Date();
  user: any;
  currentUser: string = '';
  currentUserId: string = '';
  shifts: Shift[] = [];
  selectedShift: number = 0;
  selectedDrawer: number = 0;
  drawers = [{ id: 1, drawer: 1 },
              { id: 2, drawer: 2 }];

  

  constructor(private matDialogRef: MatDialogRef<NewShiftComponent>, private accountService: AccountService, private fb: FormBuilder, private router: Router, private confirmationDialogService: ConfirmationDialogService) { }

  newShiftForm = this.fb.group({
    shiftId: [0, Validators.required],
    drawer: [0, Validators.required]
  });

  ngOnInit(): void {
    this.getShifts();
    this.loadCurrentUser();
    this.autoShiftDrawerDetection();
  }

  loadCurrentUser(){
    this.accountService.currentUser$.subscribe({
      next: user => {
        this.user = user;
        this.currentUserId = user? user.userId:'';
        this.currentUser = user? user.firstName:'';
      },
      error: error => console.log(error)
    });
    return this.currentUserId;
  }

  getShifts() {
    this.accountService.getShifts().subscribe({
      next: response => {
        this.shifts = response;
      },
      error: error => console.log(error)
    });
  }

  autoShiftDrawerDetection(){
    let now = new Date();
    let hours = parseInt(("0" + now.getHours()).slice(-2));
    let minutes = parseInt(("0" + now.getMinutes()).slice(-2));

    if (hours>=0 && hours<=7 && minutes<=59) {
      this.selectedShift = 1;
    } 
    else if (hours>=8 && hours<=15 && minutes<=59) {
      this.selectedShift = 2;
    }   
    else{
      this.selectedShift = 3;
    }
     
    if(this.selectedDrawer == 0) this.selectedDrawer = 1;
    this.newShiftForm.patchValue({shiftId: this.selectedShift, drawer: this.selectedDrawer});
    return this.selectedShift;
  }

  onShiftChange(selectedShift: any) {
    this.selectedShift = selectedShift.shiftNumber;
  }

  onDrawerChange(selectedDrawer: any) {    
    this.selectedDrawer = selectedDrawer.drawer;
  }

  onSubmit() {

    const requestBody={
      userId : this.currentUserId,
      shiftNumber : this.selectedShift,
      drawerNumber  : this.selectedDrawer
    }
    this.accountService.startNewSession(requestBody).subscribe({
      next: () => {
        this.closePopup();
        switch(true) {
          case this.user.roles.includes('FrontCash'):
            this.router.navigateByUrl('/accommodations');
            break;
          case this.user.roles.includes('Cleaner'):
            this.router.navigateByUrl('/cleaning');
            break;
          case this.user.roles.includes('Admin'):
            this.router.navigateByUrl('/administrative');
            break;
          default:
            this.confirmationDialogService.warningDialog("Sorry!, You are not Authorized for this Role");
        }
      },
      error: error => {
        console.log(error);
      }
    })
  }

  closePopup() {
    this.matDialogRef.close();
  }
}
