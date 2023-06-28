import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment as env } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http: HttpClient) { }

  getAdmins(): Observable<any> {
    const url = `${env.baseUrl}/Admin`; // Append the parameters to the URL
    return this.http.get<any[]>(url);
  }



}
