import { Component, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Iteacher } from 'src/app/Interface/iteacher';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { TeacherService } from 'src/app/Service/teacher.service';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-add-teacher',
  templateUrl: './add-teacher.component.html',
  styleUrls: ['./add-teacher.component.scss']
})
export class AddTeacherComponent {
  teacherform !: FormGroup;

  public teacher: Iteacher = {
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

  constructor(public dialogref: MatDialogRef<AddTeacherComponent>, private teacherservice: TeacherService,
    @Inject(MAT_DIALOG_DATA) public data: Iteacher, public dialog: MatDialog) { }
  ngOnInit(): void {
    this.initializeForm();

  }
  initializeForm(): void {
    this.teacherform = new FormGroup({
      name: new FormControl(this.data.name, [Validators.required]),
      email: new FormControl(this.data.email, [Validators.required]),
      password: new FormControl(this.data.password, [Validators.required]),
      class: new FormControl(this.data.class, [Validators.required]),
      subject: new FormControl(this.data.subject, [Validators.required]),
      dateofBirth: new FormControl(this.data.dataofbirth, [Validators.required]),
      enrollmentdate: new FormControl(this.data.enrollmentdate, [Validators.required]),
      qualification: new FormControl(this.data.qualification, [Validators.required]),
      id: new FormControl(this.data.id, [Validators.required]),
    });
  }
  AddTeacher(teacherform: any) {

    console.log(this.teacherform.value);
    this.teacherservice.addTeacher(teacherform.vale).subscribe(
      response => {
        // Handle successful response
        console.log(response);
      },
      error => {
        // Handle error response
        console.error(error);
      });


    this.dialogref.close(this.teacherform.value);
  }
  reset() {
    this.teacherform.reset();
  }
  close() {
    this.dialogref.close();
  }
}
