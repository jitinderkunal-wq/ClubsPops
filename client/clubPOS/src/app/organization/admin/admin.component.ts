import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AccountService } from 'app/account/account.service';
import { OrganizationService } from '../organization.sevice';
import { formatDate } from '@angular/common';
import { environment } from 'environments/environment';

@Component({
  selector: 'app-manager',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent {

  organizationId: any;
  organizationLogo: string = '';
  organizationName: string = '';
  filePathApi: string = environment.logoPathApi;
  isList : boolean = true;
  users : any;
  user : any;
  formButtonText: string = 'Create';


  constructor(private route: ActivatedRoute, private organizationService: OrganizationService, private accountService: AccountService, private fb: FormBuilder){}
  
  complexPassword = "(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$";

  ngOnInit(): void {
    this.getCurrentOrganization();
    this.getUsers();
  }

  getCurrentOrganization(){
    this.organizationId = this.route.snapshot.paramMap.get('organizationId');
    this.accountService.currentOrganization(this.organizationId).subscribe({
      next: response => {
        this.organizationLogo = this.filePathApi+response.logo!;
        this.organizationName = response.organizationName!;
      },
      error: error => console.log(error)
    });
  }

  getUsers(userId?: string){
    this.organizationId = this.route.snapshot.paramMap.get('organizationId');
    if (userId) {
      this.organizationService.getAdmins(this.organizationId).subscribe({
        next: response => {
          if (response) {
            this.user = response.find(x=>x.userId === userId);
            this.registerForm.patchValue({
              userId: this.user.userId,
              firstName: this.user.firstName,
              lastName: this.user.lastName,
              payRate: this.user.payRate,
              hireDate: formatDate(this.user.hireDate, 'yyyy-MM-dd', 'en-US'), // Use the formatted date here
              terminationDate: this.user.terminationDate ? formatDate(this.user.terminationDate, 'yyyy-MM-dd', 'en-US') : this.user.terminationDate, // Use the formatted date here
              sin: this.user.sin,
              isActive: this.user.isActive,
              isSalary: this.user.isSalary,
              inTrainingMode: this.user.inTrainingMode,
              organizationId: this.user.organizationId,
              role: this.user.role,
              email: this.user.email,
              street: this.user.street,
              city: this.user.city,
              province: this.user.province,
              postalCode: this.user.postalCode,
              cellPhone: this.user.cellPhone,
            });
          }
        }
      });
    } else {
      this.organizationService.getAdmins(this.organizationId).subscribe({
        next: response => {
          this.users = response;
        }
      });
    }
    
  }

  registerForm = this.fb.group({
    userId: [null],
    firstName: ['', Validators.required],
    lastName: [''],
    payRate: [0],
    hireDate: ['', Validators.required],
    terminationDate: [null],
    sin: [],
    isActive: [true],
    isSalary: [false],
    inTrainingMode: [false],
    organizationId: [0],
    role: ['Admin'],
    email: ['', [Validators.required, Validators.email]],
    password: [null, [Validators.required, Validators.pattern(this.complexPassword)]],
    confirmPassword: [null, [Validators.required, Validators.pattern(this.complexPassword)]],
    street: [''],
    city: ['', Validators.required],
    province: ['', Validators.required],
    postalCode: ['', Validators.required],
    cellPhone: [''],
  });

  onSubmit(){
    // Update User
    if(this.registerForm.value.userId){
      this.accountService.editUser(this.registerForm.value).subscribe({
        next: () => {
          this.registerForm.reset();
          this.isList = true;
          window.location.reload();
        },
        error: error => {
          console.log(error);
        }
      });
    }
    // Create User
    else{
      this.registerForm.value.organizationId = this.organizationId ? this.organizationId: 0;
      this.accountService.register(this.registerForm.value).subscribe({
        next: () =>{
          this.registerForm.reset();
          this.isList = true;
          window.location.reload();
        },
        error: error=> console.log(error),
      });
    }
  }

  showRegistrationForm(){
    this.isList = !this.isList;
    this.registerForm.patchValue({isActive: true, isSalary: false, inTrainingMode: false});
  }

  edit(userId: string){
    this.isList = false;
    this.formButtonText = 'Update';
    this.getUsers(userId);
  }

  // delete(userId: string){
  //   // this.isList = false;
  //   // this.organizationService.deleteOrganization(organizationId).subscribe({
  //   //   next: response => {
  //   //     if(response) {
  //   //       this.isList = true;
  //   //       window.location.reload();
  //   //     }
  //   //   },
  //   //   error:(err) => {
  //   //     console.log(err);
  //   //   },
  //   // });
  // }
}
