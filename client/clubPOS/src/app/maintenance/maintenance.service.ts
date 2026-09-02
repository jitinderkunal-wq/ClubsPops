import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment.development';
import { AccommodationStatusTypeMaintenanceLogWaitingList } from 'app/shared/models/accommodationWithTypeAndStatus';
import { Organization } from 'app/shared/models/organization';
import { BaseResponse } from 'app/shared/models/responseStatus/response';
import { Observable } from 'rxjs/internal/Observable';


@Injectable({
  providedIn: 'root'
})
export class MaintenanceService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  // Organization
  currentOrganization(id: any) {
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

  // Maintenance related services
  editMaintenanceLog(values: any): Observable<BaseResponse<null>> {
    return this.http.patch<BaseResponse<null>>(this.baseUrl + 'Maintenance/editMaintenanceLog', values);
  }
}