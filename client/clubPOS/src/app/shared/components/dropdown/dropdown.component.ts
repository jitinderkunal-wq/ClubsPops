import { Component, EventEmitter, Input, Output, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';

@Component({
  selector: 'app-dropdown',
  templateUrl: './dropdown.component.html',
  styleUrls: ['./dropdown.component.scss']
})
export class DropdownComponent implements ControlValueAccessor {
  @Input() options: any[] = [];
  @Input() label = '';
  @Input() valueProperty = 'value';
  @Input() labelProperty = 'label';
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

  onSelectChange(event: Event) {
    const selectedId = (event.target as HTMLSelectElement).value;
    const selectedOption = this.options.find(option => {
      const optionValue = option[this.valueProperty];

      // Check if the option value is a number and convert to string if needed
      const normalizedOptionValue = typeof optionValue === 'number' ? optionValue.toString() : optionValue;

      return normalizedOptionValue === selectedId;
    });

    this.onChange(selectedId); // Notify parent component about the change
    this.valueChange.emit(selectedOption); // Emit the change event with selected user
  }

  private onChange: any = () => { };
}