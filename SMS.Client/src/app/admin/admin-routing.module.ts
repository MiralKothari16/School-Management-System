import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { StudentmanagementComponent } from './studentmanagement/studentmanagement.component';
import { HeaderComponent } from './header/header.component';
import { TeachermanagementComponent } from './teachermanagement/teachermanagement.component';
import { AdminmanagementComponent } from './adminmanagement/adminmanagement.component';
import { TeacherDetailComponent } from './teacher-detail/teacher-detail.component';

const adminroutes: Routes = [
  { path: '', component: HeaderComponent },
  { path: '', redirectTo: 'StudentManagement', pathMatch: 'full' },
  { path: 'StudentManagement', component: StudentmanagementComponent },
  { path: 'TeacherManagement', component: TeachermanagementComponent },
  { path: 'TeacherDetail/:id', component: TeacherDetailComponent },
  { path: 'AdminManagement', component: AdminmanagementComponent }




  // { path: 'header', component: HeaderComponent },
  // { path: '', redirectTo: 'StudentManagement', pathMatch: 'full' },
  // { path: 'StudentManagement', component: StudentmanagementComponent },
  // { path: 'TeacherManagement', component: TeachermanagementComponent },
  // { path: 'AdminManagement', component: AdminmanagementComponent }
];

@NgModule({
  imports: [RouterModule.forChild(adminroutes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
