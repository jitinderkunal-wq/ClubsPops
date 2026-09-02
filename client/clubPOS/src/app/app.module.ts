import { Injector, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatSelectModule } from '@angular/material/select';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MatDialogModule } from '@angular/material/dialog';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ErrorInterceptor } from './core/interceptors/error.interceptor';
import { CoreModule } from './core/core.module';
import { LoadingInterceptor } from './core/interceptors/loading.interceptor';
import { JwtInterceptor } from './core/interceptors/jwt.interceptor';
import { ToastrModule } from 'ngx-toastr';
export let InjectorInstance: Injector;
@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    CoreModule,
    HttpClientModule,
    MatIconModule,
    MatListModule,
    MatSelectModule,
    MatDialogModule,
    ToastrModule.forRoot({
      positionClass: 'toast-top-right' // set position to top right
    })
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(private injector: Injector) {
    InjectorInstance = this.injector;
  }
}