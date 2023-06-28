import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment as env } from 'src/environments/environment.development';
import { Istudent } from '../Interface/istudent';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  constructor(private http: HttpClient) { }


  getStudents(): Observable<any> {
    const url = `${env.baseUrl}/Student`; // Append the parameters to the URL
    return this.http.get<any[]>(url);
  }
}
