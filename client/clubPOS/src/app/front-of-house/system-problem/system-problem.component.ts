import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { FrontOfHouseService } from '../front-of-house.service';
import { Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';


@Component({
  selector: 'app-system-problem',
  templateUrl: './system-problem.component.html',
  styleUrls: ['./system-problem.component.scss']
})
export class SystemProblemComponent {
  loading = false;

  constructor(private matDialogRef: MatDialogRef<SystemProblemComponent>, private fohService: FrontOfHouseService,
    private router: Router, private fb: FormBuilder) { }

  createSystemProblemForm = this.fb.group({
    problemExplaination: ['']
  });

  closePopup() {
    this.matDialogRef.close();
  }

  onSubmit() {
    this.loading = true;
    const problemCategoryId = 1;
    const problemExplaination = this.createSystemProblemForm.value.problemExplaination;
    const employeeName = 'bob';

    const requestBody = {
      problemCategoryId,
      problemExplaination,
      employeeName
    };

    this.fohService.createSystemProblem(requestBody).subscribe({
      next: response => {
        console.log('System problem created', response);
        this.loading = false;
        this.router.navigateByUrl('/accommodations');
        this.closePopup();
      },
      error: error => {
        console.error('Failed to create system problem', error);
        this.loading = false;
      }
    });
  }
}
