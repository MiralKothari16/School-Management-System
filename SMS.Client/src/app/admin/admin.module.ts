import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

import { AdminRoutingModule } from './admin-routing.module';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { TeachermanagementComponent } from './teachermanagement/teachermanagement.component';
import { AdminmanagementComponent } from './adminmanagement/adminmanagement.component';
import { HeaderComponent } from './header/header.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { StudentmanagementComponent } from './studentmanagement/studentmanagement.component';
import { TeacherDetailComponent } from './teacher-detail/teacher-detail.component';
import { AddTeacherComponent } from './add-teacher/add-teacher.component';
import { MatDialogModule } from '@angular/material/dialog';


@NgModule({
  declarations: [
    AdminDashboardComponent,
    TeachermanagementComponent,
    AdminmanagementComponent,
    StudentmanagementComponent,
    HeaderComponent,
    TeacherDetailComponent,
    AddTeacherComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    MatDialogModule
  ],
  exports: [
    HeaderComponent,]
})
export class AdminModule { }
