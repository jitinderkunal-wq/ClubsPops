import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { AccountService } from 'app/account/account.service';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private accountService: AccountService, private router: Router) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> {
    return this.accountService.currentUser$.pipe(
      map((auth) => {
        if (auth) {
          return true;
        } else {
          this.router.navigate(['login'], {
            queryParams: { returnUrl: state.url },
          });
          return false;
        }
      })
    );
  }
}