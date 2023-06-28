import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';


import { AppComponent } from './app.component';

import { LayoutComponent } from './Main/layout/layout.component';

import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { StudentRegistrationComponent } from './auth/student-registration/student-registration.component';
import { TeacherregistrationComponent } from './auth/teacherregistration/teacherregistration.component';
import { LoginComponent } from './auth/login/login.component';
import { AuthconfigInterceptor } from './Shared/authconfig.interceptor';
import { TeacherheaderComponent } from './Main/teacherheader/teacherheader.component';
import { StudentheaderComponent } from './Main/studentheader/studentheader.component';
import { HomeComponent } from './home/home.component';
import { PagenotfoundComponentComponent } from './pagenotfound-component/pagenotfound-component.component';
import { AdminModule } from './admin/admin.module';
import { CommonModule } from '@angular/common';




@NgModule({
  declarations: [
    AppComponent,

    LayoutComponent,
    StudentRegistrationComponent,
    TeacherregistrationComponent,
    LoginComponent,
    TeacherheaderComponent,
    StudentheaderComponent,
    HomeComponent,
    PagenotfoundComponentComponent,


  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AdminModule,
    //  CommonModule


  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthconfigInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
