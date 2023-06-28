import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Iteacher } from 'src/app/Interface/iteacher';
import { TeacherService } from 'src/app/Service/teacher.service';

@Component({
  selector: 'app-teacher-detail',
  templateUrl: './teacher-detail.component.html',
  styleUrls: ['./teacher-detail.component.scss']
})
export class TeacherDetailComponent implements OnInit {
  public teachers !: Iteacher[];
  id !: number;
  constructor(private teacherserice: TeacherService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.id = +params['id'];

      if (this.id) {
        this.getteacherdettail();
      }
    })
  }
  public getteacherdettail() {
    const id = this.getteacherdettail
    this.teacherserice.getTeacherById(this.id).subscribe(
      (res: any) => { this.teachers = res; },
      (error: any) => { console.error('Error in fetching data', error); }
    );
  }
}

