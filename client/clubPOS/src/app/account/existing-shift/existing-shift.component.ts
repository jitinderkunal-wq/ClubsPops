import { Component } from '@angular/core';
import { AccountService } from '../account.service';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { NewShiftComponent } from '../new-shift/new-shift.component';
import { ActiveSessionsComponent } from '../active-sessions/active-sessions.component';

@Component({
  selector: 'app-existing-shift',
  templateUrl: './existing-shift.component.html',
  styleUrls: ['./existing-shift.component.scss']
})
export class ExistingShiftComponent {
  dateToday: Date = new Date();
  currentUser: string = '';
  selectedShift: number = 0;

  constructor(private matDialog: MatDialog,private matDialogRef: MatDialogRef<ExistingShiftComponent>, private accountService: AccountService) { 
    
  }
  ngOnInit(): void {
    this.loadCurrentUser();
    this.autoShiftDrawerDetection();
  }

  loadCurrentUser(){
    this.accountService.currentUser$.subscribe({
      next: user => {
        this.currentUser = user? user.firstName: '';
      },
      error: error => console.log(error)
    });
    return this.currentUser;
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
    return this.selectedShift;
  }

  existingSession(){
    this.closePopup();
    this.matDialog.open(ActiveSessionsComponent,{});
  }

  newSession(){
    this.closePopup();
    this.matDialog.open(NewShiftComponent,{})
  }

  closePopup() {
    this.matDialogRef.close();
  }
}
