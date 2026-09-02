import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { NewShiftComponent } from '../new-shift/new-shift.component';
import { User } from 'app/shared/models/user';
import { ShiftLoginComponent } from '../shift-login/shift-login.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent {
  returnUrl: string;
  currentUser?: User | null;
  token?: string | null;


  constructor(private accountService: AccountService, private router: Router, private activatedRoute: ActivatedRoute, private matDialog: MatDialog) {
    this.returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '/dashboard'
  }

  complexPassword = "(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$";


  ngOnInit(): void {
    this.getCurrentUser();
    this.token = this.accountService.getToken();
  }

  getCurrentUser() {
    this.accountService.currentUser$.subscribe({
      next: user => {
        this.currentUser = user;
      },
      error: error => console.log(error)
    });
  }

  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.pattern(this.complexPassword)])
  })

  onSubmit() {
      this.accountService.login(this.loginForm.value).subscribe({
        next: () => {
            if(this.token){
              if(this.currentUser?.roles.includes('SuperAdmin')){
                this.router.navigateByUrl("/organization");
              }
              else if(this.currentUser?.roles.includes('Admin')){
                this.router.navigateByUrl("/dashboard");
              }
              else{
                this.router.navigateByUrl(this.returnUrl);
                if(this.returnUrl == "/dashboard")
                {
                  this.matDialog.open(ShiftLoginComponent,{});
                }
              }
            }
            else{
              if(this.currentUser?.roles.includes('SuperAdmin')){
                this.router.navigateByUrl("/organization");
              }
              else if(this.currentUser?.roles.includes('Admin')){
                this.router.navigateByUrl("/dashboard");
              }
              else{
                this.router.navigateByUrl(this.returnUrl);
                if(this.returnUrl == "/dashboard")
                {
                  this.matDialog.open(ShiftLoginComponent,{});
                }
              }
            } 
        },
        error: error => {
          console.log(error);
        }
      })
  }
}