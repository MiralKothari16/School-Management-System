import { Component, OnInit } from '@angular/core';
import { Iteacher } from 'src/app/Interface/iteacher';
import { TeacherService } from 'src/app/Service/teacher.service';
import { Observable } from 'rxjs';
import { Route, Router } from '@angular/router';

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

  constructor(private teacherservice: TeacherService, private route: Router) { }

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

  public OpenteacherForm() { }

  editTeacher(teacher: Iteacher) {


  }
  ViewTeacher(id: number | undefined) {
    this.route.navigate([`/TeacherDetail/${id}`])
  }

}
