import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, } from '@angular/common/http';
import { Router } from '@angular/router';
import { Iuser } from '../Interface/iuser';
import { Observable, catchError, map, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http: HttpClient, public router: Router) { }

  endpoint: string = 'https://localhost:7105/api/';
  headers = new HttpHeaders().set('Content-Type', 'application/json');
  currentUser = {};


  signUpStudent(user: Iuser): Observable<any> {
    let api = `${this.endpoint}/studentregistration`;
    return this.http.post(api, user).pipe(catchError(this.handleError));
  }


  signUpTeacher(user: Iuser): Observable<any> {
    let api = `${this.endpoint}/teacherregistration`;
    return this.http.post(api, user).pipe(catchError(this.handleError));
  }

  handleError(error: HttpErrorResponse) {
    let msg = '';
    if (error.error instanceof ErrorEvent) {
      // client-side error
      msg = error.error.message;
    } else {
      // server-side error
      msg = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    return throwError(msg);
  }

  // Sign-in
  signIn(user: Iuser) {
    return this.http
      .post<any>(`${this.endpoint}Token/Auth`, user)
      .subscribe((res: any) => {
        localStorage.setItem('access_token', JSON.stringify(res.access_token));
        alert("Login successfull");
        this.router.navigate(['layout']);

      });
  }
  getToken() {
    return JSON.parse(localStorage.getItem('access_token') as string);
  }
  isLoggedIn(): boolean {
    let authToken = localStorage.getItem('access_token');
    return authToken !== null ? true : false;
  }
  doLogout() {
    let removeToken = localStorage.removeItem('access_token');
    if (removeToken == null) {
      this.router.navigate(['/login']);
    }
  }
}
