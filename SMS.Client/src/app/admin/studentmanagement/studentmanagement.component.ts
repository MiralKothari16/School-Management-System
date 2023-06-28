import { Component, OnInit } from '@angular/core';
import { Istudent } from 'src/app/Interface/istudent';
import { StudentService } from 'src/app/Service/student.service';

@Component({
  selector: 'app-studentmanagement',
  templateUrl: './studentmanagement.component.html',
  styleUrls: ['./studentmanagement.component.scss']
})
export class StudentmanagementComponent implements OnInit {
  public students!: Istudent[];
  public updatedId!: string | null;
  public isLoading: boolean = true;
  public isError: boolean = false;

  constructor(private studentservice: StudentService) {

  }

  ngOnInit(): void {
    this.getStudents();
  }

  public OpenStudentForm(): void { }


  public getStudents() {
    this.isLoading = true;
    return this.studentservice.getStudents().subscribe({
      next: (res) => {
        this.students = res.data;
        console.log(this.students);

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

  editStudent(student: Istudent) {


  }
  DeleteStudent(id: number) { }


}
