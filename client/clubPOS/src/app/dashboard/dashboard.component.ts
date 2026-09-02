import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { AccountService } from 'app/account/account.service';
import { ShiftLoginComponent } from 'app/account/shift-login/shift-login.component';
import { Role } from 'app/shared/models/role';
import { User } from 'app/shared/models/user';
import { ConfirmationDialogService } from 'app/shared/confirmationDialogService/confirmationDialogService';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})

export class DashboardComponent implements OnInit {
  currentUser?: User | null;
  isShiftActive: boolean = false;

  constructor(public accountService: AccountService, private router: Router, private matDialog: MatDialog, private confirmationDialogService: ConfirmationDialogService) { }
  
  ngOnInit(): void {
    this.getCurrentUser();
    this.checkIfShiftActive();
  }

  getCurrentUser() {
    this.accountService.currentUser$.subscribe({
      next: user => {
        this.currentUser = user;
      },
      error: error => console.log(error)
    });
  }

  checkIfShiftActive(){
    var isShiftLoggedIn = this.accountService.checkIfShiftLoggedIn();
    if(isShiftLoggedIn == true){
      this.isShiftActive = true
    }
  }

  openModule(role: string, destinationRoute: string, permissionMessage: string) {
    if (this.currentUser?.roles.includes(role)) {
      // Check if shift is not active then open ShiftLoginComponent
      if (!this.isShiftActive) {
        this.matDialog.open(ShiftLoginComponent, {});
      } else {
        this.router.navigateByUrl(destinationRoute);
      }
    } else {
      this.confirmationDialogService.warningDialog(`Sorry!, You do not have permission for ${permissionMessage}.`);
    }
  }

  openFrontOfHouseModule() {
    this.openModule('FrontCash', '/accommodations', 'Front Cash');
  }
  
  openCleaningModule() {
    this.openModule('Cleaner', '/cleaning', 'Cleaner');
  }

  openMaintenanceModule() {
    this.openModule('Cleaner', '/maintenance', 'Maintenance');
  }
  
  // openAdministrativeModule() {
  //   this.openModule('Admin', '/administrative', 'Admin');
  // }

  openAdministrativeModule(){
    if (this.currentUser?.roles.includes('Admin')) {
      this.router.navigateByUrl('/administrative');
    }
    else{
      this.confirmationDialogService.warningDialog("Sorry!, You have not permission for Admin.");
    }
  }    

  // openFrontOfHouseModule(){
  //   if (this.currentUser?.roles.includes('FrontCash')) {
  //     // have to check here if shift is not active then open  ShiftLoginComponent
  //     if(!this.isShiftActive){
  //       this.matDialog.open(ShiftLoginComponent,{});
  //     }
  //     else{
  //       this.router.navigateByUrl('/accommodations');
  //     }
  //   }
  //   else{
  //     this.confirmationDialogService.warningDialog("Sorry!, You have not permission for Front Cash.");
  //   }
  // }

  // openCleaningModule(){
  //   if (this.currentUser?.roles.includes('Cleaner')) {
  //     // have to check here if shift is not active then open  ShiftLoginComponent
  //     if(!this.isShiftActive){
  //       this.matDialog.open(ShiftLoginComponent,{});
  //     }
  //     else{
  //       this.router.navigateByUrl('/cleaning');
  //     }
  //   }
  //   else{
  //     this.confirmationDialogService.warningDialog("Sorry!, You have not permission for Cleaner.");
  //   }
  // }

  // openMaintenanceModule(){
  //   if (this.currentUser?.roles.includes('Cleaner')) {
  //     this.router.navigateByUrl('/maintenance');
  //   }
  //   else{
  //     this.confirmationDialogService.warningDialog("Sorry!, You have not permission for Maintenance.");
  //   }
  // }

  // openAdministrativeModule(){
  //   if (this.currentUser?.roles.includes('Admin')) {
  //     this.router.navigateByUrl('/administrative');
  //   }
  //   else{
  //     this.confirmationDialogService.warningDialog("Sorry!, You have not permission for Admin.");
  //   }
  // }

  openShiftPopup(){
    this.matDialog.open(ShiftLoginComponent,{});
  }
}