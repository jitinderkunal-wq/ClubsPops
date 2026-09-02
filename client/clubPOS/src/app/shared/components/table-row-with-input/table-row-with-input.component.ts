import { Component, Input, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';

@Component({
  selector: 'app-table-row-with-input',
  templateUrl: './table-row-with-input.component.html',
  styleUrls: ['./table-row-with-input.component.scss']
})
export class TableRowWithInputComponent implements ControlValueAccessor {
  @Input() type = 'text';
  @Input() label = '';
  @Input() valueChangeFunction: (newValue: number) => void = () => {};
  @Input() customStyles: { [key: string]: string } = {};

  constructor(@Self() public controlDir: NgControl) {
    controlDir.valueAccessor = this;
  }

  writeValue(obj: any): void { }
  registerOnChange(fn: any): void { }
  registerOnTouched(fn: any): void { }

  get control(): FormControl {
    return this.controlDir.control as FormControl
  }
  
  onInputChange(event: any) {
    // Convert the input element to a number and pass it to the parent function
    const newValue = parseInt((event.target as HTMLInputElement).value);
    
    this.valueChangeFunction(isNaN(newValue) ? 0 : newValue);

    if (newValue !== 0) {
      setTimeout(() => {
        this.control.disable();
      }, 1000); // Delay in milliseconds (e.g., 1000 ms = 1 second)
    }
  }
}