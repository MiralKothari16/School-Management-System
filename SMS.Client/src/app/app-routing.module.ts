import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './Main/layout/layout.component';
import { StudentRegistrationComponent } from './auth/student-registration/student-registration.component';
import { TeacherregistrationComponent } from './auth/teacherregistration/teacherregistration.component';
import { LoginComponent } from './auth/login/login.component';
import { AuthGuard } from './Guards/auth.guard';
import { HomeComponent } from './home/home.component';
import { PagenotfoundComponentComponent } from './pagenotfound-component/pagenotfound-component.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  //   { path: 'teacherregistration', component: TeacherregistrationComponent },
  //   { path: 'studentregistration', component: StudentRegistrationComponent },
  //   {
  {
    path: 'layout', component: LayoutComponent, canActivate: [AuthGuard],
    children: [
      { path: 'home', component: HomeComponent },
      { path: 'admin-dashboard', loadChildren: () => import('./admin/admin.module').then((m) => m.AdminModule), },
      { path: 'teacher-dashboard', loadChildren: () => import('./teacher/teacher.module').then((m) => m.TeacherModule), }
    ]
  },
  { path: '', redirectTo: 'login', pathMatch: 'full' },// localhost/4200
  //{ path: '**', component: PagenotfoundComponentComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
