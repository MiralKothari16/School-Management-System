import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { environment as env } from 'src/environments/environment.development';
import { Router } from '@angular/router';

@Component({
  selector: 'app-student-registration',
  templateUrl: './student-registration.component.html',
  styleUrls: ['./student-registration.component.scss']
})
export class StudentRegistrationComponent implements OnInit {
  studentform!: FormGroup;
  submitted = false;
  unamePattern = "{1,12}$";
  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.studentform = new FormGroup({
      name: new FormControl(null, Validators.required),
      rollNo: new FormControl(null, Validators.required),
      email: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl(null, Validators.required),
      class: new FormControl(null, Validators.required),
      dateofBirth: new FormControl(null, Validators.required),
      dateOfAdmission: new FormControl(null, Validators.required),
      isActive: new FormControl(true)
    });
  }
  //ttps://localhost:7105/api/Student
  //https://localhost:7105/api/Student new date()
  //https://localhost:7105/api/Student
  signUp() {
    console.log(this.studentform.value);
    this.http.post(env.baseUrl + '/Student', this.studentform.value).subscribe({
      next: (response => { this.studentform.reset() }), //console.log(response)
    });
    // subscribe((res) => {
    //   console.log("result :", res);
    // });
    this.submitted = true;
  }

  reset() {
    this.studentform.reset();
    this.submitted = false;
  }
}
