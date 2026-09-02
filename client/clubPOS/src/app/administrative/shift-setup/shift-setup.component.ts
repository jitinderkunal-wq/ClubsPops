import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { AdministrativeService } from '../administrative.service';
import { FormBuilder, Validators } from '@angular/forms';
import { Shift } from 'app/shared/models/shift';

@Component({
  selector: 'app-shift-setup',
  templateUrl: './shift-setup.component.html',
  styleUrls: ['./shift-setup.component.scss']
})
export class ShiftSetupComponent {
  shifts: Shift[] = [];
  shiftCreateId: number = 0;
  saveButtonText: string = 'Create';
  shiftStart: string = '';
  shiftEnd: string = '';
  errors = '';
  

  constructor(private matDialogRef: MatDialogRef<ShiftSetupComponent>, private administrativeService: AdministrativeService, private fb: FormBuilder) { }

  shiftSetupForm = this.fb.group({
    shiftId: [0],
    shiftNumber: ['', Validators.required],
    shiftStart: ['', Validators.required],
    shiftEnd: ['', Validators.required],
    isActive: [true]
  });

  ngOnInit(): void {
    this.getShifts();
    this.startshiftEnd();
  }

  startshiftEnd(){
    let now = new Date();
    let hours = ("0" + now.getHours()).slice(-2);
    let minutes = ("0" + now.getMinutes()).slice(-2);
    let str = hours + ':' + minutes;
    this.shiftStart = str;

    let  end = new Date(new Date().setHours(new Date().getHours() + 6));
    let endHours = ("0" + end.getHours()).slice(-2);
    let endMinutes = ("0" + end.getMinutes()).slice(-2);
    let shiftEnd = endHours + ':' + endMinutes;
    this.shiftEnd = shiftEnd;

    this.shiftSetupForm.patchValue({shiftStart: this.shiftStart, shiftEnd : this.shiftEnd});
  }

  getShifts() {
    this.administrativeService.getShifts().subscribe({
      next: response => {
        this.shifts = response;
      },
      error: error => console.log(error)
    });
  }

  onShiftChange(selectedshift: any) {
    console.log(selectedshift);
    this.saveButtonText = 'Update';

    this.shiftCreateId = selectedshift.shiftId;

    this.shiftSetupForm.patchValue({
      shiftId: selectedshift.shiftId,
      shiftNumber: selectedshift.shiftNumber,
      shiftStart: selectedshift.shiftStart,
      shiftEnd: selectedshift.shiftEnd,
      isActive: selectedshift.isActive
    });
  }

  onSubmit() {
    if (this.shiftSetupForm.value.shiftId) {
      console.log(this.shiftSetupForm.value);
      console.log("Edit shift");

      this.administrativeService.updateShift(this.shiftSetupForm.value).subscribe({
        next: () => {
          this.shiftSetupForm.reset();
          this.closePopup();
        },
        error: error => {
          console.log(error);
          this.errors = error.errors
        }
      });
    } else {
      console.log(this.shiftSetupForm.value);
      console.log("Create shift");

      this.administrativeService.createShift(this.shiftSetupForm.value).subscribe({
        next: () => {
          this.shiftSetupForm.reset();
          this.closePopup();
        },
        error: error => {
          console.log(error);
          this.errors = error.errors
        }
      });
    }
  }

  closePopup() {
    this.matDialogRef.close();
  }
}
