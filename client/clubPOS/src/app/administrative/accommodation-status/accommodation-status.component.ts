import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { AccommodationStatus } from 'app/shared/models/accommodationStatus';
import { AdministrativeService } from '../administrative.service';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-accommodation-status',
  templateUrl: './accommodation-status.component.html',
  styleUrls: ['./accommodation-status.component.scss']
})
export class AccommodationStatusComponent {
  accommodationStatuses: AccommodationStatus[] = [];
  saveButtonText: string = 'Create';
  errors: string[] | null = null;

  constructor(private matDialogRef: MatDialogRef<AccommodationStatusComponent>, private administrativeService: AdministrativeService, private fb: FormBuilder) { }

  accommodationStatusForm = this.fb.group({
    accommodationStatusId: [0],
    statusName: ['', Validators.required],
    isActive: [true],
  });

  ngOnInit(): void {
    this.getAccommodationStatuses();
  }

  closePopup() {
    this.matDialogRef.close();
  }

  getAccommodationStatuses() {
    this.administrativeService.getAccommodationStatuses().subscribe({
      next: response => this.accommodationStatuses = response,
      error: error => console.log(error)
    });
  }

  onAccommodationStatusChange(selectedAccommodationStatus: any) {
    this.saveButtonText = 'Edit';
    this.accommodationStatusForm.patchValue({
      accommodationStatusId: selectedAccommodationStatus.accommodationStatusId,
      statusName: selectedAccommodationStatus.statusName,
      isActive: selectedAccommodationStatus.isActive
    });
  }

  onSubmit() {
    if (this.accommodationStatusForm.value.accommodationStatusId) {
      this.administrativeService.updateAccommodationStatus(this.accommodationStatusForm.value).subscribe({
        next: () => {
          this.accommodationStatusForm.reset();
          this.closePopup();
        },
        error: error => {
          console.log(error);
          this.errors = error.errors
        }
      });
    } else {
      this.administrativeService.createAccommodationStatus(this.accommodationStatusForm.value).subscribe({
        next: () => {
          this.accommodationStatusForm.reset();
          this.closePopup();
        },
        error: error => {
          console.log(error);
          this.errors = error.errors
        }
      });
    }
  }
}
