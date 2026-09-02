import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { AdministrativeService } from '../administrative.service';
import { FormBuilder, Validators } from '@angular/forms';
import { AccommodationRate } from 'app/shared/models/accommodationRate';
import { AccommodationType } from 'app/shared/models/accommodationType';
import { Tax } from 'app/shared/models/tax';

@Component({
  selector: 'app-accommodation-rate-setup',
  templateUrl: './accommodation-rate-setup.component.html',
  styleUrls: ['./accommodation-rate-setup.component.scss']
})
export class AccommodationRateSetupComponent implements OnInit {
  accommodationRates: AccommodationRate[] = [];
  accommodationTypes: AccommodationType[] = [];
  tax?: Tax;
  saveButtonText: string = 'Create';
  errors: string[] | null = null;

  days = [
    { label: "Monday", value: "Monday" },
    { label: "Tuesday", value: "Tuesday" },
    { label: "Wednesday", value: "Wednesday" },
    { label: "Thursday", value: "Thursday" },
    { label: "Friday", value: "Friday" },
    { label: "Saturday", value: "Saturday" },
    { label: "Sunday", value: "Sunday" }
  ]

  constructor(private matDialogRef: MatDialogRef<AccommodationRateSetupComponent>, private administrativeService: AdministrativeService,
    private fb: FormBuilder) { }

  accommodationRateSetupForm = this.fb.group({
    accommodationRateId: [0],
    accommodationTypeId: [1],
    hours: [0],
    rateName: ['', Validators.required],
    price: [0],
    startDayOfTheWeek: [''],
    rateStartTime: [''],
    endDayOfWeek: [''],
    rateEndTime: [''],
    tax1: [3],
    tax2: [null],
    tax3: [null],
    tax4: [null],
    tax5: [null],
    isStandardRate: [true],
    totalPrice: [0, Validators.required],
    isActive: [true],
  });

  ngOnInit(): void {
    this.getAccommodationTypes();
    this.getAccommodationRates();
    this.getTaxByTaxId();
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

  getAccommodationRates() {
    this.administrativeService.getAccommodationRates().subscribe({
      next: response => this.accommodationRates = response,
      error: error => console.log(error)
    });
  }

  getTaxByTaxId() {
    this.administrativeService.getTaxByTaxId(this.accommodationRateSetupForm.value.tax1!).subscribe({
      next: response => {
        this.tax = response;
        console.log(this.tax);
      },
      error: error => {
        console.log(error);
      }
    });
  }

  onAccommodationTypeChange(selectedAccommodationType: any) {
    // console.log(selectedAccommodationType);

    this.accommodationRateSetupForm.patchValue({
      accommodationTypeId: selectedAccommodationType.accommodationTypeId,
    });
  }

  onTotalPriceInputValueChange(input: any) {
    var price = 0;
    if (this.tax) {
      price = input / (1 + this.tax?.taxRate * 0.01);
    }
    this.accommodationRateSetupForm.patchValue({
      price: parseFloat(price.toFixed(2)),
    });
  }

  onAccommodationRateChange(selectedAccommodationRate: any) {
    // console.log(selectedAccommodationRate);
    this.saveButtonText = 'Edit';

    this.accommodationRateSetupForm.patchValue({
      accommodationRateId: selectedAccommodationRate.accommodationRateId,
      accommodationTypeId: selectedAccommodationRate.accommodationTypeId,
      hours: selectedAccommodationRate.hours,
      rateName: selectedAccommodationRate.rateName,
      price: selectedAccommodationRate.price,
      startDayOfTheWeek: selectedAccommodationRate.startDayOfTheWeek,
      rateStartTime: selectedAccommodationRate.rateStartTime,
      endDayOfWeek: selectedAccommodationRate.endDayOfWeek,
      rateEndTime: selectedAccommodationRate.rateEndTime,
      tax1: selectedAccommodationRate.tax1,
      tax2: selectedAccommodationRate.tax2,
      tax3: selectedAccommodationRate.tax3,
      tax4: selectedAccommodationRate.tax4,
      tax5: selectedAccommodationRate.tax5,
      isStandardRate: selectedAccommodationRate.isStandardRate,
      totalPrice: selectedAccommodationRate.totalPrice,
      isActive: selectedAccommodationRate.isActive,
    });
  }

  onSubmit() {
    if (this.accommodationRateSetupForm.value.accommodationRateId) {
      // console.log(this.accommodationRateSetupForm.value);
      // console.log("Edit Accommodation Rate");

      this.administrativeService.editAccommodationRate(this.accommodationRateSetupForm.value).subscribe({
        next: () => {
          this.accommodationRateSetupForm.reset();
          this.closePopup();
        },
        error: error => {
          console.log(error);
          this.errors = error.errors
        }
      });
    } else {
      // console.log(this.accommodationRateSetupForm.value);
      // console.log("Create Accommodation Rate");

      this.administrativeService.createAccommodationRate(this.accommodationRateSetupForm.value).subscribe({
        next: () => {
          this.accommodationRateSetupForm.reset();
          this.closePopup();
        },
        error: error => {
          console.log(error); ``
          this.errors = error.errors
        }
      });
    }
  }
}