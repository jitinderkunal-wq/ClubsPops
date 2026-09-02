import { Component } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { AccountService } from '../account.service';
import { ActiveSalesSession } from 'app/shared/models/activeSalesSession';
import { Router } from '@angular/router';
import { ConfirmationDialogService } from 'app/shared/confirmationDialogService/confirmationDialogService';

@Component({
  selector: 'app-active-sessions',
  templateUrl: './active-sessions.component.html',
  styleUrls: ['./active-sessions.component.scss']
})
export class ActiveSessionsComponent {
  activeSalesSessions: ActiveSalesSession[] = [];
  user: any;
  currentUser: string = '';
  dateToday: Date = new Date();
  selectedShift: number = 0;
  selectedDrawer: number = 0;

  constructor(private accountService: AccountService, private matDialogRef: MatDialogRef<ActiveSessionsComponent>, private router: Router, private confirmationDialogService: ConfirmationDialogService) {
  }


  ngOnInit(): void {
    this.loadCurrentUser();
    this.getActiveSession();
    this.autoShiftDrawerDetection();
  }

  loadCurrentUser(){
    this.accountService.currentUser$.subscribe({
      next: user => {
        this.user = user;
        this.currentUser = user? user.firstName: '';
      },
      error: error => console.log(error)
    });
    return this.currentUser;
  }

  getActiveSession(){
    this.accountService.getActiveSession().subscribe({
      next: response => {
         this.activeSalesSessions = response;
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
    return this.selectedShift;
  }

  onRowClick(clickedSession: any) {
    const id = parseInt(clickedSession.shiftSessionId);
    this.accountService.startExistingSession(id).subscribe({
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
        this.closePopup();
        console.log(error);
      }
    });
    this.closePopup();
  }

  closePopup() {
    this.matDialogRef.close();
  }
}
