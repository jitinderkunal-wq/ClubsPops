import { Component, Input, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';

@Component({
  selector: 'app-checkbox',
  templateUrl: './checkbox.component.html',
  styleUrls: ['./checkbox.component.scss']
})
export class CheckboxComponent implements ControlValueAccessor {
  @Input() label = '';
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
}