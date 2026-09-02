import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { AdministrativeService } from '../administrative.service';
import { FormBuilder, Validators } from '@angular/forms';
import { MaintenanceTask } from 'app/shared/models/maintenanceTask';

@Component({
  selector: 'app-maintenance-task-setup',
  templateUrl: './maintenance-task-setup.component.html',
  styleUrls: ['./maintenance-task-setup.component.scss']
})
export class MaintenanceTaskSetupComponent implements OnInit {
  maintenanceTasks: MaintenanceTask[] = [];
  maintenanceTaskCreateId: number = 0;
  saveButtonText: string = 'Create';
  errors = '';

  constructor(private matDialogRef: MatDialogRef<MaintenanceTaskSetupComponent>, private administrativeService: AdministrativeService, private fb: FormBuilder) { }

  maintenanceTaskSetupForm = this.fb.group({
    maintenanceTaskId: [0],
    taskName: ['', Validators.required],
    isEndUserFixable: [true],
    isActive: [true]
  });

  ngOnInit(): void {
    this.getMaintenanceTasks();
  }

  closePopup() {
    this.matDialogRef.close();
  }

  getMaintenanceTasks() {
    this.administrativeService.getMaintenanceTasks().subscribe({
      next: response => {
        this.maintenanceTasks = response;
        console.log(this.maintenanceTasks);
        
        if (this.maintenanceTasks.length > 0) {
          const lastMaintenanceTask = this.maintenanceTasks[this.maintenanceTasks.length - 1];
          const lastMaintenanceTaskId = lastMaintenanceTask.maintenanceTaskId;
          this.maintenanceTaskCreateId = lastMaintenanceTaskId + 1;
        } else {
          this.maintenanceTaskCreateId = 1;
        }
      },
      error: error => console.log(error)
    });
  }

  onMaintenanceTaskChange(selectedtask: any) {
    // console.log(selectedtask);
    this.saveButtonText = 'Edit';

    this.maintenanceTaskCreateId = selectedtask.maintenanceTaskId;

    this.maintenanceTaskSetupForm.patchValue({
      maintenanceTaskId: selectedtask.maintenanceTaskId,
      taskName: selectedtask.taskName,
      isEndUserFixable: selectedtask.isEndUserFixable,
      isActive: selectedtask.isActive
    });
  }

  onSubmit() {
    if (this.maintenanceTaskSetupForm.value.maintenanceTaskId) {
      // console.log(this.maintenanceTaskSetupForm.value);
      // console.log("Edit Maintenance Task");

      this.administrativeService.editMaintenanceTask(this.maintenanceTaskSetupForm.value).subscribe({
        next: () => {
          this.maintenanceTaskSetupForm.reset();
          this.closePopup();
        },
        error: error => {
          console.log(error);
          this.errors = error.errors
        }
      });
    } else {
      // console.log(this.maintenanceTaskSetupForm.value);
      // console.log("Create Maintenance Task");

      this.administrativeService.createMaintenanceTask(this.maintenanceTaskSetupForm.value).subscribe({
        next: () => {
          this.maintenanceTaskSetupForm.reset();
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