import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment as env } from 'src/environments/environment.development';


@Injectable({
  providedIn: 'root'
})
export class TeacherService {

  private url = `${env.baseUrl}/Teacher`;

  constructor(private http: HttpClient) { }


  getTeachers(): Observable<any> {
    //const url = `${env.baseUrl}/Teacher`; // Append the parameters to the URL
    return this.http.get<any[]>(this.url);
  }
  //https://localhost:7105/api/Teacher
  public addTeacher(teacher: any): Observable<any> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.post(this.url, teacher, { headers });
  }

  //'https://localhost:7105/api/Teacher/id?id=2' \
  // const url = `${base_url}?${params.toString()}`;
  getTeacherById(id: number) {
    //const url = `${env.baseUrl}/Teacher/id?id=`;
    const url = `${env.baseUrl}/Teacher/id`;
    const params = { id: id };
    return this.http.get(url, { params });
  }
}
