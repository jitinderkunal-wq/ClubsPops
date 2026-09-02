import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'app/account/account.service';
import { User } from 'app/shared/models/user';

@Component({
  selector: 'app-organization-dashboard',
  templateUrl: './organization-dashboard.component.html',
  styleUrls: ['./organization-dashboard.component.scss']
})
export class OrganizationDashboardComponent {
  currentUser?: User | null;

  constructor(private router: Router, public accountService: AccountService) { }
  
  ngOnInit(): void {
    this.getCurrentUser();
  }
  
  getCurrentUser() {
    this.accountService.currentUser$.subscribe({
      next: user => {
        this.currentUser = user;
      },
      error: error => console.log(error)
    });
  }

  openOrganization(){
    if (this.currentUser?.roles.includes('SuperAdmin')) {
      this.router.navigateByUrl('/organization/organizations');
    }
    else{
      alert("Sorry!, You are not Authorized for this Role");
    }
  }
}
