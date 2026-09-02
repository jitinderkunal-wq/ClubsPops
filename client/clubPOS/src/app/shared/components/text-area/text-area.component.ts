import { Component, Input, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';

@Component({
  selector: 'app-text-area',
  templateUrl: './text-area.component.html',
  styleUrls: ['./text-area.component.scss']
})
export class TextAreaComponent implements ControlValueAccessor {
  @Input() label = '';
  @Input() rows = 3;
  @Input() cols?: number;
  @Input() customStyles: { [key: string]: string } = {};

  constructor(@Self() public controlDir: NgControl) {
    controlDir.valueAccessor = this;
  }

  writeValue(obj: any): void { }
  registerOnChange(fn: any): void { }
  registerOnTouched(fn: any): void { }

  get control(): FormControl {
    return this.controlDir.control as FormControl;
  }
}