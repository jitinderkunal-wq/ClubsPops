import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { AdministrativeService } from '../administrative.service';
import { FormBuilder, Validators } from '@angular/forms';
import { CleaningTask } from 'app/shared/models/cleaningTask';

@Component({
  selector: 'app-cleaning-task-setup',
  templateUrl: './cleaning-task-setup.component.html',
  styleUrls: ['./cleaning-task-setup.component.scss']
})
export class CleaningTaskSetupComponent implements OnInit {
  cleaningTasks: CleaningTask[] = [];
  cleaningTaskCreateId: number = 0;
  saveButtonText: string = 'Create';
  errors = '';

  constructor(private matDialogRef: MatDialogRef<CleaningTaskSetupComponent>, private administrativeService: AdministrativeService, private fb: FormBuilder) { }

  cleaningTaskSetupForm = this.fb.group({
    cleaningTaskId: [0],
    taskName: ['', Validators.required],
    isActive: [true]
  });

  ngOnInit(): void {
    this.getCleaningTasks();
  }

  closePopup() {
    this.matDialogRef.close();
  }

  getCleaningTasks() {
    this.administrativeService.getCleaningTasks().subscribe({
      next: response => {
        this.cleaningTasks = response;
        if (this.cleaningTasks.length > 0) {
          const lastCleaningTask = this.cleaningTasks[this.cleaningTasks.length - 1];
          const lastCleaningTaskId = lastCleaningTask.cleaningTaskId;
          this.cleaningTaskCreateId = lastCleaningTaskId + 1;
        } else {
          this.cleaningTaskCreateId = 1;
        }
      },
      error: error => console.log(error)
    });
  }

  onCleaningTaskChange(selectedtask: any) {
    // console.log(selectedtask);

    this.saveButtonText = 'Edit';

    this.cleaningTaskCreateId = selectedtask.cleaningTaskId;

    this.cleaningTaskSetupForm.patchValue({
      cleaningTaskId: selectedtask.cleaningTaskId,
      taskName: selectedtask.taskName,
      isActive: selectedtask.isActive
    });
  }

  onSubmit() {
    if (this.cleaningTaskSetupForm.value.cleaningTaskId) {
      // console.log(this.cleaningTaskSetupForm.value);
      // console.log("Edit Cleaning Task");

      this.administrativeService.editCleaningTask(this.cleaningTaskSetupForm.value).subscribe({
        next: () => {
          this.cleaningTaskSetupForm.reset();
          this.closePopup();
        },
        error: error => {
          console.log(error);
          this.errors = error.errors
        }
      });
    } else {
      // console.log(this.cleaningTaskSetupForm.value);
      // console.log("Create Cleaning Task");

      this.administrativeService.createCleaningTask(this.cleaningTaskSetupForm.value).subscribe({
        next: () => {
          this.cleaningTaskSetupForm.reset();
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