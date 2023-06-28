import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment as env } from 'src/environments/environment.development';


@Injectable({
  providedIn: 'root'
})
export class TeacherService {

  constructor(private http: HttpClient) { }

  getTeachers(): Observable<any> {
    const url = `${env.baseUrl}/Teacher`; // Append the parameters to the URL
    return this.http.get<any[]>(url);
  }
  //https://localhost:7105/api/Student/GetStudentById?id=13
  getTeacherById(id: number) {
    //const url = `${env.baseUrl}/Student/GetStudentById/${id}`;
    const url = `${env.baseUrl}/Teacher/GetTeacherById`;
    const params = { id: id.toString() };
    return this.http.get(url, { params });
  }

}
