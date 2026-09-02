import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TextInputComponent } from './components/text-input/text-input.component';
import { TableRowWithInputComponent } from './components/table-row-with-input/table-row-with-input.component';
import { TextAreaComponent } from './components/text-area/text-area.component';
import { CheckboxComponent } from './components/checkbox/checkbox.component';
import { DropdownComponent } from './components/dropdown/dropdown.component';



@NgModule({
  declarations: [
    TextInputComponent,
    TextAreaComponent,
    CheckboxComponent,
    DropdownComponent,
    TableRowWithInputComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule
  ],
  exports: [
    MatCardModule,
    ReactiveFormsModule,
    FormsModule,
    MatFormFieldModule,
    MatDividerModule,
    TextInputComponent,
    TextAreaComponent,
    CheckboxComponent,
    DropdownComponent,
    TableRowWithInputComponent
  ],
  providers: [
    DatePipe
  ]
})

export class SharedModule { }