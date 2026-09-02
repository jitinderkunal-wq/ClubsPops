import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AccommodationStatus } from 'app/shared/models/accommodationStatus';
import { AccommodationType } from 'app/shared/models/accommodationType';
import { AccommodationStatusTypeMaintenanceLogWaitingList } from 'app/shared/models/accommodationWithTypeAndStatus';
import { AccommodationStatusTypeMaintenanceRateAndCoupons } from 'app/shared/models/accommodationStatusTypeMaintenanceRateAndCoupons';
import { MaintenanceTask } from 'app/shared/models/maintenanceTask';
import { environment } from 'environments/environment.development';
import { CleaningTask } from 'app/shared/models/cleaningTask';
import { MembershipType } from 'app/shared/models/membershipType';
import { BookedAccommodation } from 'app/shared/models/bookedAccommodation';
import { Organization } from 'app/shared/models/organization';
import { TransferBooking } from 'app/shared/models/transferBooking';
import { Observable } from 'rxjs/internal/Observable';
import { BaseResponse } from 'app/shared/models/responseStatus/response';


@Injectable({
  providedIn: 'root'
})
export class FrontOfHouseService {
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

  getBookedAccommodations() {
    return this.http.get<BookedAccommodation[]>(this.baseUrl + 'Accommodation/bookedAccommodations');
  }

  // getting Booked Accommodation Detail for Check-Out 
  getBookedAccommodationDetail(accommodationId: number) {
    return this.http.get<any>(this.baseUrl + 'Accommodation/bookedAccommodationsDetail/' + accommodationId);
  }

  getAccommodationStatuses() {
    return this.http.get<AccommodationStatus[]>(this.baseUrl + 'Accommodation/getAccommodationStatuses');
  }

  getAccommodationTypes() {
    return this.http.get<AccommodationType[]>(this.baseUrl + 'Accommodation/getAccommodationTypes');
  }

  getAccommodation(accommodationId: number) {
    return this.http.get<AccommodationStatusTypeMaintenanceRateAndCoupons>(this.baseUrl + 'Accommodation/' + accommodationId);
  }

  getOverDueAccommodations() {
    return this.http.get<BookedAccommodation[]>(this.baseUrl + 'Accommodation/overDueAccommodations');
  }

  // Cleaning related services
  // Used in cleaning module also
  getCleaningTasks() {
    return this.http.get<CleaningTask[]>(this.baseUrl + 'Cleaning/cleaningTasks');
  }

  createCleaningLog(values: any): Observable<BaseResponse<null>> {
    return this.http.post<BaseResponse<null>>(this.baseUrl + 'Cleaning/cleaningLog', values);
  }

  // Maintenance related services
  getMaintenanceTasks() {
    return this.http.get<MaintenanceTask[]>(this.baseUrl + 'Maintenance/maintenanceTasks');
  }

  createMaintenanceLog(values: any) {
    return this.http.post(this.baseUrl + 'Maintenance/createMaintenanceLog', values);
  }

  // Used in Maintenance module also
  editMaintenanceLog(values: any): Observable<BaseResponse<null>> {
    return this.http.patch<BaseResponse<null>>(this.baseUrl + 'Maintenance/editMaintenanceLog', values);
  }

  // Problem related services
  createSystemProblem(value: any) {
    return this.http.post(this.baseUrl + 'Problem', value);
  }

  // Booking related services
  bookAccommodation(value: any) {
    return this.http.post(this.baseUrl + 'Booking', value);
  }

  refundAmount(accommodationId: any) {
    return this.http.get(this.baseUrl + 'Booking/refundAmount/' + accommodationId);
  }

  renewBooking(accommodationId: any) {
    return this.http.get(this.baseUrl + 'Booking/' + accommodationId);
  }

  checkOut(accommodationId: number) {
    return this.http.patch(this.baseUrl + 'Booking/' + accommodationId, {});
  }

  // Transfer Booking related services
  transferBooking(bookedAaccommodationId: number) {
    return this.http.get(this.baseUrl + 'Booking/transferBooking/' + bookedAaccommodationId);
  }

  // Waiting List
  getMembershipLists() {
    return this.http.get<MembershipType[]>(this.baseUrl + 'Booking/getMembershipLists');
  }

  createWaitingList(values: any): Observable<BaseResponse<null>> {
    return this.http.post<BaseResponse<null>>(this.baseUrl + 'Booking/addToWaiting', values);
  }

  deleteWaitingList(accommodationId: number) {
    return this.http.get(this.baseUrl + 'Booking/deleteWaiting/' + accommodationId);
  }
}