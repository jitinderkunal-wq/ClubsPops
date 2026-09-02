import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { AdministrativeService } from '../administrative.service';
import { FormBuilder, Validators } from '@angular/forms';
import { AccommodationType } from 'app/shared/models/accommodationType';

@Component({
  selector: 'app-accommodation-type',
  templateUrl: './accommodation-type.component.html',
  styleUrls: ['./accommodation-type.component.scss']
})
export class AccommodationTypeComponent {
  accommodationTypes: AccommodationType[] = [];
  saveButtonText: string = 'Create';
  errors: string[] | null = null;

  constructor(private matDialogRef: MatDialogRef<AccommodationTypeComponent>, private administrativeService: AdministrativeService, private fb: FormBuilder) { }

  accommodationTypeForm = this.fb.group({
    accommodationTypeId: [0],
    typeName: ['', Validators.required],
    maxOccupants: [1],
    compatibilityValue: [1],
    isActive: [true],
  });

  ngOnInit(): void {
    this.getAccommodationTypes();
  }

  closePopup() {
    this.matDialogRef.close();
  }

  getAccommodationTypes() {
    this.administrativeService.getAccommodationTypes().subscribe({
      next: response => this.accommodationTypes = response,
      error: error => console.log(error)
    });
  }

  onAccommodationTypeChange(selectedAccommodationType: any) {
    this.saveButtonText = 'Edit';
    this.accommodationTypeForm.patchValue({
      accommodationTypeId: selectedAccommodationType.accommodationTypeId,
      typeName: selectedAccommodationType.typeName,
      maxOccupants: selectedAccommodationType.maxOccupants,
      compatibilityValue: selectedAccommodationType.compatibilityValue,
      isActive: selectedAccommodationType.isActive
    });
  }

  onSubmit() {
    if (this.accommodationTypeForm.value.accommodationTypeId) {
      this.administrativeService.updateAccommodationType(this.accommodationTypeForm.value).subscribe({
        next: () => {
          this.accommodationTypeForm.reset();
          this.closePopup();
        },
        error: error => {
          console.log(error);
          this.errors = error.errors
        }
      });
    } else {
      this.administrativeService.createAccommodationType(this.accommodationTypeForm.value).subscribe({
        next: () => {
          this.accommodationTypeForm.reset();
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
