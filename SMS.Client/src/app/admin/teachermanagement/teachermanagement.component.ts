import { Component, OnInit } from '@angular/core';
import { Iteacher } from 'src/app/Interface/iteacher';
import { TeacherService } from 'src/app/Service/teacher.service';
import { Observable } from 'rxjs';
import { Route, Router } from '@angular/router';
import { environment as env } from 'src/environments/environment.development';
import { MatDialog } from '@angular/material/dialog';
import { AddTeacherComponent } from '../add-teacher/add-teacher.component';

@Component({
  selector: 'app-teachermanagement',
  templateUrl: './teachermanagement.component.html',
  styleUrls: ['./teachermanagement.component.scss']
})
export class TeachermanagementComponent implements OnInit {

  public teachers !: Iteacher[];
  public updatedId!: string | null;
  public isLoading: boolean = true;
  public isError: boolean = false;

  constructor(private teacherservice: TeacherService, private route: Router, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.getteachers();
  }

  getteachers() {
    this.isLoading = true;
    return this.teacherservice.getTeachers().subscribe({
      next: (res) => {
        this.teachers = res.data;
        this.isLoading = false;
        this.isError = false;
      },
      error: (error) => {
        console.log(error);
        this.isLoading = false;
        this.isError = true;
      }
    });
  }

  public OpenteacherForm() {
    let teacher: Iteacher = {
      id: 0,
      name: '',
      email: '',
      password: '',
      class: '',
      subject: '',
      dataofbirth: new Date(),
      enrollmentdate: new Date(),
      qualification: '',
    };
    const dialogRef = this.dialog.open(AddTeacherComponent, {
      width: '40%', height: '60%',
      data: teacher
    });
    // .afterClosed().subscribe({
    //   next: res => {
    //     this.teacherservice.addTeacher({
    //       name: res.name,
    //       email: res.email,
    //       password: res.password,
    //       class: res.class,
    //       subject: res.subject,
    //       dateofbirth: res.dataofbirth,
    //       enrollment: res.enrollment,
    //       qualification: res.qualification
    //     });
    //   }
    // });
  }
  editTeacher(teacher: Iteacher) {


  }
  ViewTeacher(id: number | undefined) {

    // //'https://localhost:7105/api/Teacher/id?id=2'
    //get path in angular
    this.route.navigate([`/TeacherDetail/${id}`])
  }

}
