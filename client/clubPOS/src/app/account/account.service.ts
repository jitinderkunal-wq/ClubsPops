import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ActiveSalesSession } from 'app/shared/models/activeSalesSession';
import { EditUser } from 'app/shared/models/editUser';
import { Organization } from 'app/shared/models/organization';
import { BaseResponse } from 'app/shared/models/responseStatus/response';
import { Role } from 'app/shared/models/role';
import { Shift } from 'app/shared/models/shift';
import { User } from 'app/shared/models/user';
import { environment } from 'environments/environment.development';
import { Observable, ReplaySubject, map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<User | null>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) { }

  loadCurrentUser(token: string | null) {
    if (token === null) {
      this.currentUserSource.next(null);
      return of(null);
    }

    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.get<User>(this.baseUrl + 'Account', { headers }).pipe(
      map(user => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
          return user;
        } else {
          return null;
        }
      })
    )
  }

  loadCurrentUserFromStorage() {
    const token = localStorage.getItem('token');
    return this.loadCurrentUser(token);
  }

  login(values: any) {
    return this.http.post<User>(this.baseUrl + 'Account/login', values).pipe(
      map(user => {
        localStorage.setItem('token', user.token);
        this.currentUserSource.next(user);
      })
    );
  }

  getToken() {
    const token = localStorage.getItem('token');
    return token;
  }

  logout() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/account/login');
  }

  setIsCurrentlyInUseFalse() {
    return this.http.get(this.baseUrl + 'Account/setIsCurrentlyInUseFalse').pipe(
      map(() => {
        localStorage.setItem('isShiftLoggedIn', "false");
        this.router.navigateByUrl('/dashboard');
      }));
  }

  register(values: any) {
    return this.http.post<User>(this.baseUrl + 'Account/register', values);
  }

  editUser(values: any) {
    return this.http.patch<User>(this.baseUrl + 'Account/editUser', values);
  }

  checkEmailExists(email: string) {
    return this.http.get<boolean>(this.baseUrl + 'Account/emailexists?email=' + email);
  }

  getUsers() {
    // let headers = new HttpHeaders();
    // headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.get<EditUser[]>(this.baseUrl + 'Account/users');
  }

  getRoles() {
    return this.http.get<Role[]>(this.baseUrl + 'Admin/roles');
  }

  // Shift related services
  getShifts() {
    return this.http.get<Shift[]>(this.baseUrl + 'Account/shift');
  }

  getActiveSession() {
    return this.http.get<ActiveSalesSession[]>(this.baseUrl + 'Account/getActiveSession');
  }

  startNewSession(values: any) {
    return this.http.post(this.baseUrl + 'Account/startNewSession', values).pipe(
      map(() => {
        localStorage.setItem('isShiftLoggedIn', "true");
      }));
  }

  startExistingSession(id: number) {
    return this.http.get(this.baseUrl + 'Account/startExistingSession/' + id).pipe(
      map(() => {
        localStorage.setItem('isShiftLoggedIn', "true");
      }));
  }

  endSession(values: any) {
    return this.http.post(this.baseUrl + 'Account/endSession', values).pipe(
      map(() => {
        localStorage.setItem('isShiftLoggedIn', "false");
        this.router.navigateByUrl('/dashboard');
      }));
  }

  createCashDropDrawerSkim(values: any): Observable<BaseResponse<null>> {
    return this.http.post<BaseResponse<null>>(this.baseUrl + 'Account/cashDropDrawerSkim', values);
  }

  checkIfShiftLoggedIn(){ 
    var response = localStorage.getItem('isShiftLoggedIn');
    if(response == "true"){
      return true;
    }
    else if(response == "false"){
      return false;
    }
    else{
      return false;
    }
  }

  // Organization
  currentOrganization(id:any){
    return this.http.get<Organization>(this.baseUrl + 'Organization/' + id);
  }

}