import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Membership } from 'app/shared/models/membership';
import { MembershipType } from 'app/shared/models/membershipType';
import { environment } from 'environments/environment.development';
import { TaxAndExchangeRate } from 'app/shared/models/taxAndExchangeRate';
import { CreditCardType } from 'app/shared/models/creditCardType';
import { Coupon } from 'app/shared/models/coupon';
import { AccommodationType } from 'app/shared/models/accommodationType';
import { AccommodationStatusTypeMaintenanceLogWaitingList } from 'app/shared/models/accommodationWithTypeAndStatus';
import { MaintenanceTask } from 'app/shared/models/maintenanceTask';
import { CleaningTask } from 'app/shared/models/cleaningTask';
import { Shift } from 'app/shared/models/shift';
import { AccommodationRate } from 'app/shared/models/accommodationRate';
import { Tax } from 'app/shared/models/tax';
import {CashReconcilation} from 'app/shared/models/cashReconcilation';
import { ActiveSalesSession } from 'app/shared/models/activeSalesSession';
import { ActiveSessionCashReconcilation } from 'app/shared/models/activeSessionCashReconcilation';
import { AccommodationStatus } from 'app/shared/models/accommodationStatus';


@Injectable({
  providedIn: 'root'
})
export class AdministrativeService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  // Accommodation related services
  getAccommodationTypes() {
    return this.http.get<AccommodationType[]>(this.baseUrl + 'Accommodation/getAccommodationTypes');
  }

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

  createAccommodation(values: any) {
    return this.http.post<AccommodationStatusTypeMaintenanceLogWaitingList>(this.baseUrl + 'Accommodation', values);
  }

  editAccommodation(values: any) {
    return this.http.patch<boolean>(this.baseUrl + 'Accommodation', values);
  }

  editAccommodationStatus(accommodationId: number, newAccommodationStatusId: number) {
    return this.http.patch<boolean>(this.baseUrl + `Accommodation/accommodationStatus?accommodationId=${accommodationId}&newAccommodationStatusId=${newAccommodationStatusId}`, {});
  }

  // Cleaning related services
  getCleaningTasks() {
    return this.http.get<CleaningTask[]>(this.baseUrl + 'Cleaning/cleaningTasks');
  }

  createCleaningTask(values: any) {
    return this.http.post<CleaningTask>(this.baseUrl + 'Cleaning/cleaningTask', values);
  }

  editCleaningTask(values: any) {
    return this.http.patch<boolean>(this.baseUrl + 'Cleaning/cleaningTask', values);
  }

  // Accommodation Rate related services
  getAccommodationRates() {
    return this.http.get<AccommodationRate[]>(this.baseUrl + 'Administrative/accommodationRates');
  }

  createAccommodationRate(values: any) {
    return this.http.post<AccommodationRate>(this.baseUrl + 'Administrative/accommodationRate', values);
  }

  editAccommodationRate(values: any) {
    return this.http.patch<boolean>(this.baseUrl + 'Administrative/accommodationRate', values);
  }

  // Coupon related services
  getCoupons() {
    return this.http.get<Coupon[]>(this.baseUrl + 'Administrative/coupons');
  }

  createCoupon(values: any) {
    return this.http.post<Coupon>(this.baseUrl + 'Administrative/coupon', values);
  }

  editCoupon(values: any) {
    return this.http.patch<boolean>(this.baseUrl + 'Administrative/coupon', values);
  }

  // Maintenance related services
  getMaintenanceTasks() {
    return this.http.get<MaintenanceTask[]>(this.baseUrl + 'Maintenance/maintenanceTasks');
  }

  createMaintenanceTask(values: any) {
    return this.http.post<MaintenanceTask>(this.baseUrl + 'Maintenance/maintenanceTask', values);
  }

  editMaintenanceTask(values: any) {
    return this.http.patch<boolean>(this.baseUrl + 'Maintenance/maintenanceTask', values);
  }

  // Accommodation Types related Services
  createAccommodationType(values: any) {
    return this.http.post<AccommodationType>(this.baseUrl + 'Accommodation/createAccommodationType', values);
  }

  updateAccommodationType(values: any) {
    return this.http.post<AccommodationType>(this.baseUrl + 'Accommodation/updateAccommodationType', values);
  }

  // Accommodation Status related Services
  getAccommodationStatuses() {
    return this.http.get<AccommodationStatus[]>(this.baseUrl + 'Accommodation/getAccommodationStatuses');
  }

  createAccommodationStatus(values: any) {
    return this.http.post<AccommodationStatus>(this.baseUrl + 'Accommodation/createAccommodationStatus', values);
  }

  updateAccommodationStatus(values: any) {
    return this.http.post<AccommodationStatus>(this.baseUrl + 'Accommodation/updateAccommodationStatus', values);
  }

  // Membership related services
  getCreditCardTypes() {
    return this.http.get<CreditCardType[]>(this.baseUrl + 'Administrative/creditCardTypes');
  }

  getMembershipTypes() {
    return this.http.get<MembershipType[]>(this.baseUrl + 'Administrative/membershipTypes');
  }

  createMembershipType(values: any) {
    return this.http.post<MembershipType>(this.baseUrl + 'Administrative/membershipType', values);
  }

  editMembershipType(values: any) {
    return this.http.patch<boolean>(this.baseUrl + 'Administrative/membershipType', values);
  }

  getMemberships() {
    return this.http.get<Membership[]>(this.baseUrl + 'Administrative/memberships');
  }

  createMembership(values: any) {
    return this.http.post<Membership>(this.baseUrl + 'Administrative/membership', values);
  }

  editMembership(values: any) {
    return this.http.patch<boolean>(this.baseUrl + 'Administrative/membership', values);
  }

  // Shift related services
  getShifts() {
    return this.http.get<Shift[]>(this.baseUrl + 'Administrative/shift');
  }

  createShift(values: any) {
    return this.http.post<Shift>(this.baseUrl + 'Administrative/shift', values);
  }

  updateShift(values: any) {
    return this.http.patch<Shift>(this.baseUrl + 'Administrative/shift', values);
  }

  // Get Taxe And Exchange Rate service
  getTaxByTaxId(taxId: number) {
    return this.http.get<Tax>(this.baseUrl + 'Administrative/tax/' + taxId);
  }

  getTaxAndExchangeRate() {
    return this.http.get<TaxAndExchangeRate>(this.baseUrl + 'Administrative/taxAndExchangeRates');
  }

  // Update Taxe And Exchange Rate service
  updateTaxAndExchangeRate(value: any) {
    return this.http.post<TaxAndExchangeRate>(this.baseUrl + 'Administrative/updateTaxAndExchangeRates', value);
  }

  // Receipt Message related services
  createReceiptMessagesAndCoupon(value: any) {
    return this.http.post(this.baseUrl + 'Administrative/receiptMessagesAndCoupon', value);
  }

  // Get Session Data of Cash Reconcilation service
  getActiveSession() {
    return this.http.get<ActiveSessionCashReconcilation[]>(this.baseUrl + 'Administrative/getActiveSession');
  }

  // Get Cash Reconcilation service
  getCashReconcilation() {
    return this.http.get<CashReconcilation>(this.baseUrl + 'Administrative/getCashReconcilation');
  }

  createCashReconcilation(value: any) {
    return this.http.post<CashReconcilation>(this.baseUrl + 'Administrative/createCashReconcilation', value);
  }

}
