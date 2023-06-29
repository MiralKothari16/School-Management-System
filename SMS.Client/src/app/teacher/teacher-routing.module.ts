import { NgModule } from '@angular/core';
import { ChildrenOutletContexts, RouterModule, Routes } from '@angular/router';
import { TeacherHeaderComponent } from './teacher-header/teacher-header.component';
import { GradebookDashboardComponent } from './gradebook-dashboard/gradebook-dashboard.component';
import { AttndenceDashboardComponent } from './attndence-dashboard/attndence-dashboard.component';
import { RecordattendenceComponent } from './recordattendence/recordattendence.component';
import { AttendencehistoryComponent } from './attendencehistory/attendencehistory.component';

const teacherroutes: Routes = [
  {
    path: '', component: AttndenceDashboardComponent,
    children: [
      { path: 'RecordAttendence', component: RecordattendenceComponent },
      { path: 'AttendenceHisory', component: AttendencehistoryComponent },
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(teacherroutes)],
  exports: [RouterModule]
})
export class TeacherRoutingModule { }
