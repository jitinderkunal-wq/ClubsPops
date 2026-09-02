import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
  HttpStatusCode
} from '@angular/common/http';
import { Observable, catchError, map, take, throwError } from 'rxjs';
import { AccountService } from 'app/account/account.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  token?: string;

  constructor(private accountService: AccountService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    const token = this.accountService.getToken();
    if (token != null) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      })
    }
    return next.handle(request).pipe(
      map((event: HttpEvent<any>) => {
        return event;
      }),
      catchError(
        (
          httpErrorResponse: HttpErrorResponse,
          _: Observable<HttpEvent<any>>
        ) => {
          if (httpErrorResponse.status === HttpStatusCode.Unauthorized) {
            //this.token.LogOut();
            // this.router.navigate([`/login`]);
          }
          return throwError(httpErrorResponse);
        }
      )
    );
  }
}