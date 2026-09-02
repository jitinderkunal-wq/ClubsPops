import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';
import { EditUser } from 'app/shared/models/editUser';
import { DatePipe } from '@angular/common';
import { Role } from 'app/shared/models/role';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  currentorganizationId: number = 0;
  users: EditUser[] = [];
  errors: string[] | null = null;
  saveButtonText: string = 'Create';
  roles?: Role[];
  superAdminUserId: string = '';

  constructor(private fb: FormBuilder, private accountService: AccountService, private router: Router, private datePipe: DatePipe) { }

  ngOnInit(): void {
    this.loadCurrentUser();
    this.getRoles();
    this.getUsers();
  }

  complexPassword = "(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$";

  registerForm = this.fb.group({
    userId: [null],
    firstName: ['', Validators.required],
    lastName: [''],
    payRate: [0],
    hireDate: ['', Validators.required],
    terminationDate: [null],
    sin: [0],
    isActive: [true],
    isSalary: [false],
    inTrainingMode: [false],
    organizationId: [0],
    role: [''],
    email: ['', [Validators.required, Validators.email]],
    password: [null, [Validators.required, Validators.pattern(this.complexPassword)]],
    confirmPassword: [null, [Validators.required, Validators.pattern(this.complexPassword)]],
    street: [''],
    city: ['', Validators.required],
    province: ['', Validators.required],
    postalCode: ['', Validators.required],
    cellPhone: [''],
  });

  getUsers() {
    // const token = localStorage.getItem('token');
    this.accountService.getUsers().subscribe({
      next: (response) => {
        this.users = response.filter(x => x.organizationId === this.currentorganizationId && x.role  !== 'SuperAdmin');
      },
      error: error => console.log(error)
    });
  }

  loadCurrentUser(){
    this.accountService.currentUser$.subscribe({
      next: user => {
        this.currentorganizationId = user? user.organizationId: 0;
      },
      error: error => console.log(error)
    });
  }
  

  getRoles() {
    this.accountService.getRoles().subscribe({
      next: (response) => {
        this.roles = response.filter(x => x.roleName !== 'SuperAdmin');
      },
      error: error => console.log(error)
    });
  }

  onUserSelectionChange(selectedUser: any) {
    // console.log(selectedUser);
    this.saveButtonText = 'Update';

    // Format the date before patching it into the form
    const formattedHireDate = this.datePipe.transform(selectedUser.hireDate, 'yyyy-MM-dd');

    this.registerForm.patchValue({
      userId: selectedUser.userId,
      firstName: selectedUser.firstName,
      lastName: selectedUser.lastName,
      payRate: selectedUser.payRate,
      hireDate: formattedHireDate, // Use the formatted date here
      terminationDate: selectedUser.terminationDate ? this.datePipe.transform(selectedUser.terminationDate, 'yyyy-MM-dd') : selectedUser.terminationDate,
      sin: selectedUser.sin,
      isActive: selectedUser.isActive,
      isSalary: selectedUser.isSalary,
      inTrainingMode: selectedUser.inTrainingMode,
      organizationId: selectedUser.organizationId,
      role: selectedUser.role,
      email: selectedUser.email,
      street: selectedUser.street,
      city: selectedUser.city,
      province: selectedUser.province,
      postalCode: selectedUser.postalCode,
      cellPhone: selectedUser.cellPhone,
    });
  }

  onSubmit() {
    if (this.registerForm.value.userId) {
      // console.log(this.registerForm.value);

      this.accountService.editUser(this.registerForm.value).subscribe({
        next: () => {
          this.registerForm.reset();
          this.router.navigateByUrl('/administrative')
        },
        error: error => {
          console.log(error);
          this.errors = error.errors
        }
      });
    } else {
      // console.log(this.registerForm.value);
      this.registerForm.value.organizationId = this.currentorganizationId;
      this.accountService.register(this.registerForm.value).subscribe({
        next: () => {
          this.registerForm.reset();
          this.router.navigateByUrl('/administrative')
        },
        error: error => {
          console.log(error);
          this.errors = error.errors
        }
      })
    }
  }
}