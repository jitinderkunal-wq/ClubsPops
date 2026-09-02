import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { TestErrorComponent } from './core/test-error/test-error.component';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { AuthGuard } from './core/guards/auth.guard';
import { LoginComponent } from './account/login/login.component';
import { RegisterComponent } from './account/register/register.component';
import { CashierReconciliationComponent } from './administrative/cashier-reconciliation/cashier-reconciliation.component';
import {TransferComponent} from './front-of-house/transfer/transfer.component';

const routes: Routes = [
  { path: '', redirectTo: 'account', pathMatch: 'full' },
  { path: 'login', component:LoginComponent },
  { path: 'register', component:RegisterComponent, canActivate:[AuthGuard]},
  { path: 'account', loadChildren: () => import('./account/account.module').then(m => m.AccountModule)},
  { path: 'accommodations', loadChildren: () => import('./front-of-house/front-of-house.module').then(m => m.FrontOfHouseModule), canActivate:[AuthGuard]},
  { path: 'administrative', loadChildren: () => import('./administrative/administrative.module').then(m => m.AdministrativeModule), canActivate:[AuthGuard]},
  { path: 'cleaning', loadChildren: () => import('./cleaning/cleaning.module').then(m => m.CleaningModule), canActivate:[AuthGuard]},
  { path: 'maintenance', loadChildren: () => import('./maintenance/maintenance.module').then(m => m.MaintenanceModule), canActivate:[AuthGuard]},
    { path: 'organization', loadChildren: () => import('./organization/organization.module').then(m => m.OrganizationModule), canActivate: [AuthGuard] },
    { path: 'cashier', component: CashierReconciliationComponent },
  { path: 'test-error', component: TestErrorComponent },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },
  { path: 'dashboard', canActivate: [AuthGuard], component: DashboardComponent },
  { path: '**', redirectTo: 'account', pathMatch: 'full' },
  { path: 'transfer', loadChildren: () => import('./front-of-house/transfer/transfer.component').then(m => m.TransferComponent) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }