import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AdministrativeService } from '../administrative.service';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-taxes-and-exchange',
  templateUrl: './taxes-and-exchange.component.html',
  styleUrls: ['./taxes-and-exchange.component.scss']
})
export class TaxesAndExchangeComponent {

  constructor(private matDialogRef: MatDialogRef<TaxesAndExchangeComponent>, private administrativeService: AdministrativeService, private fb: FormBuilder) { }

  taxAndExchangeForm = this.fb.group({
    gst: [0],
    hst: [0],
    us: [0],
    euro: [0]
  })

  ngOnInit(): void {
    this.getTaxAndExchange();
  }

onSubmit() {
  const gst = this.taxAndExchangeForm.value.gst;
  const hst = this.taxAndExchangeForm.value.hst;
  const usExchange = this.taxAndExchangeForm.value.us;
  const euroExchange = this.taxAndExchangeForm.value.euro;

  const requestBody={
    gst,
    hst,
    usExchange,
    euroExchange
  }

  this.administrativeService.updateTaxAndExchangeRate(requestBody).subscribe({
    next: response => {
      console.log('Saved Successfully', response);
    },
    error: error => {
      console.error('Failed to save', error);
    }
  })

  this.closePopup();
  }

  getTaxAndExchange(){
    this.administrativeService.getTaxAndExchangeRate().subscribe({
      next: response => {
        this.taxAndExchangeForm.patchValue({
          gst: response.gst,
          hst: response.hst,
          us: response.usExchange,
          euro: response.euroExchange
        });
      },
      error: error => console.log(error)
    });
  }

  closePopup() {
    this.matDialogRef.close();
  }
}