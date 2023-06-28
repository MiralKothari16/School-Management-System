import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { environment as env } from 'src/environments/environment.development';

@Component({
  selector: 'app-teacherregistration',
  templateUrl: './teacherregistration.component.html',
  styleUrls: ['./teacherregistration.component.scss']
})
export class TeacherregistrationComponent implements OnInit {
  teacherform!: FormGroup;
  submitted = false;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.teacherform = new FormGroup({
      name: new FormControl(null, Validators.required),
      email: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl(null, Validators.required),
      class: new FormControl(null, Validators.required),
      subject: new FormControl(null, Validators.required),
      dateOfBirth: new FormControl(null, Validators.required),
      enrollmentdate: new FormControl(null, Validators.required),
      qualification: new FormControl(null, Validators.required),
      isActive: new FormControl(true),
    });
  }
  onSubmit() {
    console.log(this.teacherform.value);
    this.http.post(env.baseUrl + '/Teacher', this.teacherform.value).subscribe({
      next: (response) => { console.log(response) },
    });
    // this.http.post(env.baseUrl + '/Teacher', {
    //   name: this.teacherform.value.name,
    //   email: this.teacherform.value.email,
    //   password: this.teacherform.value.password,
    //   class: this.teacherform.value.class,
    //   subject: this.teacherform.value.subject,
    //   dateOfBirth: this.teacherform.value.dataofbirth,
    //   enrollmentdate: this.teacherform.value.enrollmentdate,
    //   qualification: this.teacherform.value.qualification,
    //   isActive: true
    // }).subscribe();
    this.submitted = true;
  }
  reset() {
    this.teacherform.reset();
    this.submitted = false;
  }
}
