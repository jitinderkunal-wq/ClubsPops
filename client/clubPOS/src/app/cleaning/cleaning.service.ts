import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment.development';
import { AccommodationStatusTypeMaintenanceLogWaitingList } from 'app/shared/models/accommodationWithTypeAndStatus';
import { CleaningTask } from 'app/shared/models/cleaningTask';
import { Organization } from 'app/shared/models/organization';

@Injectable({
  providedIn: 'root'
})
export class CleaningService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  // Organization
  currentOrganization(id:any){
    return this.http.get<Organization>(this.baseUrl + 'Organization/' + id);
  }
  
   // Accommodation related services
   getAccommodations(accommodationTypeId?: number, accommodationStatusId?: number) {
    let params = new HttpParams();

    if (accommodationTypeId) {
      params = params.append('accommodationTypeId', accommodationTypeId);
    }

    if (accommodationStatusId) {
      params = params.append('accommodationStatusId', accommodationStatusId);
    }

    return this.http.get<AccommodationStatusTypeMaintenanceLogWaitingList[]>(this.baseUrl + 'Accommodation', { params });
  }

  // Cleaning related services
  getCleaningTasks() {
    return this.http.get<CleaningTask[]>(this.baseUrl + 'Cleaning/cleaningTasks');
  }

  createCleaningLog(values: any) {
    return this.http.post(this.baseUrl + 'Cleaning/cleaningLog', values);
  }
}
