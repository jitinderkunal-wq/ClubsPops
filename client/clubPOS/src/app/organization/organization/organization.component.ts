import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { OrganizationService } from '../organization.sevice';
import { Organization } from 'app/shared/models/organization';
import { Router } from '@angular/router';
import { environment } from 'environments/environment';

@Component({
  selector: 'app-organization',
  templateUrl: './organization.component.html',
  styleUrls: ['./organization.component.scss']
})
export class OrganizationComponent implements OnInit{

  selectedFile: File | null = null;
  organizations: Organization[] = [];
  organization: any;
  filePathApi: string = environment.logoPathApi;
  isList: boolean = true;
  isSuperAdmin: boolean = false;

  constructor(private fb: FormBuilder, public organizationService: OrganizationService, private router: Router){}

  complexPassword = "(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$";


  ngOnInit(): void {
    this.getOrganizations();
  }

  getOrganizations(){
    this.organizationService.getOrganizations().subscribe({
      next: response => {
        this.organizations = response;
      },
      error: error => console.log(error),
    })
  }

  registerForm = this.fb.group({
    organizationId: [''],
    organizationName: ['', Validators.required],
    street: ['', Validators.required],
    city: ['', Validators.required],
    province: ['', Validators.required],
    postalCode: ['', Validators.required],
    country: ['', Validators.required],
    registrationNumber: ['', Validators.required],
    logo: ['', Validators.required],
    isActive: [true]
  });

  onFileSelected(event:any){
    this.selectedFile = event.target.files[0];
  }

  onSubmit(){
    // Update Organization
    if(this.registerForm.value.organizationId){
      if (this.selectedFile) {
        const formData = new FormData();
        formData.append('file', this.selectedFile, this.selectedFile.name);
        this.organizationService.uploadLogo(formData).subscribe({
          next: (response)=>{
            console.log(response);
          },
          error: error=> console.log(error),
        });
        this.registerForm.value.logo = this.selectedFile?.name;
      }
      this.organizationService.editOrganization(this.registerForm.value).subscribe({
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
    // Create Organization
    else{
      if(this.registerForm.valid){
        if (this.selectedFile) {
          const formData = new FormData();
          formData.append('file', this.selectedFile, this.selectedFile.name);
          console.log(formData);
    
          this.organizationService.uploadLogo(formData).subscribe({
            next: ()=>{
              this.registerForm.reset();
              this.isList = true;
              window.location.reload();
            },
            error: error=> console.log(error),
          });
          this.registerForm.value.logo = this.selectedFile?.name;
        }
    
        this.organizationService.createOrganization(this.registerForm.value).subscribe({
          next: () =>{
            this.registerForm.reset();
            this.isList = true;
            window.location.reload();
          },
          error: error=> console.log(error),
        });
      }
    }
  }

  showRegistrationForm(){
    this.isList = !this.isList;
    this.isSuperAdmin = false;
    this.registerForm.patchValue({isActive: true});
  }

  addOrganization(organizationId: number){
    this.isList =false;
    this.router.navigate(['/organization/admin', {organizationId}]);
  }

  editOrganization(organizationId: number){
    this.isList = false;
    organizationId == 1 ? this.isSuperAdmin = true : this.isSuperAdmin = false;
    this.registerForm.reset();
    this.organizationService.getOrganizationById(organizationId).subscribe({
      next: response => {
        this.organization = response;
        this.registerForm.patchValue({
          organizationId: this.organization.organizationId,
          organizationName: this.organization.organizationName,
          street: this.organization.street,
          city: this.organization.city, 
          province: this.organization.province,
          postalCode: this.organization.postalCode,
          country: this.organization.country,
          registrationNumber: this.organization.registrationNumber,
          isActive: this.organization.isActive,
          logo: this.organization.logo,
        });
      },
      error:(err) => {
        console.log(err);
      },
    });
  }

  // If Organization will deleted then Users of that organization should be deleted automatically.
  // delete(organizationId: number){
  //   this.isList = false;
  //   this.organizationService.deleteOrganization(organizationId).subscribe({
  //     next: response => {
  //       if(response) {
  //         this.isList = true;
  //         window.location.reload();
  //       }
  //     },
  //     error:(err) => {
  //       console.log(err);
  //     },
  //   });
  // }
}
