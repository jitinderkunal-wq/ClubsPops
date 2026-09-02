import { Component, EventEmitter, Input, Output, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.scss']
})
export class TextInputComponent implements ControlValueAccessor {
  @Input() type = 'text';
  @Input() label = '';
  @Input() customStyles: { [key: string]: string } = {};

  @Output() valueChange = new EventEmitter<any>();

  constructor(@Self() public controlDir: NgControl) {
    controlDir.valueAccessor = this;
  }

  writeValue(obj: any): void { }
  registerOnChange(fn: any): void { this.onChange = fn; }
  registerOnTouched(fn: any): void { }

  get control(): FormControl {
    return this.controlDir.control as FormControl
  }

  onInputChange(event: Event) {
    const value = (event.target as HTMLInputElement).value;
    if (value !== undefined) {
      // Handle the input change event as you type
      this.onChange(value); // Notify the parent component about the change
      this.valueChange.emit(value); // Emit the change event with value
    }
  }

  private onChange: any = () => { };
}