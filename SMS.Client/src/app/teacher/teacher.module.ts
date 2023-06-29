import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TeacherRoutingModule } from './teacher-routing.module';
import { TeacherDashboardComponent } from './teacher-dashboard/teacher-dashboard.component';
import { TeacherHeaderComponent } from './teacher-header/teacher-header.component';
import { AttndenceDashboardComponent } from './attndence-dashboard/attndence-dashboard.component';
import { GradebookDashboardComponent } from './gradebook-dashboard/gradebook-dashboard.component';
import { RecordattendenceComponent } from './recordattendence/recordattendence.component';
import { AttendencehistoryComponent } from './attendencehistory/attendencehistory.component';
import { RecordAttendenceComponent } from './record-attendence/record-attendence.component';


@NgModule({
  declarations: [
    TeacherDashboardComponent,
    TeacherHeaderComponent,
    AttndenceDashboardComponent,
    GradebookDashboardComponent,
    RecordattendenceComponent,
    AttendencehistoryComponent,
    RecordAttendenceComponent
  ],
  imports: [
    CommonModule,
    TeacherRoutingModule
  ]
})
export class TeacherModule { }
