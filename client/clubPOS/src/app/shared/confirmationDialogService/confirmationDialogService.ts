import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class ConfirmationDialogService {

  constructor() { }

  showConfirmationDialog(message: string): Promise<boolean> {
    return Swal.fire({
      title: 'Are you sure?',
      text: message,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, proceed!'
    }).then((result) => {
      return result.isConfirmed;
    });
  }

  warningDialog(message: string) {
    Swal.fire({
      title: 'Warning!',
      text: message,
      icon: 'warning',
      confirmButtonText: 'Okay'
    });
  }
}
