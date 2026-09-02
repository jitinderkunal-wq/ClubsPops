import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import { TestErrorComponent } from './test-error/test-error.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { NgxSpinnerModule } from 'ngx-spinner';

@NgModule({
  declarations: [
    TestErrorComponent,
    NotFoundComponent,
    ServerErrorComponent
  ],
  imports: [
    CommonModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
      preventDuplicates: true
    }),
    NgxSpinnerModule
  ],
  exports: [
    NgxSpinnerModule
  ]
})

export class CoreModule { }